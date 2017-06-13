using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;

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
        SpriteFont fonts;
        static GameWorld instance;
        List<GameObject> gameObjects;
        List<Thread> threadsForGladiators;
        List<Collider> colliders;
        List<GameObject> newObjects;
        List<GameObject> objectsToRemove;
        List<Gladiator> tempList;
        MouseState mouseState;
        float deltaTime;
        bool inMenu = false;
        bool inFight = false;
        public delegate void GenericEventHandler();
        //Sound
        List<SoundEffect> soundEffects;
        Song song;

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
        public bool InMenu
        {
            get { return inMenu; }
            set { inMenu = value; }
        }
        public bool InFight
        {
            get { return inFight; }
            set { inFight = value; }
        }
        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
        }
        public List<SoundEffect> SoundEffects
        {
            get { return soundEffects; }
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
            //Database creation
            Database.Setup();

            //List instantiation
            gameObjects = new List<GameObject>();
            threadsForGladiators = new List<Thread>();
            colliders = new List<Collider>();
            newObjects = new List<GameObject>();
            objectsToRemove = new List<GameObject>();
            soundEffects = new List<SoundEffect>();
            tempList = new List<Gladiator>();

            //Resolution
            Window.Position = new Point((int)Resolution.ScreenDimensions.X/2-640, (int)Resolution.ScreenDimensions.Y / 2-360);
            Window.IsBorderless = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            ResolutionChangedEvent();

            //GameObjects
            GameObject baseMap = new GameObject(Vector2.Zero);
            baseMap.AddComponent(new SpriteRenderer(baseMap, "Nederste lag map 1280x720", SpriteData.BackgroundDepth, 1));
            gameObjects.Add(baseMap);

            GameObject tavern = new GameObject(new Vector2(850, 490));
            tavern.AddComponent(new SpriteRenderer(tavern, "Tavern", SpriteData.StructureDepth, 0.25f));
            tavern.AddComponent(new Collider(tavern, false, true));
            tavern.AddComponent(new Button(tavern, ButtonType.Tavern));
            tavern.AddComponent(new Tavern(tavern, (Button)tavern.GetComponent("Button")));
            gameObjects.Add(tavern);

            GameObject colosseum = new GameObject(new Vector2(555, 115));
            colosseum.AddComponent(new SpriteRenderer(colosseum, "EtC arena v1", SpriteData.StructureDepth, 0.8f));
            colosseum.AddComponent(new Collider(colosseum, false, true));
            colosseum.AddComponent(new Button(colosseum, ButtonType.Colosseum));
            colosseum.AddComponent(new Colosseum(colosseum, (Button)colosseum.GetComponent("Button")));
            gameObjects.Add(colosseum);

            GameObject market = new GameObject(new Vector2(95, 160));
            market.AddComponent(new SpriteRenderer(market, "Market", SpriteData.StructureDepth, 1f));
            market.AddComponent(new Collider(market, false, true));
            market.AddComponent(new Button(market, ButtonType.Market));
            market.AddComponent(new Market(market, (Button)market.GetComponent("Button")));
            gameObjects.Add(market);

            GameObject options = new GameObject(new Vector2(20, 20));
            options.AddComponent(new SpriteRenderer(options, "options icon", SpriteData.StructureDepth, 1f));
            options.AddComponent(new Collider(options, false, true));
            options.AddComponent(new Button(options, ButtonType.Options));
            options.AddComponent(new Options(options, (Button)options.GetComponent("Button")));
            gameObjects.Add(options);

            GameObject barracks = new GameObject(new Vector2(80, 490));
            barracks.AddComponent(new SpriteRenderer(barracks, "Barrak", SpriteData.StructureDepth, 0.3f));
            barracks.AddComponent(new Collider(barracks, false, true));
            barracks.AddComponent(new Button(barracks, ButtonType.Barracks));
            barracks.AddComponent(new Barracks(barracks, (Button)barracks.GetComponent("Button")));
            gameObjects.Add(barracks);

            GameObject upgrade = new GameObject(new Vector2(880, 40));
            upgrade.AddComponent(new SpriteRenderer(upgrade, "kran", SpriteData.StructureDepth, 0.8f));
            upgrade.AddComponent(new Collider(upgrade, false, true));
            upgrade.AddComponent(new Button(upgrade, ButtonType.Upgrade));
            upgrade.AddComponent(new Upgrade(upgrade, (Button)upgrade.GetComponent("Button")));
            gameObjects.Add(upgrade);

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
            fonts = Content.Load<SpriteFont>("Fonts");

            // TODO: use this.Content to load your game content here
            foreach (GameObject obj in gameObjects)
            {
                obj.LoadContent(Content);
            }
            SoundEffects.Add(Content.Load<SoundEffect>("2642__ceacy__sword"));
            SoundEffects.Add(Content.Load<SoundEffect>("77611__joelaudio__sfx-attack-sword-001"));
            song = Content.Load<Song>("bensound-epic");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
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
#if DEBUG
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
#endif

            foreach (GameObject obj in gameObjects)
            {
                if (obj.Tag != "DoNotUpdate")
                {
                    obj.Update();
                }
            }
            for (int i = 0; i < threadsForGladiators.Count; i++)
            {
                if (!threadsForGladiators[i].IsAlive)
                {
                    threadsForGladiators[i] = new Thread(tempList[i].GameObject.Update);
                    threadsForGladiators[i].IsBackground = true;
                    threadsForGladiators[i].Start();
                }
            }
            
            if (!inFight && threadsForGladiators.Count > 0)
            {
                foreach (Thread thread in threadsForGladiators)
                {
                    thread.Abort();
                }
                threadsForGladiators.Clear();
                tempList.Clear();
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
        public void FightStart(List<Gladiator> gladiators)
        {
            tempList.AddRange(gladiators);
            for (int i = 0; i < gladiators.Count; i++)
            {
                threadsForGladiators.Add(new Thread(gladiators[i].GameObject.Update));
                threadsForGladiators[i].IsBackground = true;
            }
            inFight = true;
        }
        public void FightEnded(List<Gladiator> gladiators)
        {
            inFight = false;
            foreach (Gladiator obj in gladiators)
            {
                RemoveGameObject(obj.GameObject);
            }
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
        public event GenericEventHandler ResolutionChangedEvent;
    }
}
