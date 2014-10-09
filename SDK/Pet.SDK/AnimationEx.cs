using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public partial class Animation
    {
        int currentEntity;
        int currentAnimation;


        float currentTime = 0;


        SpatialInfo characterInfo()
        {
            // Fill a SpatialInfo class with the 
            // x,y,angle,etc of this character in game

            // To avoid distortion the character keep 
            // scaleX and scaleY values equal

            // Make scaleX or scaleY negative to flip on that axis

            // Examples (scaleX,scaleY)
            // (1,1) Normal size
            // (-2.5,2.5) 2.5x the normal size, and flipped on the x axis
            return new SpatialInfo();
        }



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
        BoneTimelineKey transformedBoneKeys[];
        for(b in mainKey.boneRefs[])
        {
            SpatialInfo parentInfo;
            var currentRef=mainKey.BoneReferences[b]; 
            if(currentRef.Parent>=0)
            {
                parentInfo=transformBoneKeys[currentRef.Parent].info;
            }
            else
            {
                parentInfo=characterInfo();
            }
            TimelineKey currentKey=keyFromRef(currentRef,newTime);
            currentKey.info=currentKey.info.unmapFromParent(parentInfo);
            transformBoneKeys.push(currentKey);
        }

        List<BrashMonkey.Spriter.Models.TimelineKey> objectKeys;
        for(int o=0; o<mainKey.ObjectReferences.Count;o++)
        {
            SpatialInfo parentInfo;
            var currentRef=mainKey.ObjectReferences[o];

            if(currentRef.Parent>=0)
            {
                parentInfo=transformBoneKeys[currentRef.Parent].info;
            }
            else
            {
                parentInfo=characterInfo();
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


        BrashMonkey.Spriter.Models.TimelineKey keyFromRef(BrashMonkey.Spriter.Models.MainlineObjectReference objref, int newTime)
        {
            BrashMonkey.Spriter.Models.Timeline timeline = mAnimation.Timelines[objref.Timeline];
            BrashMonkey.Spriter.Models.TimelineKey keyA = timeline.Keys[objref.Key];

            if (timeline.Keys.Count == 1)
            {
                return keyA;
            }

            int nextKeyIndex = objref.Key + 1;

            if (nextKeyIndex >= timeline.Keys.Count)
            {
                if (mAnimation.Looping)
                {
                    nextKeyIndex = 0;
                }
                else
                {
                    return keyA;
                }
            }

            BrashMonkey.Spriter.Models.TimelineKey keyB = timeline.Keys[nextKeyIndex];
            int keyBTime = keyB.Time;

            if (keyBTime < keyA.Time)
            {
                keyBTime = keyBTime + mAnimation.Length;
            }

            return keyA.interpolate(keyB, keyBTime, currentTime);
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
                float s = (float)Math.Sin(BrashMonkey.Spriter.MathHelper.ToRadians(parentInfo.angle));
                float c = (float)Math.Cos(BrashMonkey.Spriter.MathHelper.ToRadians(parentInfo.angle));
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
