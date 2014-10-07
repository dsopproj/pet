using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public class TimelineKey
    {
        public const String XML_NAME = "key";

        public const String ID_ATTRIBUTE = "id";
        public const String TIME_ATTRIBUTE = "time";
        public const String C1_ATTRIBUTE = "c1";
        public const String C2_ATTRIBUTE = "c2";
        public const String SPIN_ATTRIBUTE = "spin";

        public Int32 DEFAULT_SPIN = 1;
        
        public Int32 ID { get; set; }
        public Int32 Time { get; set; }
        public Single C1 { get; set; }
        public Single C2 { get; set; }
        public Int32 Spin { get; set; }

        public TimelineObject Object { get; set; }
        public TimelineBone Bone { get; set; }

        public TimelineKey()
        {
            Spin = DEFAULT_SPIN;
        }
    }
}
