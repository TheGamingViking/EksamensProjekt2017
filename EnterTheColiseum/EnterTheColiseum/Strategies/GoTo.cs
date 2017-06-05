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
        Vector2 goal;
        float speed;

        //Component Fields
        Transform transform;
        Animator animator;
        Colosseum arena;

        //Properties

        //Constructor
        public GoTo(Transform transform, Animator animator, Vector2 goal, Colosseum arena, float speed)
        {
            this.transform = transform;
            this.animator = animator;
            this.goal = goal;
            this.arena = arena;
            this.speed = speed;
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

                if ((transform.GameObject.GetComponent("Collider") as Collider).CollisionBox.Bottom + translation.Y > arena.ArenaBounds.Bottom ||
               (transform.GameObject.GetComponent("Collider") as Collider).CollisionBox.Top + translation.Y < arena.ArenaBounds.Top ||
               (transform.GameObject.GetComponent("Collider") as Collider).CollisionBox.Right + translation.X > arena.ArenaBounds.Right ||
               (transform.GameObject.GetComponent("Collider") as Collider).CollisionBox.Left + translation.X < arena.ArenaBounds.Left)
                {
                    Vector2 inversion = Vector2.Multiply(translation, 2);
                    translation = Vector2.Negate(inversion);
                }

                transform.Translate(translation * speed * GameWorld.Instance.DeltaTime);
                animator.PlayAnimation("Walk");
            }
        }
    }
}
