using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public interface IAnimation : IDisposable
    {

        void Play(Body body, BrashMonkey.Spriter.Models.ScmlReference scmlReference);

        void Stop();

        bool PlayFinished();

        string GetKey();
    }
}
