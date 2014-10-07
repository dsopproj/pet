using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public class TimelineObject : TimelineObjectBase
    {
        public const String XML_NAME = "object";

        public const String FOLDER_ATTRIBUTE = "folder";
        public const String FILE_ATTRIBUTE = "file";
        public const String NAME_ATTRIBUTE = "name";
        public const String PIVOT_X_ATTRIBUTE = "pivot_x";
        public const String PIVOT_Y_ATTRIBUTE = "pivot_y";

        public const Single DEFAULT_PIVOT_X = 0f;
        public const Single DEFAULT_PIVOT_Y = 1f;


        public Int32 Folder { get; set; }
        public Int32 File { get; set; }
        public String Name { get; set; }

        public Single PivotX { get; set; }
        public Single PivotY { get; set; }


        public TimelineObject()
            : base()
        {
            PivotX = DEFAULT_PIVOT_X;
            PivotY = DEFAULT_PIVOT_Y;
        }
    }
}
