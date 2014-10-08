using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pet.SDK
{
    public class Animation : IAnimation
    {
        //fields & propertys
        private BrashMonkey.Spriter.Models.Entity mEntity;
        private BrashMonkey.Spriter.Models.Animation mAnimation;
        private BrashMonkey.Spriter.Infrastructure.SpriterKeyList<BrashMonkey.Spriter.Models.Folder> folders;
        private long startTime;

        public int ID { get { return mAnimation.ID; } }

        public string Name { get { return mAnimation.Name; } }

        //ctor
        public Animation(BrashMonkey.Spriter.Models.Entity entity,
            BrashMonkey.Spriter.Models.Animation animation,
            BrashMonkey.Spriter.Infrastructure.SpriterKeyList<BrashMonkey.Spriter.Models.Folder> spriterKeyList)
        {
            this.mEntity = entity;
            this.mAnimation = animation;
            this.folders = spriterKeyList;
        }

        //interface
        public void Play(Body body)
        {
            float newTime = 0;
            if (mAnimation.Looping)
            {
                newTime = newTime % 360;
            }
            else
            {
                newTime = Math.Min(newTime, mAnimation.Length);
            }


            updateAnimation(newTime);
            //updateCharacter(mainlineKeyFromTime(newTime), newTime);
            
            body.Draw();

        }

        private void updateAnimation(float newTime)
        {
            System.Windows.Media.Imaging.CroppedBitmap aaa = null;

        }

        public void Stop()
        {
            throw new NotImplementedException();
        }


        public bool PlayFinished()
        {
            return true;
        }

        public string GetKey()
        {
            return Name;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }



    }
}
