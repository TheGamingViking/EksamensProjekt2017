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

            }
        }
    }
}
