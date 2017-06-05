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
    public class Colosseum : Component, ILoadable, ISaveable, IDrawable
    {
        //Fields
        Rectangle arenaBounds;
        Texture2D arenaBoundsTexture;

        //Component Fields
        Button button;

        //Properties
        public Rectangle ArenaBounds
        {
            get { return arenaBounds; }
        }

        //Constructor
        public Colosseum(GameObject gameObject, Button button) : base(gameObject)
        {
            arenaBounds = new Rectangle(220, 235, 860, 480);
            this.button = button;
        }

        //Method
        public void LoadContent(ContentManager content)
        {
            button.ColosseumClicked += Clicked;
            arenaBoundsTexture = content.Load<Texture2D>("CollisionTexture");
            //Load from database
        }
        private void Clicked()
        {
            GameObject returnButton = new GameObject(Vector2.Zero);
            returnButton.AddComponent(new SpriteRenderer(returnButton, "Exitknap", 0.6f, 1));
            returnButton.AddComponent(new Collider(returnButton, false, false));
            returnButton.AddComponent(new Button(returnButton, ButtonType.Return));
            (returnButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (returnButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameObject menu = new GameObject(Vector2.Zero);
            menu.AddComponent(new Menu(menu, (Button)returnButton.GetComponent("Button")));

            GameObject fight = new GameObject(new Vector2(1118, 621));
            fight.AddComponent(new SpriteRenderer(fight, "fitemebro", 0.6f, 1));
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
            foreach (GameObject obj in GameWorld.Instance.GameObjects)
            {
                if (obj.GetComponent("Collider") != null)
                {
                    (obj.GetComponent("Collider") as Collider).DoCollisionChecks = false;
                }
            }

            GameObject arena = new GameObject(Vector2.Zero);
            arena.AddComponent(new SpriteRenderer(arena, "EtC arena", 0.5f, 1));
            (arena.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);

            GameObject gladiator = new GameObject(new Vector2(290, 250));
            gladiator.AddComponent(new SpriteRenderer(gladiator, "EtC placeholder animation 2", 0.4f, 0.15f));
            gladiator.AddComponent(new Animator(gladiator));
            gladiator.AddComponent(new Collider(gladiator, false, false));
            gladiator.AddComponent(new Gladiator(gladiator, "KappaPride", true, this));
            (gladiator.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (gladiator.GetComponent("Gladiator") as Gladiator).LoadContent(GameWorld.Instance.Content);
            (gladiator.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameObject gladiator2 = new GameObject(new Vector2(950, 550));
            gladiator2.AddComponent(new SpriteRenderer(gladiator2, "EtC Animation v2", 0.4f, 0.15f));
            gladiator2.AddComponent(new Animator(gladiator2));
            gladiator2.AddComponent(new Collider(gladiator2, false, false));
            gladiator2.AddComponent(new Gladiator(gladiator2, "KappaPrude", true, this));
            (gladiator2.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (gladiator2.GetComponent("Gladiator") as Gladiator).LoadContent(GameWorld.Instance.Content);
            (gladiator2.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.AddGameObject(arena);
            GameWorld.Instance.AddGameObject(gladiator);
            GameWorld.Instance.AddGameObject(gladiator2);
            (gladiator.GetComponent("Gladiator") as Gladiator).SetEnemies((Gladiator)gladiator2.GetComponent("Gladiator"));
            (gladiator2.GetComponent("Gladiator") as Gladiator).SetEnemies((Gladiator)gladiator.GetComponent("Gladiator"));
            (gladiator.GetComponent("Gladiator") as Gladiator).StartFight();
            (gladiator2.GetComponent("Gladiator") as Gladiator).StartFight();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (GameWorld.Instance.InFight)
            {
                spriteBatch.Draw(arenaBoundsTexture, arenaBounds, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0.45f);
            }
        }
    }
}
