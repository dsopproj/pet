using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pet.SDK
{
    public partial class Animation : IAnimation
    {
        //fields & propertys
        private BrashMonkey.Spriter.Models.Entity mEntity;
        private BrashMonkey.Spriter.Models.Animation mAnimation;
        private BrashMonkey.Spriter.Infrastructure.SpriterKeyList<BrashMonkey.Spriter.Models.Folder> folders;
        private Dictionary<string, Image> imgDict;


        private long startTime;

        public int ID { get { return mAnimation.ID; } }

        public string Name { get { return mAnimation.Name; } }

        //ctor
        public Animation(BrashMonkey.Spriter.Models.Entity entity,
            BrashMonkey.Spriter.Models.Animation animation,
            BrashMonkey.Spriter.Infrastructure.SpriterKeyList<BrashMonkey.Spriter.Models.Folder> spriterKeyList,
            Dictionary<string, Image> imgDict)
        {
            this.mEntity = entity;
            this.mAnimation = animation;
            this.folders = spriterKeyList;
            this.imgDict = imgDict;
        }

        //interface
        public void Play(Body body, BrashMonkey.Spriter.Models.ScmlReference scmlReference)
        {
            scmlReference.SetEntity(mEntity.ID);
            scmlReference.SetAnimation(mAnimation.ID);

            scmlReference.Update(DateTime.Now.Millisecond);
            var objs = scmlReference.GetTimelineObjects();
            body.Children.Clear();
            foreach (var item in objs)
            {
                var fileKey = getFileKey(item.FolderID, item.FileID);
                if (fileKey != null && imgDict.ContainsKey("/" + fileKey.Name))
                {
                    var img = imgDict["/" + fileKey.Name];
                    img.Width = fileKey.Width;
                    img.Height = fileKey.Height;

                    TransformGroup tfg = new TransformGroup();
                    var st = new ScaleTransform(item.ScaleX, item.ScaleY);
                    tfg.Children.Add(st);
                    var rt = new RotateTransform(360.0f - item.Angle);
                    tfg.Children.Add(rt);
                    var tt = new TranslateTransform(item.X, item.Y);
                    tfg.Children.Add(tt);
                    img.RenderTransform = tfg;
                    body.Children.Add(img);
                }
            }
        }

        private BrashMonkey.Spriter.Models.File getFileKey(int FolderID, int FileID)
        {
            foreach (var folder in folders)
            {
                if (folder.ID == FolderID)
                {
                    foreach (var file in folder.Files)
                    {
                        if (file.ID == FileID)
                        {
                            return file;
                        }
                    }
                }
            }
            return null;
        }

        private void updateAnimation(float newTime)
        {
            System.Windows.Media.Imaging.CroppedBitmap aaa = null;

        }

        public void Stop()
        {
            throw new NotImplementedException();
        }


        public bool PlayFinished()
        {
            return true;
        }

        public string GetKey()
        {
            return Name;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }



    }
}
