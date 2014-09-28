using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class Body : UserControl
    {
        private Point _position;

        public Updater Updater { get; set; }


        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Canvas.SetLeft(this, Position.X);
                Canvas.SetTop(this, Position.Y);
            }
        }

        public Body()
        {
            Width = 64;
            Height = 128;
        }

        public void OnUpdated()
        {
        }

        internal void InternalUpdate()
        {
            if (Updater != null)
                Updater.Update();
            OnUpdated();
        }

    }
}
