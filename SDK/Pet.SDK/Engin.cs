using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class Engin
    {
        private DrawingSurface drawingSurface;
        private Game game;
        private List<Script> scripts = new List<Script>();
        private static Engin _current;
        private Timer timer = new Timer();
        private Action<DateTime> updateMethod;

        public static Engin Current
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

        public Engin(Game game)
        {
            if (_current != null)
                throw new Exception("Engin has initialized!");
            _current = this;
            this.game = game;
            this.game.Engin = this;
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

        public void AddScript(Script script)
        {
            if (!scripts.Contains(script))
            {
                scripts.Add(script);
            }
        }

        public void AddBody(Body body)
        {
            drawingSurface.Children.Add(body);
        }

        private void update(DateTime dateTime)
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                var script = (scripts[i] as Script);
                //script.Time = dateTime;
                script.Update();
            }
            drawingSurface.InvalidateVisual();
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
