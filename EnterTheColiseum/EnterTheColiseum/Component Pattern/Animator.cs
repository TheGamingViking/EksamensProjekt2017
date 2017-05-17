using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Animator : Component, IUpdateable
    {
        //Fields
        Dictionary<string, Animation> animations;
        string animationName;
        int currentIndex;
        Rectangle[] rectangles;
        float timeElapsed;
        float fps;

        ///Component fields
        SpriteRenderer spriteRenderer;

        //Properties
        public Dictionary<string, Animation> Animations
        {
            get { return animations; }
        }
        public string AnimationName
        {
            get { return animationName; }
        }
        public int CurrentIndex
        {
            get { return currentIndex; }
        }

        //Constructor
        public Animator(GameObject gameObject) : base(gameObject)
        {
            animations = new Dictionary<string, Animation>();

            fps = 5;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
        }

        //Methods
        public void Update()
        {
            timeElapsed += GameWorld.Instance.DeltaTime;
            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                GameObject.OnAnimationDone(animationName);
                timeElapsed = 0;
                currentIndex = 0;
            }

            spriteRenderer.Rectangle = rectangles[currentIndex];
        }
        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }
        public void PlayAnimation(string animationName)
        {
            if (this.animationName != animationName)
            {
                rectangles = animations[animationName].Rectangles;

                spriteRenderer.Rectangle = rectangles[0];

                spriteRenderer.Offset = animations[animationName].Offset;

                this.animationName = animationName;

                fps = animations[animationName].Fps;

                timeElapsed = 0;

                currentIndex = 0;
            }
        }
    }
}
