using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public class ScmlReference
    {
        public const Int32 INVALID = -1;
        public ScmlObject Reference { get; set; }

        public Int32 EntityID { get; private set; }
        public Int32 AnimationID { get; private set; }
        public Int32 Milliseconds { get; set; }

        public ScmlReference()
        {
            Reference = null;
            EntityID = INVALID;
            AnimationID = INVALID;
            Milliseconds = 0;
        }

        public void SetEntity( String pName )
        {
            if ( Reference != null )
            {
                SetCurrentEntity( Reference.Entities.Lookup( pName ) );
            }
        }
        public void SetEntity( Int32 pID )
        {
            if ( Reference != null )
            {
                SetCurrentEntity( Reference.Entities.Lookup( pID ) );
            }
        }
        public void SetAnimation( String pName )
        {
            if ( Reference != null && EntityID != INVALID )
            {
                SetCurrentAnimation( Reference.Entities[EntityID].Animations.Lookup( pName ) );
            }
        }
        public void SetAnimation( Int32 pID )
        {
            if ( Reference != null && EntityID != INVALID )
            {
                SetCurrentAnimation( Reference.Entities[EntityID].Animations.Lookup( pID ) );
            }
        }

        public void Update( Int32 pMilliseconds )
        {
            if ( Reference != null && EntityID != INVALID && AnimationID != INVALID )
            {
                Animation CurrentAnimation = GetCurrentAnimation();

                Milliseconds += pMilliseconds;

                if ( Milliseconds > CurrentAnimation.Length )
                {
                    if ( CurrentAnimation.Looping )
                    {
                        Milliseconds %= CurrentAnimation.Length;
                    }
                    else
                    {
                        Milliseconds = CurrentAnimation.Length;
                    }
                }
            }
        }

        public List<TimelineObjectReference> GetTimelineObjects()
        {
            List<TimelineObjectReference> results = new List<TimelineObjectReference>();
            if ( Reference != null && EntityID != INVALID && AnimationID != INVALID )
            {
                Animation CurrentAnimation = GetCurrentAnimation();

                MainlineKey CurrentMainlineKey = GetCurrentMainlineKey( CurrentAnimation );

                List<TimelineObjectReference> BoneReferences = new List<TimelineObjectReference>();

                for ( int i = 0; i < CurrentMainlineKey.BoneReferences.Count; i++ )
                {
                    BoneReference CurrentBoneReference = CurrentMainlineKey.BoneReferences[i];

                    int TimelineIndex = CurrentBoneReference.Timeline;
                    int TimelineKeyIndex = CurrentBoneReference.Key;
                    int NextTimelineKeyIndex = TimelineKeyIndex;

                    if ( NextTimelineKeyIndex < CurrentAnimation.Timelines[TimelineIndex].Keys.Count - 1 )
                    {
                        NextTimelineKeyIndex++;
                    }

                    TimelineKey PrimaryTimelineKey = CurrentAnimation.Timelines[TimelineIndex].Keys[TimelineKeyIndex];
                    TimelineKey SecondaryTimelineKey = CurrentAnimation.Timelines[TimelineIndex].Keys[NextTimelineKeyIndex];

                    Boolean ConnectToFirstFrame = false;

                    if ( CurrentMainlineKey.ID == CurrentAnimation.MainlineKeys.Count - 1 && CurrentAnimation.Looping && PrimaryTimelineKey.ID > 0 )
                    {
                        SecondaryTimelineKey = CurrentAnimation.Timelines[TimelineIndex].Keys[CurrentAnimation.MainlineKeys[0].BoneReferences[i].Key];
                        ConnectToFirstFrame = true;
                    }

                    TimelineObjectReference Bone;

                    if ( PrimaryTimelineKey == SecondaryTimelineKey )
                    {
                        Bone = GetTimelineObjectReference( PrimaryTimelineKey.Bone, PrimaryTimelineKey.Spin );
                    }
                    else
                    {
                        Single Progress = ( (Single) ( Milliseconds - PrimaryTimelineKey.Time ) ) / ( ( ConnectToFirstFrame ? CurrentAnimation.Length : SecondaryTimelineKey.Time ) - PrimaryTimelineKey.Time );

                        Bone = GetInterpolatedTimelineObjectReference( PrimaryTimelineKey.Bone, SecondaryTimelineKey.Bone, PrimaryTimelineKey.Spin, Progress );
                    }

                    if ( CurrentMainlineKey.BoneReferences[i].Parent != BoneReference.DEFAULT_PARENT )
                    {
                        Bone = ApplyTransformations( BoneReferences[CurrentBoneReference.Parent], Bone );
                    }

                    BoneReferences.Add( Bone );
                }

                for ( int i = 0; i < CurrentMainlineKey.ObjectReferences.Count; i++ )
                {
                    MainlineObjectReference CurrentObjectReference = CurrentMainlineKey.ObjectReferences[i];

                    int TimelineIndex = CurrentObjectReference.Timeline;
                    int TimelineKeyIndex = CurrentObjectReference.Key;
                    int NextTimelineKeyIndex = TimelineKeyIndex;

                    if ( NextTimelineKeyIndex < CurrentAnimation.Timelines[TimelineIndex].Keys.Count - 1 )
                    {
                        NextTimelineKeyIndex++;
                    }

                    TimelineKey PrimaryTimelineKey = CurrentAnimation.Timelines[TimelineIndex].Keys[TimelineKeyIndex];
                    TimelineKey SecondaryTimelineKey = CurrentAnimation.Timelines[TimelineIndex].Keys[NextTimelineKeyIndex];

                    Boolean ConnectToFirstFrame = false;

                    if ( CurrentMainlineKey.ID == CurrentAnimation.MainlineKeys.Count - 1 && CurrentAnimation.Looping && PrimaryTimelineKey.ID > 0 )
                    {
                        SecondaryTimelineKey = CurrentAnimation.Timelines[TimelineIndex].Keys[CurrentAnimation.MainlineKeys[0].ObjectReferences[i].Key];
                        ConnectToFirstFrame = true;
                    }

                    TimelineObject PrimaryTimelineObject = PrimaryTimelineKey.Object;
                    TimelineObjectReference Object;

                    try
                    {
                        if ( PrimaryTimelineKey == SecondaryTimelineKey )
                        {
                            Object = GetTimelineObjectReference( PrimaryTimelineObject, PrimaryTimelineKey.Spin );
                        }
                        else
                        {
                            Single Progress = ( (Single) ( Milliseconds - PrimaryTimelineKey.Time ) ) / ( ( ConnectToFirstFrame ? CurrentAnimation.Length : SecondaryTimelineKey.Time ) - PrimaryTimelineKey.Time );

                            TimelineObject SecondaryTimelineObject = SecondaryTimelineKey.Object;

                            Object = GetInterpolatedTimelineObjectReference( PrimaryTimelineObject, SecondaryTimelineObject, PrimaryTimelineKey.Spin, Progress );
                        }
                    }
                    catch ( Exception )
                    {
                        continue;
                    }

                    if ( CurrentObjectReference.Parent != MainlineObjectReference.DEFAULT_PARENT )
                    {
                        Object = ApplyTransformations( BoneReferences[CurrentObjectReference.Parent], Object );
                    }

                    Object.FileID = PrimaryTimelineObject.File;
                    Object.FolderID = PrimaryTimelineObject.Folder;

                    results.Add( Object );
                }
            }

            return results;
        }

        #region Helper Methods

        private void SetCurrentEntity( Entity pEntity )
        {
            if ( pEntity != null )
            {
                EntityID = pEntity.ID;
            }
            else
            {
                EntityID = INVALID;
            }

            AnimationID = INVALID;
            Milliseconds = 0;
        }

        private void SetCurrentAnimation( Animation pAnimation )
        {
            if ( Reference != null && pAnimation != null )
            {
                AnimationID = pAnimation.ID;
            }
            else
            {
                AnimationID = INVALID;
            }

            Milliseconds = 0;
        }

        private Animation GetCurrentAnimation()
        {
            return Reference.Entities[EntityID].Animations[AnimationID];
        }

        private MainlineKey GetCurrentMainlineKey( Animation pAnimation )
        {
            Int32 KeyID = 0;

            for ( int i = KeyID; i < pAnimation.MainlineKeys.Count; i++ )
            {
                if ( pAnimation.MainlineKeys[i].Time <= Milliseconds )
                {
                    KeyID = i;
                }
                else
                {
                    break;
                }
            }

            return pAnimation.MainlineKeys[KeyID];
        }

        private TimelineObjectReference GetInterpolatedTimelineObjectReference( TimelineObjectBase pPrimaryTimelineObject, TimelineObjectBase pSecondaryTimelineObject, Int32 pSpin, Single pProgress )
        {
            return new TimelineObjectReference()
            {
                AlphaMask = MathHelper.Linear( pPrimaryTimelineObject.AlphaMask, pSecondaryTimelineObject.AlphaMask, pProgress ),
                Angle = MathHelper.AngleLinear( pPrimaryTimelineObject.Angle, pSecondaryTimelineObject.Angle, pProgress, pSpin ),
                BlueMask = MathHelper.Linear( pPrimaryTimelineObject.BlueMask, pSecondaryTimelineObject.BlueMask, pProgress ),
                GreenMask = MathHelper.Linear( pPrimaryTimelineObject.GreenMask, pSecondaryTimelineObject.GreenMask, pProgress ),
                RedMask = MathHelper.Linear( pPrimaryTimelineObject.RedMask, pSecondaryTimelineObject.RedMask, pProgress ),
                ScaleX = MathHelper.Linear( pPrimaryTimelineObject.ScaleX, pSecondaryTimelineObject.ScaleX, pProgress ),
                ScaleY = MathHelper.Linear( pPrimaryTimelineObject.ScaleY, pSecondaryTimelineObject.ScaleY, pProgress ),
                Spin = pSpin,
                X = MathHelper.Linear( pPrimaryTimelineObject.X, pSecondaryTimelineObject.X, pProgress ),
                Y = MathHelper.Linear( pPrimaryTimelineObject.Y, pSecondaryTimelineObject.Y, pProgress )
            };
        }

        private TimelineObjectReference GetTimelineObjectReference( TimelineObjectBase pTimelineObjectBase, Int32 pSpin )
        {
            return new TimelineObjectReference()
            {
                AlphaMask = pTimelineObjectBase.AlphaMask,
                Angle = pTimelineObjectBase.Angle,
                BlueMask = pTimelineObjectBase.BlueMask,
                GreenMask = pTimelineObjectBase.GreenMask,
                RedMask = pTimelineObjectBase.RedMask,
                ScaleX = pTimelineObjectBase.ScaleX,
                ScaleY = pTimelineObjectBase.ScaleY,
                Spin = pSpin,
                X = pTimelineObjectBase.X,
                Y = pTimelineObjectBase.Y
            };
        }

        private TimelineObjectReference ApplyTransformations( TimelineObjectReference pParentReference, TimelineObjectReference pChildReference )
        {
            pChildReference.Angle += pParentReference.Angle;
            pChildReference.ScaleX *= pParentReference.ScaleX;
            pChildReference.ScaleY *= pParentReference.ScaleY;
            pChildReference.AlphaMask *= pParentReference.AlphaMask;

            if ( pChildReference.X != 0 || pChildReference.Y != 0 )
            {
                Single X = pChildReference.X * pParentReference.ScaleX;
                Single Y = pChildReference.Y * pParentReference.ScaleY;

                Double Sin = Math.Sin( GetRadians( pParentReference.Angle ) );
                Double Cos = Math.Cos( GetRadians( pParentReference.Angle ) );

                pChildReference.X = (Single) ( ( X * Cos ) - ( Y * Sin ) );
                pChildReference.Y = (Single) ( ( X * Sin ) - ( Y * Cos ) );

                pChildReference.X += pParentReference.X;
                pChildReference.Y += pParentReference.Y;

            }
            else
            {
                pChildReference.X = pParentReference.X;
                pChildReference.Y = pParentReference.Y;
            }

            return pChildReference;
        }

        private Single GetRadians( Single pAngle )
        {
            return (Single) ( ( pAngle / 360 ) * 2 * Math.PI );
        }

        #endregion
    }
}
