using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.Utils
{
    class StringTable
    {
        public static readonly string CharacterFolder = "Character";
        public static readonly string ResourcesFolder = "Resources";
        public static readonly string BrainFolder = "Brain";

        public static string GetCharacterFolder()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + CharacterFolder;
        }
    }
}
