using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

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
        bool inMenu = false;
        bool inFight = false;
        public delegate void ResolutionEventHandler();
        //Sound
        List<SoundEffect> soundEffects;
        Song song;
        //Database Fields
        string database = "EnterTheColiseum";
        string command;
        SQLiteCommand commander;
        SQLiteDataReader reader;
        SQLiteConnection connection;

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
        public string Command
        {
            get { return command; }
            set { command = value; }
        }
        public SQLiteCommand Commander
        {
            get { return commander; }
            set { commander = value; }
        }
        public SQLiteDataReader Reader
        {
            get { return reader; }
            set { reader = value; }
        }
        public SQLiteConnection Connection
        {
            get { return connection; }
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
            SQLiteConnection.CreateFile(database + ".db");
            connection = new SQLiteConnection($"Data Source = {database}.db;Version = 3");
            connection.Open();
            try
            {
                command = "create table Gladiators(name text primary key, strength float, agility float, strategy float, helmet text, armour text, weapon text);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                command = "create table Equipment(name text primary key, attack float, defense float, type text, cost float);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                command = "insert into Gladiators values('Ains Ooal Gown', 10, 10, 10, null, null, null);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                command = "insert into Gladiators values('Kappa Pride', 7, 5, 2, null, null, null);";
                commander = new SQLiteCommand(command, connection);
                commander.ExecuteNonQuery();
                //Insert all equipment in the game into table Equipment
            }
            catch (SQLiteException)
            {
                Console.WriteLine("SQLiteException: Table exists. Handled.");
            }

            //List instantiation
            gameObjects = new List<GameObject>();
            colliders = new List<Collider>();
            newObjects = new List<GameObject>();
            objectsToRemove = new List<GameObject>();
            soundEffects = new List<SoundEffect>();

            //Resolution
            Window.Position = new Point((int)Resolution.ScreenDimensions.X/2-640, (int)Resolution.ScreenDimensions.Y / 2-360);
            Window.IsBorderless = true;
            graphics.PreferredBackBufferWidth = 1280/*(int)Resolution.ScreenDimensions.X*/;
            graphics.PreferredBackBufferHeight = 720/*(int)Resolution.ScreenDimensions.Y*/;
            graphics.ApplyChanges();
            ResolutionChangedEvent();

            //GameObjects
            GameObject baseMap = new GameObject(Vector2.Zero);
            baseMap.AddComponent(new SpriteRenderer(baseMap, "Nederste lag map 1280x720", 1, 1));
            gameObjects.Add(baseMap);

            GameObject tavern = new GameObject(new Vector2(850, 490));
            tavern.AddComponent(new SpriteRenderer(tavern, "Tavern", 0.9f, 0.25f));
            tavern.AddComponent(new Collider(tavern, false, true));
            tavern.AddComponent(new Button(tavern, ButtonType.Tavern));
            tavern.AddComponent(new Tavern(tavern, (Button)tavern.GetComponent("Button")));
            gameObjects.Add(tavern);

            GameObject colosseum = new GameObject(new Vector2(555, 115));
            colosseum.AddComponent(new SpriteRenderer(colosseum, "EtC arena v1", 0.9f, 0.8f));
            colosseum.AddComponent(new Collider(colosseum, false, true));
            colosseum.AddComponent(new Button(colosseum, ButtonType.Colosseum));
            colosseum.AddComponent(new Colosseum(colosseum, (Button)colosseum.GetComponent("Button")));
            gameObjects.Add(colosseum);

            GameObject market = new GameObject(new Vector2(95, 160));
            market.AddComponent(new SpriteRenderer(market, "Market", 0.9f, 1f));
            market.AddComponent(new Collider(market, false, true));
            market.AddComponent(new Button(market, ButtonType.Market));
            market.AddComponent(new Market(market, (Button)market.GetComponent("Button")));
            gameObjects.Add(market);

            GameObject options = new GameObject(new Vector2(20, 20));
            options.AddComponent(new SpriteRenderer(options, "options icon", 0.9f, 1f));
            options.AddComponent(new Collider(options, false, true));
            options.AddComponent(new Button(options, ButtonType.Options));
            options.AddComponent(new Options(options, (Button)options.GetComponent("Button")));
            gameObjects.Add(options);

            GameObject barracks = new GameObject(new Vector2(80, 490));
            barracks.AddComponent(new SpriteRenderer(barracks, "Barrak", 0.9f, 0.3f));
            barracks.AddComponent(new Collider(barracks, false, true));
            barracks.AddComponent(new Button(barracks, ButtonType.Barracks));
            barracks.AddComponent(new Barracks(barracks, (Button)barracks.GetComponent("Button")));
            gameObjects.Add(barracks);

            GameObject upgrade = new GameObject(new Vector2(880, 40));
            upgrade.AddComponent(new SpriteRenderer(upgrade, "kran", 0.9f, 0.8f));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

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
