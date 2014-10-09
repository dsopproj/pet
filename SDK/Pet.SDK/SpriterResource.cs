using BrashMonkey.Spriter;
using BrashMonkey.Spriter.Models;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class SpriterResource : IResource
    {

        private bool isDisposed = false;
        private ScmlObject scmlObject;
        public Dictionary<string, Image> imgDict = new Dictionary<string, Image>();
        private BrashMonkey.Spriter.Models.ScmlReference scmlReference;

        public BrashMonkey.Spriter.Models.ScmlReference ScmlReference { get { return scmlReference; } }

        public SpriterResource(System.IO.FileStream fs)
        {
            scmlObject = parser(fs);
            scmlReference = new ScmlReference();
            scmlReference.Reference = scmlObject;
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
                    Image bitmap = new Image();
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
                    IAnimation animation = new Animation(entity, animationItem, scmlObject.Folders, imgDict);
                    list.Add(animation);
                }
            }
            return list;
        }
    }
}
