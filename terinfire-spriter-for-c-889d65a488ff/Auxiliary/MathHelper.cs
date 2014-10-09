using System;

namespace BrashMonkey.Spriter
{
    public static class MathHelper
    {
        public static Single Linear(Single pA, Single pB, Single pPercentComplete)
        {
            return ((pB - pA) * pPercentComplete) + pA;
        }

        //spin off; on t=15, tk=1 tk2=2, spin is 1.... HOW?!
        public static Single AngleLinear(Single pStartAngle, Single pEndAngle, Single pPercentComplete, Int32 pSpin)
        {
            if (pSpin == 0)
            {
                return pStartAngle;
            }
            if (pSpin > 0)
            {
                if ((pEndAngle - pStartAngle) < 0)
                {
                    pEndAngle += 360;
                }
            }
            else if (pSpin < 0)
            {
                if ((pEndAngle - pStartAngle) > 0)
                {
                    pEndAngle -= 360;
                }
            }

            return Linear(pStartAngle, pEndAngle, pPercentComplete);
        }

        public static Single Quadratic(Single pA, Single pB, Single pC, Single pPercentComplete)
        {
            return Linear(Linear(pA, pB, pPercentComplete), Linear(pB, pC, pPercentComplete), pPercentComplete);
        }

        public static Single Cubic(Single pA, Single pB, Single pC, Single pD, Single pPercentComplete)
        {
            return Linear(Quadratic(pA, pB, pC, pPercentComplete), Quadratic(pB, pC, pD, pPercentComplete), pPercentComplete);
        }

        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }


    }
}
