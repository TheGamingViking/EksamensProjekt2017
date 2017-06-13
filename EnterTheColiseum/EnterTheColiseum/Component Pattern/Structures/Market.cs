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
    class Market : Component, ILoadable, ISaveable, IDrawable
    {
        //Fields
        List<Gladiator> slavesToBuy;
        SpriteFont fonts;

        //Component Fields
        Button button;

        //Properties

        //Constructor
        public Market(GameObject gameObject, Button button) : base(gameObject)
        {
            slavesToBuy = new List<Gladiator>();

            this.button = button;
        }

        //Method
        public void LoadContent(ContentManager content)
        {
            button.MarketClicked += Clicked;
            fonts = content.Load<SpriteFont>("Fonts");
            //Generate slaves
            slavesToBuy.Add(new Gladiator(null, "Hans Gruber", 5, 7, 10));
            slavesToBuy.Add(new Gladiator(null, "Hans Gruber", 8, 3, 6));
            slavesToBuy.Add(new Gladiator(null, "Hans Gruber", 9, 1, 10));
            slavesToBuy.Add(new Gladiator(null, "Hans Gruber", 5, 10, 3));
            slavesToBuy.Add(new Gladiator(null, "Hans Gruber", 6, 6, 6));
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

            GameObject buy = new GameObject(new Vector2(1110, 120));
            buy.AddComponent(new SpriteRenderer(buy, "buybtn", SpriteData.UIElementDepth, 1));
            buy.AddComponent(new Collider(buy, false, false));
            buy.AddComponent(new Button(buy, ButtonType.Market));
            (buy.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (buy.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameObject sell = new GameObject(new Vector2(1110, 180));
            sell.AddComponent(new SpriteRenderer(sell, "selbtn", SpriteData.UIElementDepth, 1));
            sell.AddComponent(new Collider(sell, false, false));
            sell.AddComponent(new Button(sell, ButtonType.Market));
            (sell.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (sell.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            /*foreach (Gladiator slave in slavesToBuy)
            {
                GameObject gladiator = new GameObject(new Vector2(61, 60 + slavesToBuy.IndexOf(slave) * 108));
                gladiator.AddComponent(new SpriteRenderer(gladiator, "MenuItem", SpriteData.UIElementDepth, 1f));
                gladiator.AddComponent(new Collider(gladiator, false, false));
                gladiator.AddComponent(new Gladiator(gladiator, slave.Name, slave.Strength, slave.Agility, slave.Strategy));
                (gladiator.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
                (gladiator.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);
                GameWorld.Instance.AddGameObject(gladiator);
                (menu.GetComponent("Menu") as Menu).AddUIElement(gladiator);
            }*/

            GameWorld.Instance.AddGameObject(menu);
            GameWorld.Instance.AddGameObject(returnButton);
            GameWorld.Instance.AddGameObject(buy);
            GameWorld.Instance.AddGameObject(sell);
            (menu.GetComponent("Menu") as Menu).AddUIElement(menu);
            (menu.GetComponent("Menu") as Menu).AddUIElement(returnButton);
            (menu.GetComponent("Menu") as Menu).AddUIElement(buy);
            (menu.GetComponent("Menu") as Menu).AddUIElement(sell);

        }
        public void Save()
        {
            //Save to database
        }
        private void Buy()
        {

        }

        private void Sell()
        {

        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            /*if (GameWorld.Instance.InMenu)
            {
                spriteBatch.DrawString(fonts, "Name:", new Vector2(100, 40), Color.White);
                spriteBatch.DrawString(fonts, "Strength:", new Vector2(330, 40), Color.White);
                spriteBatch.DrawString(fonts, "Agility:", new Vector2(530, 40), Color.White);
                spriteBatch.DrawString(fonts, "Strategy:", new Vector2(730, 40), Color.White);
                foreach (Gladiator slave in slavesToBuy)
                {
                    spriteBatch.DrawString(fonts, slave.Name, new Vector2(100, 60 + slavesToBuy.IndexOf(slave) * 108), Color.White);
                    spriteBatch.DrawString(fonts, Convert.ToString(slave.Strength), new Vector2(330, 60 + slavesToBuy.IndexOf(slave) * 108), Color.White);
                    spriteBatch.DrawString(fonts, Convert.ToString(slave.Agility), new Vector2(530, 60 + slavesToBuy.IndexOf(slave) * 108), Color.White);
                    spriteBatch.DrawString(fonts, Convert.ToString(slave.Strategy), new Vector2(730, 60 + slavesToBuy.IndexOf(slave) * 108), Color.White);
                }
            }*/
        }
    }
}
