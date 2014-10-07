using System;
using System.Collections.Generic;
using BrashMonkey.Spriter.Infrastructure;

namespace BrashMonkey.Spriter.Models
{
    public class Animation : ISpriterKey
    {
        public const String XML_NAME = "animation";

        public const String ID_ATTRIBUTE = "id";
        public const String NAME_ATTRIBUTE = "name";
        public const String LENGTH_ATTRIBUTE = "length";
        public const String LOOPING_ATTRIBUTE = "looping";
        public const String LOOP_TO_ATTRIBUTE = "loop_to";

        public const Boolean DEFAULT_LOOPING = true;

        public Int32 ID { get; set; }
        public String Name { get; set; }

        public Int32 Length { get; set; }
        public Boolean Looping { get; set; }
        public Int32 LoopTo { get; set; }

        public Mainline MainlineKeys { get; set; }
        public SpriterKeyList<Timeline> Timelines { get; set; }

        public Animation()
        {
            Looping = DEFAULT_LOOPING;
        }
    }
}
