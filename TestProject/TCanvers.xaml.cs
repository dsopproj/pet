using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProject
{
    /// <summary>
    /// TCanvers.xaml 的交互逻辑
    /// </summary>
    public partial class TCanvers : Canvas
    {
        public TCanvers()
        {
            InitializeComponent();

            TransformGroup tg = new TransformGroup();
            RotateTransform rt = new RotateTransform();
            rt.Angle = 60;
            tg.Children.Add(rt);

            btnTest.RenderTransform = tg;

        }
    }
}
