using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Idle : IStrategy
    {
        //Fields
        Animator animator;

        //Constructor
        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        //Methods
        public void Execute(ref Direction direction)
        {
            animator.PlayAnimation("TakeDamage");
        }
    }
}
