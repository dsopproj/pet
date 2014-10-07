using System;
using System.Collections.Generic;
using BrashMonkey.Spriter.Infrastructure;

namespace BrashMonkey.Spriter.Models
{
    public class Folder : ISpriterKey
    {
        public const String XML_NAME = "folder";

        public const String ID_ATTRIBUTE = "id";
        public const String NAME_ATTRIBUTE = "name";

        public Int32 ID { get; set; }
        public String Name { get; set; }

        public SpriterKeyList<File> Files { get; set; }
    }
}
