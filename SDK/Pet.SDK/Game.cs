using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public abstract class Game
    {

        public Engine Engine { get; internal set; }



        public virtual void Initialize()
        {
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

    }
}
