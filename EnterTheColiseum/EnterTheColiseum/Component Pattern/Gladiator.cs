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
    public class Gladiator : Component, IUpdateable, ILoadable, ICollisionEnter, ICollisionExit, ICollisionStay
    {
        //Fields
        string name;
        bool fight;
        bool canMove = true;
        float strength;
        float agility;
        float strategy;
        float attack;
        float defense;
        float health;
        List<Gear> equipment;
        List<GameObject> enemyList;
        protected IStrategy combatStrategy;
        Thread thread;
        Direction currentDirection = Direction.Front;
        Vector2 goal;
        Vector2 enemySnapPos;
        Random rnd;
        Gladiator enemy;

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
        public List<Gear> Equipment
        {
            get { return equipment; }
        }
        public IStrategy CombatStrategy
        {
            get { return combatStrategy; }
            set { combatStrategy = value; }
        }
        public int GetRandomNumber
        {
            get { return rnd.Next(1, 4); }
        }

        //Constructor
        public Gladiator(GameObject gameObject, string name, bool fight) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            equipment = new List<Gear>();
            enemyList = new List<GameObject>();
            rnd = new Random();
            health = 100;

            this.fight = fight;
            this.name = name;

            thread = new Thread(AI);
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            //Get fields from database

            CreateAnimations();
        }
        public void Update()
        {
            /*if (combatStrategy is GoTo)
            {
                foreach (GameObject enemy in enemyList)
                {
                    if (enemySnapPos == goal)
                    {
                        enemySnapPos = enemyList[0].Transform.Position;
                        combatStrategy = new GoTo(GameObject.Transform, animator, enemySnapPos);
                    }
                }
            }*/
        }
        public void Equip(Gear item)
        {
            equipment.Add(item);
        }
        public void TakeDamage(int damage, Gladiator attacker)
        {
            if (currentDirection == Direction.Front)
            {
                GameObject.Transform.Translate(new Vector2(0, 100));
                attacker.GameObject.Transform.Translate(new Vector2(0, 40));
            }
            else if (currentDirection == Direction.Back)
            {
                GameObject.Transform.Translate(new Vector2(0, -100));
                attacker.GameObject.Transform.Translate(new Vector2(0, -40));
            }
            else if (currentDirection == Direction.Right)
            {
                GameObject.Transform.Translate(new Vector2(100, 0));
                attacker.GameObject.Transform.Translate(new Vector2(40, 0));
            }
            else if (currentDirection == Direction.Front)
            {
                GameObject.Transform.Translate(new Vector2(-100, 0));
                attacker.GameObject.Transform.Translate(new Vector2(-40, 0));
            }

            health -= damage;
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
                    thread.Abort();
                    GameWorld.Instance.RemoveGameObject(GameObject);
                }
                if (canMove)
                {

                    if (enemy.GameObject.Transform.Position.X != GameObject.Transform.Position.X ||
                        enemy.GameObject.Transform.Position.Y != GameObject.Transform.Position.Y)
                    {
                        combatStrategy = new GoTo(GameObject.Transform, animator, enemy.GameObject.Transform.Position);
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
            animator.CreateAnimation("Walk", new Animation(4, 360, 0, 320, 360, 5, Vector2.Zero));
            animator.CreateAnimation("TakeDamage", new Animation(3, 0, 1, 320, 360, 3, Vector2.Zero));
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
            combatStrategy = new Attack(animator);
            canMove = false;
            (other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Yellow;
        }
        public void OnCollisionExit(Collider other)
        {
            canMove = true;
            (other.GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
        }
        public void OnCollisionStay(Collider other)
        {
            if (combatStrategy is Attack)
            {
                (other.GameObject.GetComponent("Gladiator") as Gladiator).TakeDamage(20, this);
            }
        }
    }
}
