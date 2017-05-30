using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Upgrade : Component, ILoadable, ISaveable
    {
        //Fields

        //Component Fields
        Button button;

        //Properties

        //Constructor
        public Upgrade(GameObject gameObject, Button button) : base(gameObject)
            {
            this.button = button;
        }

        //Method
        public void LoadContent(ContentManager content)
        {
            button.UpgradeClicked += Clicked;
            //Load from database
        }
        private void Clicked()
        {
            GameObject returnButton = new GameObject(Vector2.Zero);
            returnButton.AddComponent(new SpriteRenderer(returnButton, "Exitknap", 0.05f, 1));
            returnButton.AddComponent(new Collider(returnButton, false, false));
            returnButton.AddComponent(new Button(returnButton, ButtonType.Return));
            (returnButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (returnButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);
            GameObject menu = new GameObject(Vector2.Zero);
            menu.AddComponent(new Menu(menu, (Button)returnButton.GetComponent("Button")));

            GameObject lvl2Sprite = new GameObject(new Vector2(100, 300));
            lvl2Sprite.AddComponent(new SpriteRenderer(lvl2Sprite, "Upgradebtn", 0.05f, 1));
            (lvl2Sprite.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);

            GameObject upgradeButton = new GameObject(new Vector2(100,300));
            upgradeButton.AddComponent(new SpriteRenderer(upgradeButton, "EtC arena v2", 0.05f, 1));
            upgradeButton.AddComponent(new Collider(upgradeButton, false, false));
            upgradeButton.AddComponent(new Button(upgradeButton, ButtonType.Upgrade));

            (upgradeButton.GetComponent("Button") as Button).UpgradeClicked += LevelUp;
            (upgradeButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (upgradeButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.AddGameObject(menu);
            GameWorld.Instance.AddGameObject(returnButton);
            GameWorld.Instance.AddGameObject(upgradeButton);
            GameWorld.Instance.AddGameObject(lvl2Sprite);
            (menu.GetComponent("Menu") as Menu).AddUIElement(menu);
            (menu.GetComponent("Menu") as Menu).AddUIElement(returnButton);
            (menu.GetComponent("Menu") as Menu).AddUIElement(lvl2Sprite);
            (menu.GetComponent("Menu") as Menu).AddUIElement(upgradeButton);
        }
        public void Save()
        {
            //Save to database
        }
        private void LevelUp()
        {
            
        }
    }
}
