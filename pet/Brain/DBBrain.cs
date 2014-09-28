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
        private SDK.Body body;

        public DBBrain(SDK.Body body)
        {
            this.body = body;
        }

        public void Update()
        {
            if (body != null)
            {
                body.Position = new Point(body.Position.X + 1, body.Position.Y);
            }
        }
    }
}
