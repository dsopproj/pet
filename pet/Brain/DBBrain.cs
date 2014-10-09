using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet.Scirpt
{
    internal class DBBrain : SDK.Updater
    {
        private SDK.Body player;
        int lastSec;

        public DBBrain(SDK.Body body)
        {
            if (body == null)
                throw new ArgumentNullException();
            this.player = body;
        }

        public void Update()
        {
            //player.Position = new Point(player.Position.X + 1, player.Position.Y);
            var time = SDK.Game.Current.GameTime;
            if (lastSec != DateTime.Now.Second)
            {
                lastSec = DateTime.Now.Second;
            }
                if (player.PlayFinished())
                {
                    player.Play(BrainActionEnum.walk.ToString());
                }
        }
    }

    internal enum BrainActionEnum
    {
        stand_up, walk, idle, Speak
    }
}
