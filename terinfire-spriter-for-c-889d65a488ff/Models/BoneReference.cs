using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public class BoneReference
    {
        public const String XML_NAME = "bone_ref";

        public const String ID_ATTRIBUTE = "id";
        public const String KEY_ATTRIBUTE = "key";
        public const String TIMELINE_ATTRIBUTE = "timeline";
        public const String PARENT_ATTRIBUTE = "parent";

        public const Int32 DEFAULT_PARENT = -1;

        public Int32 ID { get; set; }
        public Int32 Key { get; set; }
        public Int32 Timeline { get; set; }
        public Int32 Parent { get; set; }

        public BoneReference()
        {
            Parent = DEFAULT_PARENT;
        }
    }
}
