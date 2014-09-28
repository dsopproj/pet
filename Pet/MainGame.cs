using Pet.Core;
using Pet.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Pet
{
    public class MainGame : Game
    {

        public override void Initialize()
        {
            base.Initialize();

            var layer = new Layer();
            layer.initialize();
            SDK.Engin.Current.DrawingSurface.Children.Add(layer);
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
