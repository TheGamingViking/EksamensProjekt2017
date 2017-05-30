using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EnterTheColiseum
{
    enum ButtonType
    {
        Market,
        Tavern,
        Options,
        Upgrade,
        Colosseum,
        Barracks,
        Return,
        Fight
    }    
    class Button : UI, IUpdateable
    {
        //Fields
        ButtonType type;
        bool pressed = false;
        public delegate void ClickHandler();

        //Properties

        //Constructor
        public Button(GameObject gameObject, ButtonType type) : base(gameObject)
        {
            this.type = type;
            if (type == ButtonType.Return)
            {
                GameObject.Transform.Position = new Vector2(1165, 20);
            }
        }

        //Methods
        public override void Update()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (CollisionBox.Contains(mouseState.Position) && !pressed 
                    && !GameWorld.Instance.InMenu && !GameWorld.Instance.InFight)
                {
                    pressed = true;
                    GameWorld.Instance.InMenu = true;
                    switch (type)
                    {
                        case ButtonType.Barracks:
                            BarracksClicked();
                            break;
                        case ButtonType.Colosseum:
                            ColosseumClicked();
                            break;
                        case ButtonType.Market:
                            MarketClicked();
                            break;
                        case ButtonType.Options:
                            OptionsClicked();
                            break;
                        case ButtonType.Tavern:
                            TavernClicked();
                            break;
                        case ButtonType.Upgrade:
                            UpgradeClicked();
                            break;
                    }
                }
                else if (CollisionBox.Contains(mouseState.Position) && !GameWorld.Instance.InFight)
                {
                    switch (type)
                    {
                        case ButtonType.Fight:
                            FightClicked();
                            GameWorld.Instance.InFight = true;
                            break;
                        case ButtonType.Return:
                            ReturnClicked();
                            GameWorld.Instance.InMenu = false;
                            break;
                    }
                }
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                pressed = false;
            }
            base.Update();
        }

        //Events
        public event ClickHandler BarracksClicked;
        public event ClickHandler ColosseumClicked;
        public event ClickHandler MarketClicked;
        public event ClickHandler OptionsClicked;
        public event ClickHandler TavernClicked;
        public event ClickHandler UpgradeClicked;
        public event ClickHandler ReturnClicked;
        public event ClickHandler FightClicked;
    }
}
