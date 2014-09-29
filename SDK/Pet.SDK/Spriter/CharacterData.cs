using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncWorks.XNA.XSpriter
{
    public class CharacterData
    {
        public Image[][] Textures;
        public AnimationList Animations;
        public Int32 FramesPerSecond;

        public int? GetAnimationIdByName(string animationName)
        {
            for (int i = 0; i < Animations.Count; i++)
            {
                if (Animations[i].Name.Equals(animationName))
                {
                    return i;
                }
            }
            return null;
        }

        public CharacterAnimator GetCharacterAnimator()
        {
            return new CharacterAnimator(this);
        }
    }
}
