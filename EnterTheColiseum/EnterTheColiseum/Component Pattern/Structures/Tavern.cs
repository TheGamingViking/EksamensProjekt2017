using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace EnterTheColiseum
{
    public class Tavern : Component, ILoadable, ISaveable
    {
        //Field
        
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
            returnButton.AddComponent(new SpriteRenderer(returnButton, "Exitknap", 0.6f, 1f));
            returnButton.AddComponent(new Collider(returnButton, false, false));
            returnButton.AddComponent(new Button(returnButton, ButtonType.Return));
            (returnButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (returnButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);
            GameObject menu = new GameObject(Vector2.Zero);
            menu.AddComponent(new Menu(menu, (Button)returnButton.GetComponent("Button")));

            GameObject exitButton = new GameObject(new Vector2(500, 500));
            exitButton.AddComponent(new SpriteRenderer(exitButton, "Exitknap", 0.06f, 1));
            exitButton.AddComponent(new Collider(exitButton, false, false));
            exitButton.AddComponent(new Button(exitButton, ButtonType.Exit));
            (exitButton.GetComponent("Button") as Button).ExitClicked += QuitGame;
            (exitButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (exitButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.AddGameObject(menu);
            GameWorld.Instance.AddGameObject(returnButton);
            GameWorld.Instance.AddGameObject(exitButton);
            (menu.GetComponent("Menu") as Menu).AddUIElement(menu);
            (menu.GetComponent("Menu") as Menu).AddUIElement(returnButton);
            (menu.GetComponent("Menu") as Menu).AddUIElement(exitButton);
        }
        public void Save()
        {
            //Save to database
        }
        public void QuitGame()
        {
            GameWorld.Instance.Exit();
        }
    }
}
