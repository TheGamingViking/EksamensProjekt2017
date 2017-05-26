using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EnterTheColiseum
{
    class Tavern : Component, ILoadable, ISaveable
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
            //Load from database
        }
        private void Clicked()
        {
            GameObject returnButton = new GameObject(Vector2.Zero);
            returnButton.AddComponent(new SpriteRenderer(returnButton, "Exitknap", 0.05f, 1));
            returnButton.AddComponent(new Collider(returnButton, false, false));
            returnButton.AddComponent(new Button(returnButton, StructureType.Return));
            (returnButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (returnButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);
            GameObject menu = new GameObject(Vector2.Zero);
            menu.AddComponent(new Menu(menu, (Button)returnButton.GetComponent("Button")));

            GameWorld.Instance.AddGameObject(menu);
            GameWorld.Instance.AddGameObject(returnButton);
        }
        public void Save()
        {
            //Save to database
        }
    }
}
