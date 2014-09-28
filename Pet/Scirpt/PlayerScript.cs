using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet.Scirpt
{
    internal class PlayerScript : SDK.Script
    {
        private Core.Player player;
        private SDK.Body body;

        public PlayerScript(Core.Player player)
        {
            this.player = player;
            this.body = this.player.Body;
        }
        public override void Update()
        {
            if (body != null)
            {
                body.Position = new Point(body.Position.X + 1, body.Position.Y);
                Canvas.SetLeft(body, Convert.ToDouble(body.Position.X));
                Canvas.SetTop(body, Convert.ToDouble(body.Position.Y));
            }
        }
    }
}
