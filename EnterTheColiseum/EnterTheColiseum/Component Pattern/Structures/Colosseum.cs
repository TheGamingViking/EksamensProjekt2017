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
        GameObject arena;
        Rectangle arenaBounds;
        Texture2D arenaBoundsTexture;
        SpriteFont fonts;
        List<Gladiator> gladiatorsInFight;

        //Component Fields
        Button button;

        //Properties
        public Rectangle ArenaBounds
        {
            get { return arenaBounds; }
        }
        public List<Gladiator> GladiatorsInFight
        {
            get { return gladiatorsInFight; }
        }

        //Constructor
        public Colosseum(GameObject gameObject, Button button) : base(gameObject)
        {
            arenaBounds = new Rectangle(220, 235, 860, 480);
            gladiatorsInFight = new List<Gladiator>();
            this.button = button;
        }

        //Method
        public void LoadContent(ContentManager content)
        {
            button.ColosseumClicked += Clicked;
            arenaBoundsTexture = content.Load<Texture2D>("CollisionTexture");
            fonts = content.Load<SpriteFont>("Fonts");
            //Load from database
        }
        private void Clicked()
        {
            GameObject returnButton = new GameObject(Vector2.Zero);
            returnButton.AddComponent(new SpriteRenderer(returnButton, "Exitknap", SpriteData.UIElementDepth, 1));
            returnButton.AddComponent(new Collider(returnButton, false, false));
            returnButton.AddComponent(new Button(returnButton, ButtonType.Return));
            (returnButton.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (returnButton.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameObject menu = new GameObject(Vector2.Zero);
            menu.AddComponent(new Menu(menu, (Button)returnButton.GetComponent("Button")));

            GameObject fight = new GameObject(new Vector2(1118, 621));
            fight.AddComponent(new SpriteRenderer(fight, "fitemebro", SpriteData.UIElementDepth, 1));
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
            /*foreach (GameObject obj in GameWorld.Instance.GameObjects)
            {
                if (obj.GetComponent("Collider") != null)
                {
                    (obj.GetComponent("Collider") as Collider).DoCollisionChecks = false;
                }
            }*/

            arena = new GameObject(Vector2.Zero);
            arena.AddComponent(new SpriteRenderer(arena, "EtC arena", SpriteData.MiddlegroundDepth, 1));
            (arena.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);

            GameObject gladiator = new GameObject(new Vector2(290, 250));
            gladiator.AddComponent(new SpriteRenderer(gladiator, "EtC placeholder animation 2", SpriteData.GladiatorDepth, SpriteData.GladiatorScale));
            gladiator.AddComponent(new Animator(gladiator));
            gladiator.AddComponent(new Collider(gladiator, false, false));
            gladiator.AddComponent(new Gladiator(gladiator, "Ains Ooal Gown", this));
            gladiator.Tag = "DoNotUpdate";
            (gladiator.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (gladiator.GetComponent("Gladiator") as Gladiator).LoadContent(GameWorld.Instance.Content);
            (gladiator.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);
            gladiatorsInFight.Add((Gladiator)gladiator.GetComponent("Gladiator"));

            GameObject gladiator2 = new GameObject(new Vector2(950, 550));
            gladiator2.AddComponent(new SpriteRenderer(gladiator2, "EtC Animation v2", SpriteData.GladiatorDepth, SpriteData.GladiatorScale));
            gladiator2.AddComponent(new Animator(gladiator2));
            gladiator2.AddComponent(new Collider(gladiator2, false, false));
            gladiator2.AddComponent(new Gladiator(gladiator2, "Kappa Pride", this));
            gladiator2.Tag = "DoNotUpdate";
            (gladiator2.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (gladiator2.GetComponent("Gladiator") as Gladiator).LoadContent(GameWorld.Instance.Content);
            (gladiator2.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);
            gladiatorsInFight.Add((Gladiator)gladiator2.GetComponent("Gladiator"));

            GameWorld.Instance.AddGameObject(arena);
            GameWorld.Instance.AddGameObject(gladiator);
            GameWorld.Instance.AddGameObject(gladiator2);
            (gladiator.GetComponent("Gladiator") as Gladiator).SetEnemies((Gladiator)gladiator2.GetComponent("Gladiator"));
            (gladiator2.GetComponent("Gladiator") as Gladiator).SetEnemies((Gladiator)gladiator.GetComponent("Gladiator"));
            GameWorld.Instance.FightStart(gladiatorsInFight);
            (gladiator.GetComponent("Gladiator") as Gladiator).StartFight();
            (gladiator2.GetComponent("Gladiator") as Gladiator).StartFight();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (GameWorld.Instance.InFight)
            {
                spriteBatch.Draw(arenaBoundsTexture, arenaBounds, null, Color.Transparent, 0, Vector2.Zero, SpriteEffects.None, 0.45f);
                spriteBatch.DrawString(fonts, $"{gladiatorsInFight[0].Name}: {gladiatorsInFight[0].Health}", new Vector2(15, 10), Color.White);
                spriteBatch.DrawString(fonts, $"{gladiatorsInFight[1].Name}: {gladiatorsInFight[1].Health}", new Vector2(15, 40), Color.White);
            }
        }
        public void ResetArena()
        {
            gladiatorsInFight.Clear();
            GameWorld.Instance.RemoveGameObject(arena);
        }
    }
}
