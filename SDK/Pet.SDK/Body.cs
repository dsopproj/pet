using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class Body : UserControl, IDisposable, IBody
    {
        private Point _position;
        private Dictionary<string, Animation> animationDict = new Dictionary<string, Animation>();
        private Animation currentAnimation;

        public Action OnLoaded;
        public Action OnUnLoaded;


        public Updater Updater { get; set; }

        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Canvas.SetLeft(this, Position.X);
                Canvas.SetTop(this, Position.Y);
            }
        }


        public Body()
        {
            Width = 64;
            Height = 128;
        }

        public Body(string skinPath, string brainPath)
            : this()
        {

        }

        public Body(SkinResource skinResource, SkeletonResource brainResource)
            : this()
        {

            InternalOnLoad();
        }

        public void OnUpdated()
        {
        }

        public void Dispose()
        {
            if (OnUnLoaded != null)
                OnUnLoaded();
        }

        public void Play(string key)
        {
            if (animationDict.ContainsKey(key))
            {
                currentAnimation = animationDict[key];
                currentAnimation.Play();
            }
        }




        private void InternalOnLoad()
        {
            if (OnLoaded != null)
                OnLoaded();
        }


        internal void InternalUpdate()
        {
            if (Updater != null)
                Updater.Update();
            OnUpdated();
        }


        public bool PlayFinished()
        {
            if (currentAnimation != null)
                return currentAnimation.PlayFinished();
            else
                return true;
        }
    }
}
