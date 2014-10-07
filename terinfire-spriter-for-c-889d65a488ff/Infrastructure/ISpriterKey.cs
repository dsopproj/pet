using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Infrastructure
{
    public interface ISpriterKey
    {
        Int32 ID { get; set; }

        String Name { get; set; }
    }
}
