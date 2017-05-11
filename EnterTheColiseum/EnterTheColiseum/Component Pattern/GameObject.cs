using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class GameObject : Component
    {
        //Fields
        List<Component> components;
        string tag;

        ///Component fields
        Transform transform;

        //Properties
        public Transform Transform
        {
            get { return transform; }
        }
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        //Contrstuctor
        public GameObject(Vector2 position) : base()
        {
            transform = new Transform(this, position);
            components.Add(transform);
        }

        //Methods

    }
}
