using Pet.Engin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pet.Core
{
    /// <summary>
    /// Layer.xaml 的交互逻辑
    /// </summary>
    public partial class Layer : UserControl
    {
        public Dictionary<string, Player> playerDic = new Dictionary<string, Player>();
        private Timer timer = new Timer();
        private ArrayList renders = ArrayList.Synchronized(new ArrayList());
        private Action<DateTime> updateMethod;

        public Layer()
        {
            InitializeComponent();
            timer.Interval = 1000 / 60;
            timer.Disposed += timer_Disposed;
            timer.Elapsed += timer_Elapsed;
            this.Loaded += Layer_Loaded;
            this.Unloaded += Layer_Unloaded;
            this.updateMethod = update;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(updateMethod, e.SignalTime);
        }
        private void update(DateTime dateTime)
        {
            for (int i = 0; i < renders.Count; i++)
            {
                (renders[i] as IRender).Render(this, dateTime);
            }
            this.UpdateLayout();
        }

        void timer_Disposed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void initialize()
        {
        }

        void Layer_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            timer.Start();
        }

        void Layer_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            timer.Stop();
        }


        internal void AddPlayer(Player player)
        {
            if (!playerDic.ContainsKey(player.Name))
            {
                RegisteRender(player);
                playerDic.Add(player.Name, player);
            }
            //throw new NotImplementedException();
        }

        internal void RegisteRender(IRender render)
        {
            if (render != null)
            {
                renders.Add(render);
                this.LayoutRoot.Children.Add(render.GetBody());
            }
        }
    }
}
