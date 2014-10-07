using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using BrashMonkey.Spriter.Infrastructure;

namespace BrashMonkey.Spriter
{
    public class ScmlLoader
    {
        public ScmlLoader()
        { }

        public Models.ScmlObject LoadFromFile( string pFilename )
        {
            if ( File.Exists( pFilename ) )
            {
                return LoadFromStream( new FileStream( pFilename, FileMode.Open ) );
            }

            return null;
        }

        public Models.ScmlObject LoadFromStream( Stream pStream )
        {
            if ( pStream != null )
            {
                using ( TextReader reader = new StreamReader( pStream ) )
                {
                    return LoadFromXml( reader.ReadToEnd() );
                }
            }

            return null;
        }

        public Models.ScmlObject LoadFromXml( string pXml )
        {
            Models.ScmlObject result = new Models.ScmlObject();

            result.Folders = new SpriterKeyList<Models.Folder>();
            result.Entities = new SpriterKeyList<Models.Entity>();

            Dictionary<string, Action<XmlElement, Models.ScmlObject>> elementMap = GetScmlObjectElementMapping( result );

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml( pXml );

                foreach ( XmlElement element in xml.DocumentElement )
                {
                    string key = element.Name.ToLower();

                    if ( elementMap.ContainsKey( key ) )
                    {
                        elementMap[key]( element, result );
                    }
                }
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }

            result.Folders.Compile();
            result.Entities.Compile();

            return result;
        }

        #region Overloads

        protected virtual void OnException( Exception pException )
        { }

        protected virtual void OnScmlObjectLoaded( Models.ScmlObject pObject )
        { }

        protected virtual void OnFolderLoaded( Models.Folder pFolder )
        { }

        protected virtual void OnFileLoaded( Models.File pFile )
        { }

        protected virtual void OnEntityLoaded( Models.Entity pEntity )
        { }

        protected virtual void OnAnimationLoaded( Models.Animation pAnimation )
        { }

        protected virtual void OnMainlineLoaded( Models.Mainline pMainline )
        { }

        protected virtual void OnMainlineKeyLoaded( Models.MainlineKey pMainlineKey )
        { }

        protected virtual void OnMainlineObjectReferenceLoaded( Models.MainlineObjectReference pMainlineObjectReference )
        { }

        protected virtual void OnBoneReferenceLoaded( Models.BoneReference pBoneReference )
        { }

        protected virtual void OnTimelineLoaded( Models.Timeline pTimeline )
        { }

        protected virtual void OnTimelineKeyLoaded( Models.TimelineKey pTimelineKey )
        { }

        protected virtual void OnTimelineObjectLoaded( Models.TimelineObject pTimelineObject )
        { }

        protected virtual void OnTimelineBoneLoaded( Models.TimelineBone pTimelineBone )
        { }

        protected virtual void OnCharacterMapLoaded( Models.CharacterMap pCharacterMap )
        { }

        protected virtual void OnCharacterMapInstructionLoaded( Models.CharacterMapInstruction pCharacterMapInstruction )
        { }

        #endregion

        #region Helper Methods

        #region ScmlObject Loading

        private Dictionary<string, Action<XmlElement, Models.ScmlObject>> GetScmlObjectElementMapping( Models.ScmlObject pObject )
        {
            Dictionary<string, Action<XmlElement, Models.ScmlObject>> map = new Dictionary<string, Action<XmlElement, Models.ScmlObject>>();

            map.Add( Models.Folder.XML_NAME, ( e, m ) => { m.Folders.Add( LoadFolder( e ) ); } );
            map.Add( Models.Entity.XML_NAME, ( e, m ) => { m.Entities.Add( LoadEntity( e ) ); } );

            return map;
        }

        #endregion

        #region Folder Loading

        private Dictionary<string, Action<XmlAttribute, Models.Folder>> GetFolderAttributeMapping( Models.Folder pFolder )
        {
            Dictionary<string, Action<XmlAttribute, Models.Folder>> map = new Dictionary<string, Action<XmlAttribute, Models.Folder>>();

            map.Add( Models.Folder.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.Folder.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );

            return map;
        }
        private Dictionary<string, Action<XmlElement, Models.Folder>> GetFolderElementMapping( Models.Folder pFolder )
        {
            Dictionary<string, Action<XmlElement, Models.Folder>> map = new Dictionary<string, Action<XmlElement, Models.Folder>>();

            map.Add( Models.File.XML_NAME, ( a, m ) => { m.Files.Add( LoadFile( a ) ); } );

            return map;
        }

        private Models.Folder LoadFolder( XmlElement pElement )
        {
            Models.Folder result = new Models.Folder();

            result.Files = new SpriterKeyList<Models.File>();

            Dictionary<string, Action<XmlAttribute, Models.Folder>> attributeMap = GetFolderAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.Folder>> elementMap = GetFolderElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            result.Files.Compile();

            OnFolderLoaded( result );

            return result;
        }

        #endregion

        #region File Loading

        private Dictionary<string, Action<XmlAttribute, Models.File>> GetFileAttributeMapping( Models.File pFile )
        {
            Dictionary<string, Action<XmlAttribute, Models.File>> map = new Dictionary<string, Action<XmlAttribute, Models.File>>();

            map.Add( Models.File.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.File.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );
            map.Add( Models.File.WIDTH_ATTRIBUTE, ( a, m ) => { m.Width = Int32.Parse( a.Value ); } );
            map.Add( Models.File.HEIGHT_ATTRIBUTE, ( a, m ) => { m.Height = Int32.Parse( a.Value ); } );
            map.Add( Models.File.PIVOT_X_ATTRIBUTE, ( a, m ) => { m.PivotX = Single.Parse( a.Value ); } );
            map.Add( Models.File.PIVOT_Y_ATTRIBUTE, ( a, m ) => { m.PivotY = Single.Parse( a.Value ); } );

            return map;
        }

        private Models.File LoadFile( XmlElement pElement )
        {
            Models.File result = new Models.File();

            Dictionary<string, Action<XmlAttribute, Models.File>> map = GetFileAttributeMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( map.ContainsKey( key ) )
                {
                    map[key]( attribute, result );
                }
            }

            OnFileLoaded( result );

            return result;
        }

        #endregion

        #region Entity Loading

        private Dictionary<string, Action<XmlAttribute, Models.Entity>> GetEntityAttributeMapping( Models.Entity pEntity )
        {
            Dictionary<string, Action<XmlAttribute, Models.Entity>> map = new Dictionary<string, Action<XmlAttribute, Models.Entity>>();

            map.Add( Models.Entity.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.Entity.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.Entity>> GetEntityElementMapping( Models.Entity pEntity )
        {
            Dictionary<string, Action<XmlElement, Models.Entity>> map = new Dictionary<string, Action<XmlElement, Models.Entity>>();

            map.Add( Models.Animation.XML_NAME, ( a, m ) => { m.Animations.Add( LoadAnimation( a ) ); } );
            map.Add( Models.CharacterMap.XML_NAME, ( a, m ) => { m.CharacterMaps.Add( LoadCharacterMap( a ) ); } );

            return map;
        }

        private Models.Entity LoadEntity( XmlElement pElement )
        {
            Models.Entity result = new Models.Entity();

            result.Animations = new SpriterKeyList<Models.Animation>();
            result.CharacterMaps = new List<Models.CharacterMap>();

            Dictionary<string, Action<XmlAttribute, Models.Entity>> attributeMap = GetEntityAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.Entity>> elementMap = GetEntityElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            result.Animations.Compile();

            OnEntityLoaded( result );

            return result;
        }


        #endregion

        #region Animation Loading

        private Dictionary<string, Action<XmlAttribute, Models.Animation>> GetAnimationAttributeMapping( Models.Animation pAnimation )
        {
            Dictionary<string, Action<XmlAttribute, Models.Animation>> map = new Dictionary<string, Action<XmlAttribute, Models.Animation>>();

            map.Add( Models.Animation.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.Animation.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );
            map.Add( Models.Animation.LENGTH_ATTRIBUTE, ( a, m ) => { m.Length = Int32.Parse( a.Value ); } );
            map.Add( Models.Animation.LOOPING_ATTRIBUTE, ( a, m ) => { m.Looping = Boolean.Parse( a.Value ); } );
            map.Add( Models.Animation.LOOP_TO_ATTRIBUTE, ( a, m ) => { m.LoopTo = Int32.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.Animation>> GetAnimationElementMapping( Models.Animation pAnimation )
        {
            Dictionary<string, Action<XmlElement, Models.Animation>> map = new Dictionary<string, Action<XmlElement, Models.Animation>>();

            map.Add( Models.Mainline.XML_NAME, ( a, m ) => { m.MainlineKeys = LoadMainline( a ); } );
            map.Add( Models.Timeline.XML_NAME, ( a, m ) => { m.Timelines.Add( LoadTimeline( a ) ); } );

            return map;
        }

        private Models.Animation LoadAnimation( XmlElement pElement )
        {
            Models.Animation result = new Models.Animation();

            result.MainlineKeys = new Models.Mainline();
            result.Timelines = new SpriterKeyList<Models.Timeline>();

            Dictionary<string, Action<XmlAttribute, Models.Animation>> attributeMap = GetAnimationAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.Animation>> elementMap = GetAnimationElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            result.Timelines.Compile();

            OnAnimationLoaded( result );

            return result;
        }

        #endregion

        #region Mainline Loading

        private Dictionary<string, Action<XmlElement, Models.Mainline>> GetMainlineElementMapping( Models.Mainline pMainline )
        {
            Dictionary<string, Action<XmlElement, Models.Mainline>> map = new Dictionary<string, Action<XmlElement, Models.Mainline>>();

            map.Add( Models.MainlineKey.XML_NAME, ( a, m ) => { m.Add( LoadMainlineKey( a ) ); } );

            return map;
        }

        private Models.Mainline LoadMainline( XmlElement pElement )
        {
            Models.Mainline result = new Models.Mainline();

            Dictionary<string, Action<XmlElement, Models.Mainline>> elementMap = GetMainlineElementMapping( result );

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnMainlineLoaded( result );

            return result;
        }

        #endregion

        #region MainlineKey Loading

        private Dictionary<string, Action<XmlAttribute, Models.MainlineKey>> GetMainlineKeyAttributeMapping( Models.MainlineKey pFolder )
        {
            Dictionary<string, Action<XmlAttribute, Models.MainlineKey>> map = new Dictionary<string, Action<XmlAttribute, Models.MainlineKey>>();

            map.Add( Models.MainlineKey.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );
            map.Add( Models.MainlineKey.TIME_ATTRIBUTE, ( a, m ) => { m.Time = Int32.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.MainlineKey>> GetMainlineKeyElementMapping( Models.MainlineKey pFolder )
        {
            Dictionary<string, Action<XmlElement, Models.MainlineKey>> map = new Dictionary<string, Action<XmlElement, Models.MainlineKey>>();

            map.Add( Models.MainlineObjectReference.XML_NAME, ( a, m ) => { m.ObjectReferences.Add( LoadMainlineObjectReference( a ) ); } );
            map.Add( Models.BoneReference.XML_NAME, ( a, m ) => { m.BoneReferences.Add( LoadBoneReference( a ) ); } );

            return map;
        }

        private Models.MainlineKey LoadMainlineKey( XmlElement pElement )
        {
            Models.MainlineKey result = new Models.MainlineKey();

            result.ObjectReferences = new List<Models.MainlineObjectReference>();
            result.BoneReferences = new List<Models.BoneReference>();

            Dictionary<string, Action<XmlAttribute, Models.MainlineKey>> attributeMap = GetMainlineKeyAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.MainlineKey>> elementMap = GetMainlineKeyElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnMainlineKeyLoaded( result );

            return result;
        }

        #endregion

        #region MainlineObjectReference Loading

        private Dictionary<string, Action<XmlAttribute, Models.MainlineObjectReference>> GetMainlineObjectReferenceAttributeMapping( Models.MainlineObjectReference pMainlineObjectReference )
        {
            Dictionary<string, Action<XmlAttribute, Models.MainlineObjectReference>> map = new Dictionary<string, Action<XmlAttribute, Models.MainlineObjectReference>>();

            map.Add( Models.MainlineObjectReference.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );
            map.Add( Models.MainlineObjectReference.KEY_ATTRIBUTE, ( a, m ) => { m.Key = Int32.Parse( a.Value ); } );
            map.Add( Models.MainlineObjectReference.TIMELINE_ATTRIBUTE, ( a, m ) => { m.Timeline = Int32.Parse( a.Value ); } );
            map.Add( Models.MainlineObjectReference.PARENT_ATTRIBUTE, ( a, m ) => { m.Parent = Int32.Parse( a.Value ); } );
            map.Add( Models.MainlineObjectReference.Z_INDEX_ATTRIBUTE, ( a, m ) => { m.ZIndex = Int32.Parse( a.Value ); } );

            return map;
        }

        private Models.MainlineObjectReference LoadMainlineObjectReference( XmlElement pElement )
        {
            Models.MainlineObjectReference result = new Models.MainlineObjectReference();

            Dictionary<string, Action<XmlAttribute, Models.MainlineObjectReference>> attributeMap = GetMainlineObjectReferenceAttributeMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            OnMainlineObjectReferenceLoaded( result );

            return result;
        }

        #endregion

        #region BoneReference Loading

        private Dictionary<string, Action<XmlAttribute, Models.BoneReference>> GetBoneReferenceAttributeMapping( Models.BoneReference pBoneReference )
        {
            Dictionary<string, Action<XmlAttribute, Models.BoneReference>> map = new Dictionary<string, Action<XmlAttribute, Models.BoneReference>>();

            map.Add( Models.BoneReference.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );
            map.Add( Models.BoneReference.KEY_ATTRIBUTE, ( a, m ) => { m.Key = Int32.Parse( a.Value ); } );
            map.Add( Models.BoneReference.TIMELINE_ATTRIBUTE, ( a, m ) => { m.Timeline = Int32.Parse( a.Value ); } );
            map.Add( Models.BoneReference.PARENT_ATTRIBUTE, ( a, m ) => { m.Parent = Int32.Parse( a.Value ); } );

            return map;
        }

        private Models.BoneReference LoadBoneReference( XmlElement pElement )
        {
            Models.BoneReference result = new Models.BoneReference();

            Dictionary<string, Action<XmlAttribute, Models.BoneReference>> attributeMap = GetBoneReferenceAttributeMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            OnBoneReferenceLoaded( result );

            return result;
        }

        #endregion

        #region Timeline Loading

        private Dictionary<string, Action<XmlAttribute, Models.Timeline>> GetTimelineAttributeMapping( Models.Timeline pTimeline )
        {
            Dictionary<string, Action<XmlAttribute, Models.Timeline>> map = new Dictionary<string, Action<XmlAttribute, Models.Timeline>>();

            map.Add( Models.Timeline.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.Timeline.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.Timeline>> GetTimelineElementMapping( Models.Timeline pTimeline )
        {
            Dictionary<string, Action<XmlElement, Models.Timeline>> map = new Dictionary<string, Action<XmlElement, Models.Timeline>>();

            map.Add( Models.TimelineKey.XML_NAME, ( a, m ) => { m.Keys.Add( LoadTimelineKey( a ) ); } );

            return map;
        }

        private Models.Timeline LoadTimeline( XmlElement pElement )
        {
            Models.Timeline result = new Models.Timeline();

            result.Keys = new List<Models.TimelineKey>();

            Dictionary<string, Action<XmlAttribute, Models.Timeline>> attributeMap = GetTimelineAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.Timeline>> elementMap = GetTimelineElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnTimelineLoaded( result );

            return result;
        }

        #endregion

        #region TimelineKey Loading

        private Dictionary<string, Action<XmlAttribute, Models.TimelineKey>> GetTimelineKeyAttributeMapping( Models.TimelineKey pFolder )
        {
            Dictionary<string, Action<XmlAttribute, Models.TimelineKey>> map = new Dictionary<string, Action<XmlAttribute, Models.TimelineKey>>();

            map.Add( Models.TimelineKey.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );
            map.Add( Models.TimelineKey.TIME_ATTRIBUTE, ( a, m ) => { m.Time = Int32.Parse( a.Value ); } );
            map.Add( Models.TimelineKey.C1_ATTRIBUTE, ( a, m ) => { m.C1 = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineKey.C2_ATTRIBUTE, ( a, m ) => { m.C2 = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineKey.SPIN_ATTRIBUTE, ( a, m ) => { m.Spin = Int32.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.TimelineKey>> GetTimelineKeyElementMapping( Models.TimelineKey pFolder )
        {
            Dictionary<string, Action<XmlElement, Models.TimelineKey>> map = new Dictionary<string, Action<XmlElement, Models.TimelineKey>>();

            map.Add( Models.TimelineObject.XML_NAME, ( a, m ) => { m.Object = LoadTimelineObject( a ); } );
            map.Add( Models.TimelineBone.XML_NAME, ( a, m ) => { m.Bone = LoadTimelineBone( a ); } );

            return map;
        }

        private Models.TimelineKey LoadTimelineKey( XmlElement pElement )
        {
            Models.TimelineKey result = new Models.TimelineKey();

            Dictionary<string, Action<XmlAttribute, Models.TimelineKey>> attributeMap = GetTimelineKeyAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.TimelineKey>> elementMap = GetTimelineKeyElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnTimelineKeyLoaded( result );

            return result;
        }

        #endregion

        #region TimelineObject Loading

        private Dictionary<string, Action<XmlAttribute, Models.TimelineObject>> GetTimelineObjectAttributeMapping( Models.TimelineObject pFolder )
        {
            Dictionary<string, Action<XmlAttribute, Models.TimelineObject>> map = new Dictionary<string, Action<XmlAttribute, Models.TimelineObject>>();

            map.Add( Models.TimelineObject.X_ATTRIBUTE, ( a, m ) => { m.X = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.Y_ATTRIBUTE, ( a, m ) => { m.Y = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.ANGLE_ATTRIBUTE, ( a, m ) => { m.Angle = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.SCALE_X_ATTRIBUTE, ( a, m ) => { m.ScaleX = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.SCALE_Y_ATTRIBUTE, ( a, m ) => { m.ScaleY = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.RED_MASK_ATTRIBUTE, ( a, m ) => { m.RedMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.BLUE_MASK_ATTRIBUTE, ( a, m ) => { m.BlueMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.GREEN_MASK_ATTRIBUTE, ( a, m ) => { m.GreenMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.ALPHA_MASK_ATTRIBUTE, ( a, m ) => { m.AlphaMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.FOLDER_ATTRIBUTE, ( a, m ) => { m.Folder = Int32.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.FILE_ATTRIBUTE, ( a, m ) => { m.File = Int32.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.TimelineObject.PIVOT_X_ATTRIBUTE, ( a, m ) => { m.PivotX = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineObject.PIVOT_Y_ATTRIBUTE, ( a, m ) => { m.PivotY = Single.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.TimelineObject>> GetTimelineObjectElementMapping( Models.TimelineObject pFolder )
        {
            Dictionary<string, Action<XmlElement, Models.TimelineObject>> map = new Dictionary<string, Action<XmlElement, Models.TimelineObject>>();

            //map.Add( Models.TimelineObject.XML_NAME, ( a, m ) => { m.MainlineObjectReferences.Add( LoadMainlineObjectReference( a ) ); } );

            return map;
        }

        private Models.TimelineObject LoadTimelineObject( XmlElement pElement )
        {
            Models.TimelineObject result = new Models.TimelineObject();

            Dictionary<string, Action<XmlAttribute, Models.TimelineObject>> attributeMap = GetTimelineObjectAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.TimelineObject>> elementMap = GetTimelineObjectElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnTimelineObjectLoaded( result );

            return result;
        }

        #endregion

        #region TimelineBone Loading

        private Dictionary<string, Action<XmlAttribute, Models.TimelineBone>> GetTimelineBoneAttributeMapping( Models.TimelineBone pFolder )
        {
            Dictionary<string, Action<XmlAttribute, Models.TimelineBone>> map = new Dictionary<string, Action<XmlAttribute, Models.TimelineBone>>();

            map.Add( Models.TimelineBone.X_ATTRIBUTE, ( a, m ) => { m.X = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.Y_ATTRIBUTE, ( a, m ) => { m.Y = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.ANGLE_ATTRIBUTE, ( a, m ) => { m.Angle = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.SCALE_X_ATTRIBUTE, ( a, m ) => { m.ScaleX = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.SCALE_Y_ATTRIBUTE, ( a, m ) => { m.ScaleY = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.RED_MASK_ATTRIBUTE, ( a, m ) => { m.RedMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.BLUE_MASK_ATTRIBUTE, ( a, m ) => { m.BlueMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.GREEN_MASK_ATTRIBUTE, ( a, m ) => { m.GreenMask = Single.Parse( a.Value ); } );
            map.Add( Models.TimelineBone.ALPHA_MASK_ATTRIBUTE, ( a, m ) => { m.AlphaMask = Single.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.TimelineBone>> GetTimelineBoneElementMapping( Models.TimelineBone pFolder )
        {
            Dictionary<string, Action<XmlElement, Models.TimelineBone>> map = new Dictionary<string, Action<XmlElement, Models.TimelineBone>>();

            //map.Add( Models.TimelineBone.XML_NAME, ( a, m ) => { m.MainlineObjectReferences.Add( LoadMainlineObjectReference( a ) ); } );

            return map;
        }

        private Models.TimelineBone LoadTimelineBone( XmlElement pElement )
        {
            Models.TimelineBone result = new Models.TimelineBone();

            Dictionary<string, Action<XmlAttribute, Models.TimelineBone>> attributeMap = GetTimelineBoneAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.TimelineBone>> elementMap = GetTimelineBoneElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnTimelineBoneLoaded( result );

            return result;
        }

        #endregion

        #region CharacterMap Loading

        private Dictionary<string, Action<XmlAttribute, Models.CharacterMap>> GetCharacterMapAttributeMapping( Models.CharacterMap pCharacterMap )
        {
            Dictionary<string, Action<XmlAttribute, Models.CharacterMap>> map = new Dictionary<string, Action<XmlAttribute, Models.CharacterMap>>();

            map.Add( Models.CharacterMap.NAME_ATTRIBUTE, ( a, m ) => { m.Name = a.Value; } );
            map.Add( Models.CharacterMap.ID_ATTRIBUTE, ( a, m ) => { m.ID = Int32.Parse( a.Value ); } );

            return map;
        }

        private Dictionary<string, Action<XmlElement, Models.CharacterMap>> GetCharacterMapElementMapping( Models.CharacterMap pCharacterMap )
        {
            Dictionary<string, Action<XmlElement, Models.CharacterMap>> map = new Dictionary<string, Action<XmlElement, Models.CharacterMap>>();

            map.Add( Models.CharacterMapInstruction.XML_NAME, ( a, m ) => { m.Maps.Add( LoadCharacterMapInstruction( a ) ); } );

            return map;
        }

        private Models.CharacterMap LoadCharacterMap( XmlElement pElement )
        {
            Models.CharacterMap result = new Models.CharacterMap();

            result.Maps = new List<Models.CharacterMapInstruction>();

            Dictionary<string, Action<XmlAttribute, Models.CharacterMap>> attributeMap = GetCharacterMapAttributeMapping( result );
            Dictionary<string, Action<XmlElement, Models.CharacterMap>> elementMap = GetCharacterMapElementMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( attributeMap.ContainsKey( key ) )
                {
                    attributeMap[key]( attribute, result );
                }
            }

            foreach ( XmlElement element in pElement )
            {
                string key = element.Name.ToLower();

                if ( elementMap.ContainsKey( key ) )
                {
                    elementMap[key]( element, result );
                }
            }

            OnCharacterMapLoaded( result );

            return result;
        }

        #endregion

        #region CharacterMapInstruction Loading

        private Dictionary<string, Action<XmlAttribute, Models.CharacterMapInstruction>> GetCharacterMapInstructionAttributeMapping( Models.CharacterMapInstruction pCharacterMapInstruction )
        {
            Dictionary<string, Action<XmlAttribute, Models.CharacterMapInstruction>> map = new Dictionary<string, Action<XmlAttribute, Models.CharacterMapInstruction>>();

            map.Add( Models.CharacterMapInstruction.FOLDER_ATTRIBUTE, ( a, m ) => { m.Folder = Int32.Parse( a.Value ); } );
            map.Add( Models.CharacterMapInstruction.FILE_ATTRIBUTE, ( a, m ) => { m.File = Int32.Parse( a.Value ); } );
            map.Add( Models.CharacterMapInstruction.TARGET_FOLDER_ATTRIBUTE, ( a, m ) => { m.TargetFolder = Int32.Parse( a.Value ); } );
            map.Add( Models.CharacterMapInstruction.TARGET_FILE_ATTRIBUTE, ( a, m ) => { m.TargetFile = Int32.Parse( a.Value ); } );

            return map;
        }

        private Models.CharacterMapInstruction LoadCharacterMapInstruction( XmlElement pElement )
        {
            Models.CharacterMapInstruction result = new Models.CharacterMapInstruction();

            Dictionary<string, Action<XmlAttribute, Models.CharacterMapInstruction>> map = GetCharacterMapInstructionAttributeMapping( result );

            foreach ( XmlAttribute attribute in pElement.Attributes )
            {
                string key = attribute.Name.ToLower();

                if ( map.ContainsKey( key ) )
                {
                    map[key]( attribute, result );
                }
            }

            OnCharacterMapInstructionLoaded( result );

            return result;
        }

        #endregion

        #endregion
    }
}
