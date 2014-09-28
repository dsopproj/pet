using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pet.Core
{
    /// <summary>
    /// Layer.xaml 的交互逻辑
    /// </summary>
    public partial class Layer : UserControl
    {
        public Dictionary<string, Player> playerDic = new Dictionary<string, Player>();

        public Layer()
        {
            InitializeComponent();
        }



        internal void initialize()
        {
        }


        internal void AddPlayer(Player player)
        {
            if (!playerDic.ContainsKey(player.Name))
            {
                SDK.Engin.Current.AddBody(player.Body);
                playerDic.Add(player.Name, player);
            }
        }

    }
}
