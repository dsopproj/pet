using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public abstract class Game
    {
        public static Game Current { get; protected set; }


        public Engine Engine { get; internal set; }



        public virtual void Initialize()
        {
            Current = this;

        }

        public virtual void Start()
        {
        }

        public virtual void Pause()
        {
            throw new NotImplementedException();
        }

        public virtual void Stop()
        {
            Console.WriteLine("game stop.");
        }

        public GameTime GameTime
        {
            get { return new GameTime(); }
        }

    }
}
