using DragonScale.Portable.Zip;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
            //OpenFileDialog ofd = new OpenFileDialog();
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            
           fbd.ShowDialog();
           if (fbd.SelectedPath != string.Empty)
           {
               ZipEntry zip = new ZipEntry("GreyGuy");

               fbd.SelectedPath;  

           }
            
        }
    }
}
