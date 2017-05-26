using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    public class Gear : Component
    {
        //Fields
        string name;
        float armor;
        float attack;
        int cost;

        //Properties
        public string Name
        {
            get { return name; }
        }
        public float Armor
        {
            get { return armor; }
        }
        public float Attack
        {
            get { return attack; }
        }
        public int Cost
        {
            get { return cost; }
        }
        //Constructor
        public Gear(float attack, float armor, int cost, string name)
        {
            this.attack = attack;
            this.armor = armor;
            this.cost = cost;
            this.name = name;
        }

        //Methods


    }
}
