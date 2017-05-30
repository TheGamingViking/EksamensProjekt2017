using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EnterTheColiseum
{
    class Trap : Component, ICollisionEnter, ICollisionStay, IAnimateable, ISaveable, ILoadable
    {
        //Fields
        int id;
        int damage;
        int uses;
        float time;

        //Properties

        //Constructor
        public Trap(GameObject gameObject) : base(gameObject)
        {
            time = 0;
        }

        //Methods
        public void LoadContent(ContentManager content)
        {
            //Load fields from database
        }
        public void OnCollisionEnter(Collider other)
        {
            TrapTripped(other.GameObject);
        }
        public void OnCollisionStay(Collider other)
        {
            time += GameWorld.Instance.DeltaTime;
            if (time > 1000)
            {
                time = 0;
                TrapTripped(other.GameObject);
            }
        }
        public void Save()
        {
            //Save to database
        }
        public void TrapTripped(GameObject gameObject)
        {
            try
            {
                (gameObject.GetComponent("Gladiator") as Gladiator).TakeDamage(damage, null);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Gladiator component is null or not found. Exception handled.");
            }
        }
        public void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }
    }
}
