using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Pet.SDK
{
    public interface IResource : IDisposable
    {
        bool IsDisposed();
    }

    public enum ResourceType
    {
        Image, Mp3, Spriter, Zip
    }

}
