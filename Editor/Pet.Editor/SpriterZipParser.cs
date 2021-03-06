﻿using BrashMonkey.Spriter;
using BrashMonkey.Spriter.Models;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Pet.Editor
{
    public class SpriterZipParser
    {

        public Dictionary<string, CroppedBitmap> imgDict = new Dictionary<string, CroppedBitmap>();


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
    }
}
