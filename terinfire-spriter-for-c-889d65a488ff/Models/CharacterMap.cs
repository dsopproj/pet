using System;
using System.Collections.Generic;
using BrashMonkey.Spriter.Infrastructure;

namespace BrashMonkey.Spriter.Models
{
    public class CharacterMap : ISpriterKey
    {
        public const String XML_NAME = "character_map";

        public const String ID_ATTRIBUTE = "id";
        public const String NAME_ATTRIBUTE = "name";

        public Int32 ID { get; set; }
        public String Name { get; set; }

        public List<CharacterMapInstruction> Maps { get; set; }
    }
}
