using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public abstract class TimelineObjectBase
    {
        public const String X_ATTRIBUTE = "x";
        public const String Y_ATTRIBUTE = "y";
        public const String ANGLE_ATTRIBUTE = "angle";
        public const String SCALE_X_ATTRIBUTE = "scale_x";
        public const String SCALE_Y_ATTRIBUTE = "scale_y";
        public const String SPIN_ATTRIBUTE = "spin";
        public const String RED_MASK_ATTRIBUTE = "r";
        public const String GREEN_MASK_ATTRIBUTE = "g";
        public const String BLUE_MASK_ATTRIBUTE = "b";
        public const String ALPHA_MASK_ATTRIBUTE = "a";

        public const Single DEFAULT_X = 0f;
        public const Single DEFAULT_Y = 0f;
        public const Single DEFAULT_ANGLE = 0f;
        public const Single DEFAULT_SCALE_X = 1f;
        public const Single DEFAULT_SCALE_Y = 1f;
        public const Single DEFAULT_RED_MASK = 1f;
        public const Single DEFAULT_BLUE_MASK = 1f;
        public const Single DEFAULT_GREEN_MASK = 1f;
        public const Single DEFAULT_ALPHA_MASK = 1f;

        public Single X { get; set; }
        public Single Y { get; set; }
        public Single Angle { get; set; }
        public Single ScaleX { get; set; }
        public Single ScaleY { get; set; }
        public Single RedMask { get; set; }
        public Single GreenMask { get; set; }
        public Single BlueMask { get; set; }
        public Single AlphaMask { get; set; }

        public TimelineObjectBase()
        {
            X = DEFAULT_X;
            Y = DEFAULT_Y;
            Angle = DEFAULT_ANGLE;
            ScaleX = DEFAULT_SCALE_X;
            ScaleY = DEFAULT_SCALE_Y;
            
            RedMask = DEFAULT_RED_MASK;
            BlueMask = DEFAULT_BLUE_MASK;
            GreenMask = DEFAULT_GREEN_MASK;
            AlphaMask = DEFAULT_ALPHA_MASK;
        }
    }
}
