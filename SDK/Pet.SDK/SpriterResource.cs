using BrashMonkey.Spriter;
using BrashMonkey.Spriter.Models;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Pet.SDK
{
    public class SpriterResource : IResource
    {

        private bool isDisposed = false;
        private ScmlObject scmlObject;
        public Dictionary<string, CroppedBitmap> imgDict = new Dictionary<string, CroppedBitmap>();

        public SpriterResource(System.IO.FileStream fs)
        {
            scmlObject = parser(fs);

        }


        internal ScmlObject parser(System.IO.Stream stream)
        {
            ScmlObject scmlObject = null;
            ZipPackage zipPackage = (ZipPackage)ZipPackage.Open(stream);
            foreach (var item in zipPackage.GetParts())
            {
                var path = item.Uri.ToString();
                if (path.EndsWith(".png"))
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = item.GetStream();
                    bi.EndInit();
                    CroppedBitmap bitmap = new CroppedBitmap();
                    bitmap.Source = bi;
                    imgDict.Add(path, bitmap);
                }
                else if (path.EndsWith(".scml"))
                {
                    ScmlLoader loader = new ScmlLoader();
                    scmlObject = loader.LoadFromStream(item.GetStream());
                    Console.WriteLine(scmlObject);
                }
            }
            return scmlObject;
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
            }
        }


        public bool IsDisposed()
        {
            return isDisposed;
        }

        public List<IAnimation> GetAnimations()
        {
            List<IAnimation> list = new List<IAnimation>();
            foreach (var entity in scmlObject.Entities)
            {
                foreach (var animationItem in entity.Animations)
                {
                    IAnimation animation = new Animation(entity, animationItem, scmlObject.Folders);
                    list.Add(animation);
                }
            }
            return list;
        }
    }
}
