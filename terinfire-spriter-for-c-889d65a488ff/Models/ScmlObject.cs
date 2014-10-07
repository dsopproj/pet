using System;
using System.Collections.Generic;
using BrashMonkey.Spriter.Infrastructure;

namespace BrashMonkey.Spriter.Models
{
    public class ScmlObject
    {
        public const String XML_NAME = "spriter_data";

        public SpriterKeyList<Folder> Folders { get; set; }
        public SpriterKeyList<Entity> Entities { get; set; }
    }
}
