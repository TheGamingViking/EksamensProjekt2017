using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace EnterTheColiseum
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        //Fields
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        static GameWorld instance;
        List<GameObject> gameObjects;
        List<Collider> colliders;
        MouseState mouseState;
        float deltaTime;
        List<GameObject> newObjects;
        List<GameObject> objectsToRemove;
        Random rnd;
        bool resolutionIndependent = true;
        Vector2 baseScreenSize;
        int screenWidth;
        int screenHeight;
        
        //Properties
        static public GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }
        public MouseState MouseState
        {
            get { return mouseState; }
        }
        public List<Collider> Colliders
        {
            get { return colliders; }
        }
        public float DeltaTime
        {
            get { return deltaTime; }
        }

        //Constructor
        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
            mouseState = Mouse.GetState();
            baseScreenSize = new Vector2(800, 600);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();
            colliders = new List<Collider>();
            newObjects = new List<GameObject>();
            objectsToRemove = new List<GameObject>();

            GameObject obj = new GameObject(Vector2.Zero);
            gameObjects.Add(obj);
            obj.AddComponent(new Collider(obj));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            device = graphics.GraphicsDevice;

            if (resolutionIndependent)
            {
                screenWidth = (int)baseScreenSize.X;
                screenHeight = (int)baseScreenSize.Y;
            }
            else
            {
                screenWidth = device.PresentationParameters.BackBufferWidth;
                screenHeight = device.PresentationParameters.BackBufferHeight;
            }

            foreach (GameObject obj in gameObjects)
            {
                obj.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

                // TODO: Add your update logic here
                foreach (GameObject obj in gameObjects)
                {
                    obj.Update();
                }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Vector3 screenScalingFactor;

            if (resolutionIndependent)
            {
                float horizontalScaling = (float)device.PresentationParameters.BackBufferWidth / baseScreenSize.X;
                float verticalScaling = (float)device.PresentationParameters.BackBufferHeight / baseScreenSize.Y;
                screenScalingFactor = new Vector3(horizontalScaling, verticalScaling, 1);
            }
            else
            {
                screenScalingFactor = new Vector3(1, 1, 1);
            }
            Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, globalTransformation);

            foreach (GameObject obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
