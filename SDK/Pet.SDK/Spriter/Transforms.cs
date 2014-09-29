using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace FuncWorks.XNA.XSpriter
{
    public struct AnimationTransform
    {
        public Point Position;
        public Single Angle;
        public Point Scale;
        public Boolean Hidden;

        public AnimationTransform(Single angle, Point position, Point scale)
        {
            Angle = angle;
            Position = position;
            Scale = scale;
            Hidden = false;
        }
    }

    public struct RuntimeTransform
    {
        public Int32? TimelineId;
        public String BoneName;
        public Point Position;
        public Single Angle;
        public Point Scale;
        public Boolean Hidden;
    }

    public struct RenderedPosition
    {
        public Point Positon;
        public Point Pivot;
        public Single Angle;
        public Point Scale;
        //public SpriteEffects Effects;..
        public Single Layer;
    }
}
