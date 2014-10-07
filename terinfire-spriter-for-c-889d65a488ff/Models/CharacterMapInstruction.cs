using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public class CharacterMapInstruction
    {
        public const String XML_NAME = "map";

        public const String FOLDER_ATTRIBUTE = "folder";
        public const String FILE_ATTRIBUTE = "file";
        public const String TARGET_FOLDER_ATTRIBUTE = "target_folder";
        public const String TARGET_FILE_ATTRIBUTE = "target_file";

        public const Int32 DEFAULT_TARGET_FOLDER = -1;
        public const Int32 DEFAULT_TARGET_FILE = -1;

        public Int32 Folder { get; set; }
        public Int32 File { get; set; }
        public Int32 TargetFolder { get; set; }
        public Int32 TargetFile { get; set; }

        public CharacterMapInstruction()
        {
            TargetFolder = DEFAULT_TARGET_FOLDER;
            TargetFile = DEFAULT_TARGET_FILE;
        }
    }
}
