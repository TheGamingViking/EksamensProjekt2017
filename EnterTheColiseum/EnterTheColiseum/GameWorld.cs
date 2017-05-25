using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        List<GameObject> newObjects;
        List<GameObject> objectsToRemove;
        MouseState mouseState;
        float deltaTime;
        Random rnd;
        public delegate void ResolutionEventHandler();

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
        public ContentManager GetContent
        {
            get { return Content; }
        }

        //Constructor
        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
            Resolution.Initialize(graphics);
            IsMouseVisible = true;
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
            Window.Position = new Point(0, 0);
            Window.IsBorderless = true;
            graphics.PreferredBackBufferWidth = (int)Resolution.ScreenDimensions.X;
            graphics.PreferredBackBufferHeight = (int)Resolution.ScreenDimensions.Y;
            graphics.ApplyChanges();
            ResolutionChangedEvent();

            //GameObjects
            GameObject baseMap = new GameObject(Vector2.Zero);
            baseMap.AddComponent(new SpriteRenderer(baseMap, "Nederste lag map 1280x720", 1, 1));
            gameObjects.Add(baseMap);

            GameObject tavern = new GameObject(new Vector2(850, 490));
            tavern.AddComponent(new SpriteRenderer(tavern, "Tavern", 0.5f, 0.25f));
            tavern.AddComponent(new Collider(tavern, false, false));
            tavern.AddComponent(new Button(tavern, StructureType.Tavern));
            tavern.AddComponent(new Tavern(tavern, (Button)tavern.GetComponent("Button")));
            gameObjects.Add(tavern);

            GameObject gladiator = new GameObject(Vector2.Zero);
            gladiator.AddComponent(new SpriteRenderer(gladiator, "EtC placeholder animation", 0.2f, 0.5f));
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

            // TODO: Add your update logic here
            foreach (GameObject obj in gameObjects)
            {
                obj.Update();
            }

            if (newObjects.Count > 0)
            {
                foreach (GameObject newObj in newObjects)
                {
                    gameObjects.Add(newObj);
                }
                newObjects.Clear();
            }
            if (objectsToRemove.Count > 0)
            {
                foreach (GameObject objToRemove in objectsToRemove)
                {
                    gameObjects.Add(objToRemove);
                }
                objectsToRemove.Clear();
            }

            mouseState = Mouse.GetState();
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
            newObjects.Add(gameObject);
        }

        //Events
        public event ResolutionEventHandler ResolutionChangedEvent;
    }
}
