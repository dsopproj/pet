using System;
using BrashMonkey.Spriter.Infrastructure;

namespace BrashMonkey.Spriter.Models
{
    public class File : ISpriterKey
    {
        public const String XML_NAME = "file";

        public const String ID_ATTRIBUTE = "id";
        public const String NAME_ATTRIBUTE = "name";
        public const String WIDTH_ATTRIBUTE = "width";
        public const String HEIGHT_ATTRIBUTE = "height";
        public const String PIVOT_X_ATTRIBUTE = "pivot_x";
        public const String PIVOT_Y_ATTRIBUTE = "pivot_y";

        public const Single DEFAULT_PIVOT_X = 0f;
        public const Single DEFAULT_PIVOT_Y = 1f;

        public Int32 ID { get; set; }
        public String Name { get; set; }

        public Int32 Width { get; set; }
        public Int32 Height { get; set; }

        public Single PivotX { get; set; }
        public Single PivotY { get; set; }

        public File()
        {
            PivotX = DEFAULT_PIVOT_X;
            PivotY = DEFAULT_PIVOT_Y;
        }
    }
}
