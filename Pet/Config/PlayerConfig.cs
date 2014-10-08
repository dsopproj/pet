using BrashMonkey.Spriter.Models;
using Pet.Scirpt;
using Pet.SDK;
using Pet.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Pet.Config
{
    public class PlayerConfig
    {
        public string PlayerName { get; set; }

        public string PlayerResource { get; set; }

        public string PlayerBrain { get; set; }

        public string Manifest { get; set; }


        public static Body LoadPlayer(PlayerConfig config)
        {
            //load zip, resource. then create player.
            ContentManager cm = Engine.Current.ContentManager;
            SpriterResource resource = (SpriterResource)cm.LoadResource(config.PlayerResource, ResourceType.Spriter);

            var body = new SDK.Body(resource);
            body.Updater = new DBBrain(body);
            body.Children.Add(new System.Windows.Controls.Grid() { Background = System.Windows.Media.Brushes.Red });

            return body;
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
