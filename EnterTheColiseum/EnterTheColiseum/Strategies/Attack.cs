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
        Gladiator gladiator;

        //Properties

        //Constructor
        public Attack(Animator animator, Gladiator gladiator)
        {
            this.animator = animator;
            this.gladiator = gladiator;
        }

        //Methods
        public void Execute(ref Direction direction)
        {
            animator.PlayAnimation("Attack");
            if (!gladiator.SoundIsPlaying)
            {
                if (gladiator.Rnd.Next(1, 3) == 1)
                {
                    GameWorld.Instance.SoundEffects[0].Play();
                }
                else
                {
                    GameWorld.Instance.SoundEffects[1].Play();
                }
                gladiator.SoundIsPlaying = true;
            }
        }
    }
}
