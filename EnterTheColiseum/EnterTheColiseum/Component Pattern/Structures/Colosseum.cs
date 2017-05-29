using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Colosseum : Component, ILoadable, ISaveable
    {
        //Fields

        //Component Fields
        Button button;

        //Properties

        //Constructor
        public Colosseum(GameObject gameObject, Button button) : base(gameObject)
        {
            this.button = button;
        }

        //Method
        public void LoadContent(ContentManager content)
        {
            button.ColosseumClicked += Clicked;
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

            GameObject fight = new GameObject(new Vector2(1118, 621));
            fight.AddComponent(new SpriteRenderer(fight, "fitemebro", 0.05f, 1));
            fight.AddComponent(new Collider(fight, false, false));
            fight.AddComponent(new Button(fight, ButtonType.Fight));
            (fight.GetComponent("Button") as Button).FightClicked += StartFight;
            (fight.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (fight.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.AddGameObject(menu);
            GameWorld.Instance.AddGameObject(returnButton);
            GameWorld.Instance.AddGameObject(fight);
            (menu.GetComponent("Menu") as Menu).AddUIElement(menu);
            (menu.GetComponent("Menu") as Menu).AddUIElement(returnButton);
            (menu.GetComponent("Menu") as Menu).AddUIElement(fight);
        }
        public void Save()
        {
            //Save to database
        }
        private void StartFight()
        {
            GameObject arena = new GameObject(Vector2.Zero);
            arena.AddComponent(new SpriteRenderer(arena, "EtC arena", 0.03f, 1));
            (arena.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);

            GameObject gladiator = new GameObject(new Vector2(80, 350));
            gladiator.AddComponent(new SpriteRenderer(gladiator, "EtC placeholder animation", 0.01f, 0.1f));
            gladiator.AddComponent(new Animator(gladiator));
            gladiator.AddComponent(new Collider(gladiator, true, false));
            gladiator.AddComponent(new Gladiator(gladiator, "KappaPride"));
            (gladiator.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (gladiator.GetComponent("Gladiator") as Gladiator).LoadContent(GameWorld.Instance.Content);
            (gladiator.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameObject gladiator2 = new GameObject(new Vector2(1080, 350));
            gladiator2.AddComponent(new SpriteRenderer(gladiator2, "EtC placeholder animation", 0.01f, 0.1f));
            gladiator2.AddComponent(new Animator(gladiator2));
            gladiator2.AddComponent(new Collider(gladiator2, true, false));
            gladiator2.AddComponent(new Gladiator(gladiator2, "KappaPrude"));
            (gladiator2.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (gladiator2.GetComponent("Gladiator") as Gladiator).LoadContent(GameWorld.Instance.Content);
            (gladiator2.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.AddGameObject(gladiator);
            GameWorld.Instance.AddGameObject(gladiator2);
            GameWorld.Instance.AddGameObject(arena);
        }
    }
}
