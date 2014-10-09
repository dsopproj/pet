using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrashMonkey.Spriter.Models
{
    public partial class TimelineKey
    {
        public const String XML_NAME = "key";

        public const String ID_ATTRIBUTE = "id";
        public const String TIME_ATTRIBUTE = "time";
        public const String C1_ATTRIBUTE = "c1";
        public const String C2_ATTRIBUTE = "c2";
        public const String SPIN_ATTRIBUTE = "spin";

        public Int32 DEFAULT_SPIN = 1;

        public Int32 ID { get; set; }
        public Int32 Time { get; set; }
        public Single C1 { get; set; }
        public Single C2 { get; set; }
        public Int32 Spin { get; set; }

        public TimelineObject Object { get; set; }
        public TimelineBone Bone { get; set; }

        public TimelineKey()
        {
            Spin = DEFAULT_SPIN;
        }








        int curveType = 1; // enum : 0 INSTANT, 1 LINEAR, 2 QUADRATIC, 3 CUBIC // curve_type="0"
        public TimelineKey interpolate(TimelineKey nextKey, int nextKeyTime, float currentTime)
        {
            return linear(nextKey, getTWithNextKey(nextKey, nextKeyTime, currentTime));
        }

        float getTWithNextKey(TimelineKey nextKey, int nextKeyTime, float currentTime)
        {
            if (curveType == 0 || this.Time == nextKey.Time)
            {
                return 0;
            }

            float t = (currentTime - Time) / (nextKey.Time - Time);

            if (curveType == 1)
            {
                return t;
            }
            else if (curveType == 2)
            {
                return (MathHelper.Quadratic(0.0f, C1, 1.0f, t));
            }
            else if (curveType == 3)
            {
                return (MathHelper.Cubic(0.0f, C1, C2, 1.0f, t));
            }

            return 0; // Runtime should never reach here        
        }

        TimelineKey linear(TimelineKey keyB, float t)
        {
            // overridden in inherited types  return linear(this,keyB,t);
            //TODO, to inherited.
            return null;
        }
         
    }
}
