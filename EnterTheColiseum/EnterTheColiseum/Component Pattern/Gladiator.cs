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
    public class Gladiator : Component, IUpdateable, ILoadable
    {
        //Fields
        string name;
        bool fight;
        float strength;
        float agility;
        float strategy;
        float attack;
        float defense;
        float health;
        List<Gear> equipment;
        protected IStrategy combatStrategy;
        Thread thread;
        Direction currentDirection;

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

        //Constructor
        public Gladiator(GameObject gameObject, string name) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            equipment = new List<Gear>();
            fight = false;

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
            if (GameObject.Transform.Position.X == 500 && GameObject.Transform.Position.Y <= 199)
            {
                combatStrategy = new Idle(animator);
            }

            combatStrategy.Execute(ref currentDirection);
        }
        public void Equip(Gear item)
        {
            equipment.Add(item);
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
        }
        private void CalculateStatstics()
        {

        }
        public void AI()
        {
            while (fight)
            {

            }
        }
        public void CreateAnimations()
        {
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 320, 360, 0, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 360, 0, 320, 360, 5, Vector2.Zero));
            animator.CreateAnimation("TakeDamage", new Animation(3, 0, 1, 320, 360, 3, Vector2.Zero));
            animator.PlayAnimation("Idle");

            combatStrategy = new GoTo(GameObject.Transform, animator, new Vector2(500, 200));
        }
    }
}
