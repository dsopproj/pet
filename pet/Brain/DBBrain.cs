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
            if (body == null)
                throw new ArgumentNullException();
            this.body = body;
        }

        public void Update()
        {
            body.Position = new Point(body.Position.X + 1, body.Position.Y);
            if (body.PlayFinished())
            {
                body.Play(BrainActionEnum.Dance.ToString());
            }
        }
    }

    internal enum BrainActionEnum
    {
        Stand, Walk, Dance, Speak
    }
}
