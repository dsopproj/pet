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
            //if(time > 5*60*1000) do dance.
            if (player.PlayFinished())
            {
                player.Play(BrainActionEnum.Stand.ToString());
            }
        }
    }

    internal enum BrainActionEnum
    {
        Stand, Walk, Dance, Speak
    }
}
