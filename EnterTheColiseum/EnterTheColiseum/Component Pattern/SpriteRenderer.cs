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
    public class SpriteRenderer : Component, ILoadable, IDrawable
    {
        //Fields
        Texture2D sprite;
        Rectangle rectangle;
        Color color;
        Vector2 offset;
        string spritePath;
        float layerDepth;
        float scale;

        //Properties
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
        }
        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        //Constructor
        public SpriteRenderer(GameObject gameObject, string spritePath, float layerDepth, float scale) : base(gameObject)
        {
            this.spritePath = spritePath;
            this.layerDepth = layerDepth;
            this.scale = scale;
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spritePath);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(sprite, position + offset, rectangle, color, 0f, Vector2.Zero, scale, SpriteEffects.None, layerDepth);
        }
    }
}
