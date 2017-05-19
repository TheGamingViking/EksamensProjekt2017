using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Reaver : Gladiator
    {
        //Fields
        float skillLevel;
        Thread thread;

        //Properties
        public float SkillLevel
        {
            get { return skillLevel; }
        }

        //Constructor
        public Reaver(GameObject gameObject, string name) : base(gameObject, name)
        {
        }

        //Methods

    }
}
