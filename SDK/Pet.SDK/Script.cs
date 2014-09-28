using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public abstract class Script : Updater
    {
        private TimeSpan _time;
        public TimeSpan Time { get { return _time; } set { _time = value; } }

        private long _runTime;

        public long RunTime
        {
            get { return _runTime; }
            set { _runTime = value; }
        }


        public abstract void Update();
    }
}
