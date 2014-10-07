using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public class MainlineKey
    {
        public const String XML_NAME = "key";

        public const String ID_ATTRIBUTE = "id";
        public const String TIME_ATTRIBUTE = "time";

        public const Int32 DEFAULT_TIME = 0;

        public Int32 ID { get; set; }
        public Int32 Time { get; set; }

        public List<MainlineObjectReference> ObjectReferences { get; set; }
        public List<BoneReference> BoneReferences { get; set; }

        public MainlineKey()
        {
            Time = DEFAULT_TIME;
        }
    }
}
