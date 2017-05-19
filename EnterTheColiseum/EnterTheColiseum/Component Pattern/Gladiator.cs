using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    class Gladiator : Component, IUpdateable, ILoadable
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

        /// <summary>
        /// Neural fields
        /// </summary>
        

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
            equipment = new List<Gear>();
            fight = false;

            this.name = name;

            thread = new Thread(AI);
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            //Get fields from database
        }
        public void Update()
        {

        }
        public void Execute(ref Direction direction)
        {

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
    }
}
