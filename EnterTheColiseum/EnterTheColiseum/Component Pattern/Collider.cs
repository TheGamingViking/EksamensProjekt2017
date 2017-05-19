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
    public class Collider : Component, IDrawable, ILoadable, IUpdateable
    {
        //Fields
        Texture2D colliderTexture;
        bool doCollisionChecks;
        bool usePixelCollision;
        List<Collider> otherColliders;
        Dictionary<string, Color[][]> pixels;

        ///Component Fields
        SpriteRenderer spriteRenderer;
        Animator animator;

        //Properties
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X + spriteRenderer.Offset.X),
                        (int)(GameObject.Transform.Position.Y + spriteRenderer.Offset.Y),
                        spriteRenderer.Rectangle.Width, spriteRenderer.Rectangle.Height
                    );
            }
        }
        public bool DoCollisionChecks
        {
            set { doCollisionChecks = value; }
        }
        private Color[] CurrentPixels
        {
            get { return pixels[animator.AnimationName][animator.CurrentIndex]; }
        }
        public bool UsePixelCollision
        {
            get { return usePixelCollision; }
        }

        //Constructor
        public Collider(GameObject gameObject, bool usePixelCollision) : base(gameObject)
        {
            otherColliders = new List<Collider>();
            pixels = new Dictionary<string, Color[][]>();

            doCollisionChecks = true;
            this.usePixelCollision = usePixelCollision;
            GameWorld.Instance.Colliders.Add(this);
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            animator = (Animator)GameObject.GetComponent("Animator");
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            colliderTexture = content.Load<Texture2D>("CollisionTexture");
            if (usePixelCollision)
            {
                CachePixels();
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(colliderTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(colliderTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(colliderTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(colliderTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
        public void Update()
        {
            CheckCollision();
        }
        private void CheckCollision()
        {
            if (doCollisionChecks)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other != this)
                    {
                        if (CollisionBox.Intersects(other.CollisionBox) && ((usePixelCollision && CheckPixelCollision(other)) || !usePixelCollision))
                        {
                            GameObject.OnCollisionStay(other);

                            if (!otherColliders.Contains(other))
                            {
                                otherColliders.Add(other);
                                GameObject.OnCollisionEnter(other);
                            }
                        }
                        else if ((otherColliders.Contains(other) && !usePixelCollision) || (CollisionBox.Intersects(other.CollisionBox) && (usePixelCollision && !CheckPixelCollision(other))))
                        {
                            otherColliders.Remove(other);
                            GameObject.OnCollisionExit(other);
                        }
                    }
                }
            }
        }
        private void CachePixels()
        {
            foreach (KeyValuePair<string, Animation> pair in animator.Animations)
            {
                Animation animation = pair.Value;

                Color[][] colors = new Color[animation.Frames][];

                for (int i = 0; i < animation.Frames; i++)
                {
                    colors[i] = new Color[animation.Rectangles[i].Width * animation.Rectangles[i].Height];

                    spriteRenderer.Sprite.GetData(0, animation.Rectangles[i], colors[i], 0, animation.Rectangles[i].Width * animation.Rectangles[i].Height);
                }
                pixels.Add(pair.Key, colors);
            }
        }
        private bool CheckPixelCollision(Collider other)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(CollisionBox.Top, other.CollisionBox.Top);
            int bottom = Math.Min(CollisionBox.Bottom, other.CollisionBox.Bottom);
            int left = Math.Max(CollisionBox.Left, other.CollisionBox.Left);
            int right = Math.Min(CollisionBox.Right, other.CollisionBox.Right);
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int firstIndex = (x - CollisionBox.Left) + (y - CollisionBox.Top) * CollisionBox.Width;
                    int secondIndex = (x - other.CollisionBox.Left) + (y - other.CollisionBox.Top) * other.CollisionBox.Width;
                    //Get the color of both pixels at this point
                    Color colorA = CurrentPixels[firstIndex];
                    Color colorB = other.CurrentPixels[secondIndex];
                    //If both pixels are not completely transparent
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        //Then an intersection has been found
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
