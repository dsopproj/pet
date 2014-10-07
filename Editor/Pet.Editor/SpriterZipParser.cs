using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using BrashMonkey.Spriter;

namespace Pet.Editor
{
    public class SpriterZipParser
    {
        public Dictionary<string, CroppedBitmap> imgDict = new Dictionary<string, CroppedBitmap>();
        private const string PackageRelationshipType =
            @"http://schemas.microsoft.com/opc/2006/sample/document";
        private const string ResourceRelationshipType =
            @"http://schemas.microsoft.com/opc/2006/sample/required-resource";


        internal bool parser(System.IO.Stream stream)
        {
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
                    var scmlObject = loader.LoadFromStream(item.GetStream());
                    Console.WriteLine(scmlObject);
                }

            }



            // Get the Package Relationships and look for
            //   the Document part based on the RelationshipType
            Uri uriDocumentTarget = null;
            foreach (PackageRelationship relationship in
                zipPackage.GetRelationshipsByType(PackageRelationshipType))
            {
                // Resolve the Relationship Target Uri
                //   so the Document Part can be retrieved.
                uriDocumentTarget = PackUriHelper.ResolvePartUri(
                    new Uri("/", UriKind.Relative), relationship.TargetUri);

                // Open the Document Part, write the contents to a file.
                var part = zipPackage.GetPart(uriDocumentTarget);

                part.GetStream();

            }

            //// Get the Document part's Relationships,
            ////   and look for required resources.
            //Uri uriResourceTarget = null;
            //foreach (PackageRelationship relationship in
            //    documentPart.GetRelationshipsByType(
            //                            ResourceRelationshipType))
            //{
            //    // Resolve the Relationship Target Uri
            //    //   so the Resource Part can be retrieved.
            //    uriResourceTarget = PackUriHelper.ResolvePartUri(
            //        documentPart.Uri, relationship.TargetUri);

            //    // Open the Resource Part and write the contents to a file.
            //    resourcePart = package.GetPart(uriResourceTarget);
            //    ExtractPart(resourcePart, targetDirectory);
            //}


            //ScmlLoader loader = new ScmlLoader();
            //loader.LoadFromStream();

            return true;
        }
    }
}
