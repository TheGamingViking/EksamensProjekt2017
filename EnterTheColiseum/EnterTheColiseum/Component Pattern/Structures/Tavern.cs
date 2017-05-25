using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EnterTheColiseum
{
    class Tavern : Component, ILoadable
    {
        //Fields

        
        //Component Fields
        Button button;

        //Properties

        //Constructor
        public Tavern(GameObject gameObject, Button button) : base(gameObject)
        {
            this.button = button;
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            button.TavernClicked += Clicked;
        }
        private void Clicked()
        {
            GameObject menu = new GameObject(Vector2.Zero);
            menu.AddComponent(new SpriteRenderer(menu, "kran", 0.1f, 1f));
            GameWorld.Instance.AddGameObject(menu);
        }
    }
}
