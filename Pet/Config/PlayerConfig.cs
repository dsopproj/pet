using Pet.Core;
using Pet.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pet.Config
{
    public class PlayerConfig
    {
        public string PlayerName { get; set; }

        public string PlayerResource { get; set; }

        public string PlayerBrain { get; set; }

        public string Manifest { get; set; }


        public static Player LoadPlayer(PlayerConfig config)
        {
            var body = new Engin.Body();
            var player = new Player(new System.Drawing.Point(300, 300), body) { Name = config.PlayerName };
            body.Content = new System.Windows.Controls.Grid() { Background = System.Windows.Media.Brushes.Red };
            return player;
        }

        internal static void SavePlayer(Player player)
        {
            throw new NotImplementedException();
        }

        internal static List<PlayerConfig> LoadPlayers()
        {
            string playerManifestsPath = StringTable.GetCharacterFolder();
            var dir = Directory.CreateDirectory(playerManifestsPath);
            List<PlayerConfig> list = new List<PlayerConfig>();
            foreach (var item in dir.GetFiles())
            {
                var config = new PlayerConfig();
                config.PlayerName = item.Name;
                config.Manifest = item.FullName;
                list.Add(config);
            }
            return list;
        }


    }
}
