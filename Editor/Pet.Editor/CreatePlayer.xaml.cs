using DragonScale.Portable.Zip;
using Microsoft.Win32;
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

namespace Pet.Editor
{
    /// <summary>
    /// CreatePlayer.xaml 的交互逻辑
    /// </summary>
    public partial class CreatePlayer : UserControl
    {
        public CreatePlayer()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.scml|*.scml";
            if (ofd.ShowDialog().HasValue && ofd.ShowDialog().Value)
            {
                var folder = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf(Path.DirectorySeparatorChar));
                var zipFilePath = folder.Substring(0, folder.LastIndexOf(Path.DirectorySeparatorChar)) + ofd.SafeFileName + ".zip";
                zipSpriter(folder, zipFilePath);
            }
        }


        private void zipSpriter(string folder, string zipFilePath)
        {
            var zipFile = ZipFile.Create();
            zipFile.AddDirectory(folder);
            var stream = zipFile.BaseStream;
            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            var file = File.Create(zipFilePath);
            byte[] bytes = new byte[2048];
            int i = 0;
            while ((i = stream.Read(bytes, 0, bytes.Length)) != -1)
            {
                file.Write(bytes, 0, i);
            }
            file.Flush();
            file.Close();
            MessageBox.Show("OK");
        }
    }
}
