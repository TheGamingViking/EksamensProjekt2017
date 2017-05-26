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

            GameObject colosseum = new GameObject(new Vector2(555, 115));
            colosseum.AddComponent(new SpriteRenderer(colosseum, "EtC arena v1", 0.5f, 0.8f));
            colosseum.AddComponent(new Collider(colosseum, false, false));
            colosseum.AddComponent(new Button(colosseum, StructureType.Colosseum));
            colosseum.AddComponent(new Colosseum(colosseum, (Button)colosseum.GetComponent("Button")));
            gameObjects.Add(colosseum);

            GameObject market = new GameObject(new Vector2(95, 160));
            market.AddComponent(new SpriteRenderer(market, "Market", 0.5f, 1f));
            market.AddComponent(new Collider(market, false, false));
            market.AddComponent(new Button(market, StructureType.Market));
            market.AddComponent(new Colosseum(market, (Button)market.GetComponent("Button")));
            gameObjects.Add(market);

            GameObject options = new GameObject(new Vector2(20, 20));
            options.AddComponent(new SpriteRenderer(options, "options icon", 0.5f, 1f));
            options.AddComponent(new Collider(options, false, false));
            options.AddComponent(new Button(options, StructureType.Options));
            options.AddComponent(new Colosseum(options, (Button)options.GetComponent("Button")));
            gameObjects.Add(options);

            GameObject barracks = new GameObject(new Vector2(80, 490));
            barracks.AddComponent(new SpriteRenderer(barracks, "Barrak", 0.5f, 0.3f));
            barracks.AddComponent(new Collider(barracks, false, false));
            barracks.AddComponent(new Button(barracks, StructureType.Barracks));
            barracks.AddComponent(new Colosseum(barracks, (Button)barracks.GetComponent("Button")));
            gameObjects.Add(barracks);

            GameObject upgrade = new GameObject(new Vector2(880, 40));
            upgrade.AddComponent(new SpriteRenderer(upgrade, "kran", 0.5f, 0.8f));
            upgrade.AddComponent(new Collider(upgrade, false, false));
            upgrade.AddComponent(new Button(upgrade, StructureType.Upgrade));
            upgrade.AddComponent(new Colosseum(upgrade, (Button)upgrade.GetComponent("Button")));
            gameObjects.Add(upgrade);

            GameObject gladiator = new GameObject(Vector2.Zero);
            gladiator.AddComponent(new SpriteRenderer(gladiator, "EtC placeholder animation", 0.2f, 0.2f));
            gladiator.AddComponent(new Animator(gladiator));
            gladiator.AddComponent(new Collider(gladiator, false, false));
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
                    gameObjects.Remove(objToRemove);
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
        public void RemoveGameObject(GameObject gameObject)
        {
            objectsToRemove.Add(gameObject);
        }

        //Events
        public event ResolutionEventHandler ResolutionChangedEvent;
    }
}
