using Pet.Core;
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

        private Layer layer;

        public MainWindow()
        {
            InitializeComponent();
            initialize();
        }

        private void initialize()
        {
            layer = new Layer();
            layer.initialize();
            LayoutRoot.Children.Add(layer);
            var players = Config.PlayerConfig.LoadPlayers();
            if (players.Count == 0)
            {
                MessageBox.Show("you can create your first player now");
            }
            else
            {
                foreach (var item in players)
                {
                    var player = Config.PlayerConfig.LoadPlayer(item);
                    layer.AddPlayer(player);
                }
            }
        }

    }
}
