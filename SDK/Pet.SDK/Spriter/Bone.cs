using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FuncWorks.XNA.XSpriter
{
    public class Bone
    {
        public Int32 Id;
        public Int32 Parent = -1;
        public String Name;
        public Point Position;
        public Point Scale;
        public Single Angle;
        public Int32 TimelineId = -1;
        public Boolean Clockwise;
    }
}
