using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public partial class Animation
    {
        float currentTime = 0;
        void setCurrentTime(float newTime)
        {

            if (mAnimation.Looping)
            {
                newTime = newTime % 360;
            }
            else
            {
                newTime = Math.Min(newTime, mAnimation.Length);
            }

            updateCharacter(mainlineKeyFromTime(currentTime), currentTime);
        }

        void updateCharacter(BrashMonkey.Spriter.Models.MainlineKey mainKey, int newTime)
    {
        //BoneTimelineKey transformedBoneKeys[];
        //for(b in mainKey.boneRefs[])
        //{
        //    SpatialInfo parentInfo;
        //    Ref currentRef=mainKey.boneRefs[b]; 
        //    if(currentRef.parent>=0)
        //    {
        //        parentInfo=transformBoneKeys[currentRef.parent].info;
        //    }
        //    else
        //    {
        //        parentInfo=characterInfo;
        //    }

        //    TimelineKey currentKey=keyFromRef(currentRef,newTime);
        //    currentKey.info=currentKey.info.unmapFromParent(parentInfo);
        //    transformBoneKeys.push(currentKey);
        //}

        List<BrashMonkey.Spriter.Models.TimelineKey> objectKeys;
        for(int o=0; o<mainKey.ObjectReferences.Count;o++)
        {
            SpatialInfo parentInfo;
            Ref currentRef=mainKey.objRefs[b];

            if(currentRef.parent>=0)
            {
                parentInfo=transformBoneKeys[currentRef.parent].info;
            }
            else
            {
                parentInfo=characterInfo;
            }

            BrashMonkey.Spriter.Models.TimelineKey currentKey=keyFromRef(currentRef,newTime);
            currentKey.info=currentKey.info.unmapFromParent(parentInfo);
            objectKeys.Add(currentKey);
        }

        // <expose objectKeys to api users to retrieve AND replace objectKeys>

        for(k in objectKeys)
        {            
            objectKeys[k].paint();
        }
    }

        BrashMonkey.Spriter.Models.MainlineKey mainlineKeyFromTime(int time)
        {
            int currentMainKey = 0;
            for (int i = 0; i < mAnimation.MainlineKeys.Count; i++)
            {
                if (mAnimation.MainlineKeys[i].Time <= currentTime)
                {
                    currentMainKey = i;
                }
                if (mAnimation.MainlineKeys[i].Time >= currentTime)
                {
                    break;
                }
            }
            return mAnimation.MainlineKeys[currentMainKey];
        }


    }



    class SpatialInfo
    {
        float x = 0;
        float y = 0;
        float angle = 0;
        float scaleX = 1;
        float scaleY = 1;
        float a = 1;
        int spin = 1;

        SpatialInfo unmapFromParent(SpatialInfo parentInfo)
        {
            SpatialInfo unmappedObj = this;
            unmappedObj.angle += parentInfo.angle;
            unmappedObj.scaleX *= parentInfo.scaleX;
            unmappedObj.scaleY *= parentInfo.scaleY;
            unmappedObj.a *= parentInfo.a;

            if (x != 0 || y != 0)
            {
                var preMultX = x * parentInfo.scaleX;
                var preMultY = y * parentInfo.scaleY;
                float s = sin(Math.toRadians(parentInfo.angle));
                float c = cos(Math.toRadians(parentInfo.angle));
                unmappedObj.x = (preMultX * c) - (preMultY * s);
                unmappedObj.y = (preMultX * s) + (preMultY * c);
                unmappedObj.x += parentInfo.x;
                unmappedObj.y += parentInfo.y;
            }
            else
            {
                // Mandatory optimization for future features           
                unmappedObj.x = parentInfo.x;
                unmappedObj.y = parentInfo.y;
            }

            return unmappedObj;
        }
    }
}
