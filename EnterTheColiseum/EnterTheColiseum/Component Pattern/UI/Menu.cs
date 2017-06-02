using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheColiseum
{
    class Menu : UI, ILoadable, IDrawable
    {
        //Fields
        List<GameObject> currentUIElements; 
        string spritePath = "Menu baggrund";
        float scale = 0.9f;
        Texture2D sprite;
        Rectangle rectangle;

        //Component Fields
        Button returnButton;
        Button exitButton;

        //Properties

        //Constructor
        public Menu(GameObject gameObject, Button returnButton) : base(gameObject)
        {
            currentUIElements = new List<GameObject>();

            GameObject.Transform.Position = new Vector2(61, 20);
            this.returnButton = returnButton;
            
            LoadContent(GameWorld.Instance.Content);
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spritePath);
            rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            returnButton.ReturnClicked += Clicked;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(sprite, position, rectangle, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.2f);
        }
        private void Clicked()
        {
            foreach (GameObject element in currentUIElements)
            {
                if (GameWorld.Instance.GameObjects.Contains(element))
                {
                    GameWorld.Instance.RemoveGameObject(element);
                }
            }
        }
        public void AddUIElement(GameObject element)
        {
            currentUIElements.Add(element);
        }
    }
}
