using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    public class Component
    {
        //Fields
        GameObject gameObject;

        //Properties
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        //Constructor
        public Component()
        {

        }
        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        //Methods
    }
}
