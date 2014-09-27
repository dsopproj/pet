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

namespace Pet.Importer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImportSpriter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("find spriter`s resources. zip res to direct folder.");
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("show create user page, select actions, and character.");
        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("save user resource to Pet Folder.");
        }
    }
}
