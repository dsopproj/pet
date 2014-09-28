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



        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Body()
        {
           
        }

    }
}
