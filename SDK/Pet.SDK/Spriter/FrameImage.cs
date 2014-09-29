using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FuncWorks.XNA.XSpriter
{
    public class FrameImage
    {
        public Int32 TextureFolder;
        public Int32 TextureFile;
        public Point Position;
        public Point Pivot;
        public Single Angle;
        public Boolean Clockwise;
        public Point Scale;

        public Int32 TimelineId = -1;
        public Int32 Parent = -1;
    }
}
