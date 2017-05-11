using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Transform : Component
    {
        //Fields
        Vector2 position;

        //Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        //Constructor
        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.position = position;
        }

        //Methods
        public void Translate(Vector2 translation)
        {
            position += translation;
        }
    }
}
