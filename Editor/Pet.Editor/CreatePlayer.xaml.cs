using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO.Packaging;
using BrashMonkey.Spriter;

namespace Pet.Editor
{
    /// <summary>
    /// CreatePlayer.xaml 的交互逻辑
    /// </summary>
    public partial class CreatePlayer : UserControl
    {
        //fields 
        private string relativePath;
        private SpriterManager spriterManager;

        //constractor
        public CreatePlayer()
        {
            InitializeComponent();
        }

        //events.
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnLoadZip_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "*.zip|*.zip";
            if (ofd.ShowDialog().Value)
            {
                SpriterZipParser sp = new SpriterZipParser(); 
                bool value = sp.parser(ofd.OpenFile());
                MessageBox.Show("ret:" + value);

            }
        }

        //public


        //private

        private void unZipSpriter()
        {

        }

        private void zipSpriter()
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "*.scml|*.scml";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                var file = new FileInfo(ofd.FileName);
                relativePath = file.DirectoryName;
                //var folder = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf(Path.DirectorySeparatorChar));
                //var zipFilePath = folder.Substring(0, folder.LastIndexOf(Path.DirectorySeparatorChar)) + ofd.SafeFileName + ".zip";
                var zipFilePath = relativePath + ".zip";
                string folder = relativePath;

                ZipPackage package = (ZipPackage)ZipPackage.Open(zipFilePath, FileMode.Create);
                var dirInfo = Directory.CreateDirectory(folder);
                DirectoryInfo di = new DirectoryInfo(folder);
                addDirectoryInfoToZip(package, di);
                package.Close();
                MessageBox.Show("OK");
            }

        }

        //add DirectoryInfo To Zip
        private void addDirectoryInfoToZip(ZipPackage package, DirectoryInfo di)
        {
            if (di.GetFiles() != null)
                foreach (var item in di.GetFiles())
                {
                    AddFileToZip(package, item);
                }
            if (di.GetDirectories() != null)
                foreach (var item in di.GetDirectories())
                {
                    addDirectoryInfoToZip(package, item);
                }
        }

        //Method to add files and folder within the zip file package
        private void AddFileToZip(ZipPackage package, FileInfo item)
        {
            //Here get the file node and add it in the Zip file. Make sure 
            //to maintain the folder structure that is defined within the tree view.
            string physicalfilePath = item.FullName;

            //Check for file existing. If file does not exists,
            //then add in the report to generate at the end of the process.
            if (System.IO.File.Exists(physicalfilePath))
            {

                var filePath = physicalfilePath.Replace(relativePath, "");
                filePath = filePath.Replace("\\", "/");

                // Incase if there is space in the file name, then this won't work.
                // So we need to remove space from the file name and replace it with "_"
                string fileName = System.IO.Path.GetFileName(filePath);

                filePath = filePath.Replace(fileName,
                                  fileName.Replace(" ", "_"));

                //create Packagepart that will represent file that we will add. 
                //Define URI for this file that needs to be added within the Zip file.
                //Define the added file as related to the Zip file in terms of the path.
                //This part will define the location where this file needs 
                //to be extracted while the zip file is getting exteacted.
                Uri partURI = new Uri(filePath, UriKind.Relative);

                string contentType = System.Net.Mime.MediaTypeNames.Application.Zip;

                //Define the Content Type of the file that we will be adding. 
                //This depends upon the file extension
                switch (System.IO.Path.GetExtension(filePath).ToLower())
                {
                    case (".xml"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Text.Xml;
                            break;
                        }

                    case (".txt"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Text.Plain;
                            break;
                        }

                    case (".rtf"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Application.Rtf;
                            break;
                        }

                    case (".gif"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Image.Gif;
                            break;
                        }
                    case (".jpeg"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                            break;
                        }
                    case (".jpg"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                            break;
                        }
                    case (".png"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                            break;
                        }
                    case (".tiff"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Image.Tiff;
                            break;
                        }
                    case (".pdf"):
                        {
                            contentType =
                                  System.Net.Mime.MediaTypeNames.Application.Pdf;
                            break;
                        }
                    case (".doc"):
                    case (".docx"):
                    case (".ppt"):
                    case (".xls"):
                        {
                            contentType = System.Net.Mime.MediaTypeNames.Text.RichText;
                            break;
                        }
                }

                System.IO.Packaging.PackagePart newFilePackagePart =
                       package.CreatePart(partURI,
                       contentType, CompressionOption.Normal);
                byte[] fileContent = System.IO.File.ReadAllBytes(physicalfilePath);

                newFilePackagePart.GetStream().Write(fileContent, 0, fileContent.Length);
            }
            else
            {
                // Add within the collection and use it to generate report.
            }
        }

    }



    public static class Extentions
    {
        public static void CopyTo(this Stream value, Stream target)
        {
            int bufSize = 1024;
            byte[] buf = new byte[bufSize];
            int size = 0;
            while ((size = value.Read(buf, 0, bufSize)) > 0)
                target.Write(buf, 0, size);
        }
    }
}
