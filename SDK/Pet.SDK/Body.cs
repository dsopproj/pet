using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pet.SDK
{
    public class Body : Canvas, IDisposable, IBody
    {
        private Point _position;
        private Dictionary<string, IAnimation> animationDict = new Dictionary<string, IAnimation>();
        private IAnimation currentAnimation;

        public Action OnLoaded;
        public Action OnUnLoaded;
        private SpriterResource resource;


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

        public Body(SkinResource skinResource)
            : this()
        {
            InternalOnLoad();
        }

        public Body(SpriterResource resource)
            : this()
        {
            this.resource = resource;
            SetAnimations(resource.GetAnimations());
        }

        public void OnUpdated()
        {
        }

        public void Dispose()
        {
            if (OnUnLoaded != null)
                OnUnLoaded();
            //TODO; dispose resource.
        }

        public void Play(string key)
        {
            if (animationDict.ContainsKey(key))
            {
                currentAnimation = animationDict[key];
                currentAnimation.Play(this, resource.ScmlReference);
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

        public void SetAnimations(List<IAnimation> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    animationDict[item.GetKey()] = item;
                }
            }
        }

        internal void Draw()
        {

        }
    }
}
