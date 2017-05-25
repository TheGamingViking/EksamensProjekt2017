﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheColiseum
{
    enum StructureType
    {
        Tavern,
        Market,
        Options,
        Upgrade,
        Colosseum,
        Armory
    }    
    class Button : UI, IUpdateable
    {
        //Fields
        StructureType type;
        bool pressed = false;
        public delegate void ClickHandler();

        //Properties

        //Constructor
        public Button(GameObject gameObject, StructureType type) : base(gameObject)
        {
            this.type = type;
        }

        //Methods
        public void Update()
        {
            mouseState = GameWorld.Instance.MouseState;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (CollisionBox.Contains(mouseState.Position) && !pressed)
                {
                    pressed = true;
                    switch (type)
                    {
                        case StructureType.Armory:
                            ArmoryClicked();
                            break;
                        case StructureType.Colosseum:
                            ColosseumClicked();
                            break;
                        case StructureType.Market:
                            MarketClicked();
                            break;
                        case StructureType.Options:
                            OptionsClicked();
                            break;
                        case StructureType.Tavern:
                            TavernClicked();
                            break;
                        case StructureType.Upgrade:
                            UpgradeClicked();
                            break;
                    }
                }
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                pressed = false;
            }
        }

        //Events
        public event ClickHandler ArmoryClicked;
        public event ClickHandler ColosseumClicked;
        public event ClickHandler MarketClicked;
        public event ClickHandler OptionsClicked;
        public event ClickHandler TavernClicked;
        public event ClickHandler UpgradeClicked;
    }
}
