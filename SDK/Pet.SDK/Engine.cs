using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class Engine
    {
        private DrawingSurface drawingSurface;
        private Game game;
        private static Engine _current;
        private Timer timer = new Timer();
        private Action<DateTime> updateMethod;

        public static Engine Current
        {
            get
            {
                if (_current == null)
                    throw new Exception("Engin do not initialize!");
                return _current;
            }
        }


        public DrawingSurface DrawingSurface
        {
            get
            {
                return drawingSurface;
            }
        }

        public Engine(Game game)
        {
            if (_current != null)
                throw new Exception("Engin has initialized!");
            _current = this;
            this.game = game;
            this.game.Engine = this;
            drawingSurface = new DrawingSurface();
            timer.Interval = 1000 / 60;
            timer.Disposed += timer_Disposed;
            timer.Elapsed += timer_Elapsed;

            game.Initialize();
            updateMethod = update;

        }

        public void StartGame()
        {
            game.Start();
            timer.Start();
            //game.Stop();
        }

        public void Pause()
        {
            game.Pause();
            timer.Stop();
        }

        public void StopGame()
        {
            game.Stop();
        }

        public void AddBody(Body body)
        {
            drawingSurface.Children.Add(body);
        }

        private void update(DateTime dateTime)
        {
            updateBody(drawingSurface.Children);
            drawingSurface.InvalidateVisual();
        }

        private void updateBody(UIElementCollection value)
        {
            foreach (var item in value)
                if (item is Body)
                    (item as Body).InternalUpdate();
                else if (item is Panel)
                    updateBody((item as Panel).Children);
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            drawingSurface.Dispatcher.BeginInvoke(updateMethod, e.SignalTime);
        }

        private void timer_Disposed(object sender, EventArgs e)
        {
        }

    }
}
