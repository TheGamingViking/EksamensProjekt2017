using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    public class Gladiator : Component, IUpdateable, ILoadable, ICollisionEnter, ICollisionExit, ICollisionStay, IAnimateable
    {
        //Fields
        string name;
        bool fight = false;
        bool canMove = true;
        bool soundIsPlaying = false;
        float strength;
        float agility;
        float strategy;
        float attack;
        float defense;
        float health;
        List<Gear> equipment;
        //List<GameObject> enemyList;
        protected IStrategy combatStrategy;
        Thread thread;
        Direction currentDirection = Direction.Front;
        //Vector2 goal;
        //Vector2 enemySnapPos;
        Vector2 modifiedPosition;
        Gladiator enemy;
        Colosseum arena;
        Random rnd;

        ///Component fields
        Animator animator;

        /// Neural fields
        
        

        //Properties
        public string Name
        {
            get { return name; }
        }
        public float Strength
        {
            get { return strength; }
        }
        public float Agility
        {
            get { return agility; }
        }
        public float Strategy
        {
            get { return strategy; }
        }
        public float Health
        {
            get { return health; }
        }
        public List<Gear> Equipment
        {
            get { return equipment; }
        }
        public IStrategy CombatStrategy
        {
            get { return combatStrategy; }
            set { combatStrategy = value; }
        }
        public bool SoundIsPlaying
        {
            get { return soundIsPlaying; }
            set { soundIsPlaying = value; }
        }
        public Random Rnd
        {
            get { return rnd; }
        }

        //Constructor
        public Gladiator(GameObject gameObject, string name, bool fight, Colosseum arena) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            equipment = new List<Gear>();
            //enemyList = new List<GameObject>();
            rnd = new Random();
            health = 100;

            this.fight = fight;
            this.name = name;
            this.arena = arena;

            thread = new Thread(AI);
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            CreateAnimations();
            //Get fields from database
        }
        public void Update()
        {

        }
        public void Equip(Gear item)
        {
            equipment.Add(item);
        }
        public void TakeDamage(int damage, Gladiator attacker)
        {
            if (currentDirection == Direction.Front)
            {
                modifiedPosition = (new Vector2(0, 100));
                attacker.GameObject.Transform.Translate(new Vector2(0, 40));
            }
            else if (currentDirection == Direction.Back)
            {
                modifiedPosition = (new Vector2(0, -100));
                attacker.GameObject.Transform.Translate(new Vector2(0, -40));
            }
            else if (currentDirection == Direction.Right)
            {
                modifiedPosition = (new Vector2(100, 0));
                attacker.GameObject.Transform.Translate(new Vector2(40, 0));
            }
            else if (currentDirection == Direction.Left)
            {
                modifiedPosition = (new Vector2(-100, 0));
                attacker.GameObject.Transform.Translate(new Vector2(-40, 0));
            }

            if ((GameObject.GetComponent("Collider") as Collider).CollisionBox.Bottom + modifiedPosition.Y > arena.ArenaBounds.Bottom ||
                (GameObject.GetComponent("Collider") as Collider).CollisionBox.Top + modifiedPosition.Y < arena.ArenaBounds.Top ||
                (GameObject.GetComponent("Collider") as Collider).CollisionBox.Right + modifiedPosition.X > arena.ArenaBounds.Right ||
                (GameObject.GetComponent("Collider") as Collider).CollisionBox.Left + modifiedPosition.X < arena.ArenaBounds.Left)
            {
                modifiedPosition = Vector2.Zero;
            }

            GameObject.Transform.Translate(modifiedPosition);
            health -= damage;
            animator.PlayAnimation("TakeDamage");
        }
        private void CalculateStatstics()
        {

        }
        public void AI()
        {
            while (fight)
            {
                if (health <= 0)
                {
                    canMove = false;
                    combatStrategy = new Die(animator);
                }
                if (enemy.Health <= 0)
                {
                    thread.Abort();
                }
                if (canMove)
                {

                    if (enemy.GameObject.Transform.Position.X != GameObject.Transform.Position.X ||
                        enemy.GameObject.Transform.Position.Y != GameObject.Transform.Position.Y)
                    {
                        combatStrategy = new GoTo(GameObject.Transform, animator, enemy.GameObject.Transform.Position, arena);
                    }
                    else
                    {
                        combatStrategy = new Idle(animator);
                    }
                }
                
                combatStrategy.Execute(ref currentDirection);
                Thread.Sleep(Convert.ToInt32((1 - GameWorld.Instance.DeltaTime) * 10));
            }
        }
        public void CreateAnimations()
        {
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 320, 360, 0, Vector2.Zero));
            animator.CreateAnimation("TakeDamage", new Animation(3, 0, 1, 320, 360, 3, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 360, 0, 320, 360, 5, Vector2.Zero));
            animator.CreateAnimation("Die", new Animation(4, 720, 0, 320, 360, 5, Vector2.Zero));
            animator.CreateAnimation("Attack", new Animation(4, 1080, 0, 320, 360, 5, Vector2.Zero));
            animator.PlayAnimation("Idle");
        }
        public void SetEnemies(Gladiator enemy)
        {
            //enemyList.Add(enemy);
            this.enemy = enemy;
        }
        public void StartFight()
        {
            thread.Start();
        }
        public void OnCollisionEnter(Collider other)
        {
            combatStrategy = new Attack(animator, this);
            canMove = false;
        }
        public void OnCollisionExit(Collider other)
        {

        }
        public void OnCollisionStay(Collider other)
        {
            if (combatStrategy is Attack)
            {
                (other.GameObject.GetComponent("Gladiator") as Gladiator).TakeDamage(20, this);
            }
        }

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("Die"))
            {
                GameWorld.Instance.RemoveGameObject(GameObject);
                thread.Abort();
            }
            if (animationName.Contains("Attack"))
            {
                canMove = true;
                soundIsPlaying = false;
            }
        }
    }
}
