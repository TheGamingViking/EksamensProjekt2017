using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class GoTo : IStrategy
    {
        //Fields
        Transform transform;
        Animator animator;
        Vector2 goal;

        //Properties

        //Constructor
        public GoTo(Transform transform, Animator animator, Vector2 goal)
        {
            this.transform = transform;
            this.animator = animator;
            this.goal = goal;
        }

        //Methods
        public void Execute(ref Direction direction)
        {
            if (transform.Position != goal)
            {
                Vector2 translation = Vector2.Zero;

                if (transform.Position.X >= goal.X)
                {
                    translation += new Vector2(-1, 0);
                    direction = Direction.Left;
                }
                if (transform.Position.X <= goal.X)
                {
                    translation += new Vector2(1, 0);
                    direction = Direction.Right;
                }
                if (transform.Position.Y >= goal.Y)
                {
                    translation += new Vector2(0, -1);
                    direction = Direction.Back;
                }
                if (transform.Position.Y <= goal.Y)
                {
                    translation += new Vector2(0, 1);
                    direction = Direction.Front;
                }

                transform.Translate(translation * 100 * GameWorld.Instance.DeltaTime);
                animator.PlayAnimation("Walk");
            }
        }
    }
}
