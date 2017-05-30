using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Attack : IStrategy
    {
        //Fields

        //Component Fields
        Animator animator;

        //Properties

        //Constructor
        public Attack(Animator animator)
        {
            this.animator = animator;
        }

        //Methods
        public void Execute(ref Direction direction)
        {
            //animator.PlayAnimation("Attack");
        }
    }
}
