using Pet.SDK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet
{
    public class MainGame : Game
    {

        public override void Initialize()
        {
            base.Initialize();

            var layer = new Canvas();//word layout.
            SDK.Engine.Current.DrawingSurface.Children.Add(layer);
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
                    player.Position = new System.Windows.Point(200, 200);
                    layer.Children.Add(player);
                }
            }
        }
    }
}
