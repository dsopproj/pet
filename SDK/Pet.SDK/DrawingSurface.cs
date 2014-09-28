using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class DrawingSurface : Canvas
    {

        public DrawingSurface()
        {
            Button btn = new Button() { Content = "testbtn", Width = 100, Height = 30, };
            Children.Add(btn);
        }
    }
}
