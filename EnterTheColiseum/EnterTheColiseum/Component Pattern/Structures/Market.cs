﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Market : Component, ILoadable, ISaveable
    {
        //Fields

        //Component Fields
        Button button;

        //Properties

        //Constructor
        public Market(GameObject gameObject, Button button) : base(gameObject)
        {
            this.button = button;
        }

        //Method
        public void LoadContent(ContentManager content)
        {
            button.MarketClicked += Clicked;
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

            GameObject buy = new GameObject(new Vector2(100, 100));
            buy.AddComponent(new SpriteRenderer(buy, "buybtn", 0.05f, 1));
            buy.AddComponent(new Collider(buy, false, false));
            buy.AddComponent(new Button(buy, ButtonType.Market));
            (buy.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (buy.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameObject sell = new GameObject(new Vector2(100, 200));
            sell.AddComponent(new SpriteRenderer(sell, "selbtn", 0.05f, 1));
            sell.AddComponent(new Collider(sell, false, false));
            sell.AddComponent(new Button(sell, ButtonType.Market));
            (sell.GetComponent("SpriteRenderer") as SpriteRenderer).LoadContent(GameWorld.Instance.Content);
            (sell.GetComponent("Collider") as Collider).LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.AddGameObject(menu);
            GameWorld.Instance.AddGameObject(returnButton);
            GameWorld.Instance.AddGameObject(buy);
            GameWorld.Instance.AddGameObject(sell);
            (menu.GetComponent("Menu") as Menu).AddUIElement(menu);
            (menu.GetComponent("Menu") as Menu).AddUIElement(returnButton);

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
    }
}
