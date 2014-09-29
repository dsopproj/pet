using Pet.SDK;
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

namespace Pet
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private SDK.Engine engin;

        public MainWindow()
        {
            InitializeComponent();
            initialize();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        private void initialize()
        {
            engin = new Engine(new MainGame());
            LayoutRoot.Children.Add(engin.DrawingSurface);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            engin.StartGame();
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            engin.StopGame();
        }
    }
}
