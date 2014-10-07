using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public class Animation
    {
        private readonly List<string> _sprite = new List<string>();
        public string Name { get; set; }

        public List<string> sprite { get { return _sprite; } }



        public void Play()
        {
        }

        internal bool PlayFinished()
        {
            return true;
        }
    }
}
