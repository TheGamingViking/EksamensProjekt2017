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
        SpriteBatch spriteBatch;
        static GameWorld instance;
        List<GameObject> gameObjects;
        List<Collider> colliders;
        MouseState mouseState;
        float deltaTime;
        List<GameObject> newObjects;
        List<GameObject> objectsToRemove;
        Random rnd;
        public delegate void ResolutionEventHandler();

        bool keyPressed = false;

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
            Resolution.Initialize(graphics);
            mouseState = Mouse.GetState();
            ResolutionChangedEvent += ResolutionChanged;
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
            //List instantiation
            gameObjects = new List<GameObject>();
            colliders = new List<Collider>();
            newObjects = new List<GameObject>();
            objectsToRemove = new List<GameObject>();

            //Resolution
            graphics.IsFullScreen = false;

            graphics.ApplyChanges();

            //GameObjects
            GameObject baseMap = new GameObject(Vector2.Zero);
            baseMap.AddComponent(new SpriteRenderer(baseMap, "Nederste lag map 1280x720", 1, 1));
            gameObjects.Add(baseMap);

            GameObject gladiator = new GameObject(Vector2.Zero);
            gladiator.AddComponent(new SpriteRenderer(gladiator, @"EtC placeholder animation", 0.1f, 0.5f));
            gladiator.AddComponent(new Animator(gladiator));
            gladiator.AddComponent(new Collider(gladiator, true, true));
            gladiator.AddComponent(new Gladiator(gladiator, "KappaPride"));
            gameObjects.Add(gladiator);

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
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F11) && !keyPressed)
            {
                keyPressed = true;
                if (Window.IsBorderless == true)
                {
                    Window.Position = new Point((int)(Resolution.ScreenDimensions.X - Resolution.GameDimensions.X) / 2, (int)((Resolution.ScreenDimensions.Y - Resolution.GameDimensions.Y) / 2) - 40);
                    Window.IsBorderless = false;
                    //insert settings resolution here.
                    graphics.PreferredBackBufferWidth = (int)Resolution.GameDimensions.X;
                    graphics.PreferredBackBufferHeight = (int)Resolution.GameDimensions.Y;
                    graphics.ApplyChanges();
                    ResolutionChangedEvent();
                }
                else
                {
                    Window.Position = new Point(0, 0);
                    Window.IsBorderless = true;
                    graphics.PreferredBackBufferWidth = (int)Resolution.ScreenDimensions.X;
                    graphics.PreferredBackBufferHeight = (int)Resolution.ScreenDimensions.Y;
                    graphics.ApplyChanges();
                    ResolutionChangedEvent();
                }
            }
            if (!Keyboard.GetState().IsKeyDown(Keys.F11))
                keyPressed = false;

            // TODO: Add your update logic here
            foreach (GameObject obj in gameObjects)
            {
                obj.Update();
            }

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Resolution.ScaleMatrix);

            foreach (GameObject obj in gameObjects)
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void ResolutionChanged()
        {
            Resolution.CalculateMatrix(graphics);
        }
        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        //Events
        public event ResolutionEventHandler ResolutionChangedEvent;
    }
}
