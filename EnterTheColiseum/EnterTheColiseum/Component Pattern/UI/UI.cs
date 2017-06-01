using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EnterTheColiseum
{
    public class UI : Component, IUpdateable
    {
        //Fields
        protected MouseState mouseState;

        //Properties
        public Rectangle CollisionBox
        {
            get
            {
                return (GameObject.GetComponent("Collider") as Collider).CollisionBox;
            }
        }

        //Constructor
        public UI(GameObject gameObject) : base(gameObject)
        {

        }

        public virtual void Update()
        {
            mouseState = GameWorld.Instance.MouseState;
        }

        //Methods
    }
}
