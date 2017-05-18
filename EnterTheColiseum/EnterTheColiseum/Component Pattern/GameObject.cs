using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    public class GameObject : Component
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
            components = new List<Component>();
            transform = new Transform(this, position);
            components.Add(transform);
        }

        //Methods
        public void AddComponent(Component component)
        {
            components.Add(component);
        }
        public Component GetComponent(string component)
        {
            foreach (Component c in components)
            {
                if (c.GetType().Name == component)
                {
                    return c;
                }
            }
            return null;
        }

        //Component Loops
        public void LoadContent(ContentManager content)
        {
            foreach (Component component in components)
            {
                if (component is ILoadable)
                {
                    (component as ILoadable).LoadContent(content);
                }
            }
        }
        public void Update()
        {
            foreach (Component component in components)
            {
                if (component is IUpdateable)
                {
                    (component as IUpdateable).Update();
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is IDrawable)
                {
                    (component as IDrawable).Draw(spriteBatch, transform.Position);
                }
            }
        }
        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimateable)
                {
                    (component as IAnimateable).OnAnimationDone(animationName);
                }
            }
        }
        public void OnCollisionStay(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollisionStay)
                {
                    (component as ICollisionStay).OnCollisionStay(other);
                }
            }
        }
        public void OnCollisionEnter(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollisionEnter)
                {
                    (component as ICollisionEnter).OnCollisionEnter(other);
                }
            }
        }
        public void OnCollisionExit(Collider other)
        {
            foreach (Component component in components)
            {
                if (component is ICollisionExit)
                {
                    (component as ICollisionExit).OnCollisionExit(other);
                }
            }
        }
    }
}
