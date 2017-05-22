using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    static class Resolution
    {
        //Fields
        static Matrix scaleMatrix;
        static Vector2 scale;
        static Vector2 gameDimensions;
        static Vector2 screenDimensions;

        //Properties
        static public Matrix ScaleMatrix
        {
            get { return scaleMatrix; }
            set { scaleMatrix = value; }
        }
        static public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        static public Vector2 GameDimensions
        {
            get { return gameDimensions; }
            set { gameDimensions = value; }
        }
        static public Vector2 ScreenDimensions
        {
            get { return screenDimensions; }
            set { screenDimensions = value; }
        }

        //Constructor - Static Class

        //Methods
        static public void Initialize(GraphicsDeviceManager graphics)
        {
            screenDimensions = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            gameDimensions = new Vector2(1280, 720);

            CalculateMatrix(graphics);
        }
        static public void CalculateMatrix(GraphicsDeviceManager graphics)
        {
            scaleMatrix = Matrix.CreateScale(graphics.PreferredBackBufferWidth / gameDimensions.X, graphics.PreferredBackBufferHeight / gameDimensions.Y, 1f);
            scale = new Vector2(ScaleMatrix.M11, ScaleMatrix.M22);
        }
    }
}

