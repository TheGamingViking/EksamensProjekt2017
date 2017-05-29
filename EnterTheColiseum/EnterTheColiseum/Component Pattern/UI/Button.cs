using System;
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
        Market,
        Tavern,
        Options,
        Upgrade,
        Colosseum,
        Barracks,
        Return
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
            if (type == StructureType.Return)
            {
                GameObject.Transform.Position = new Vector2(1165, 20);
            }
        }

        //Methods
        public override void Update()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (CollisionBox.Contains(mouseState.Position) && !pressed && !GameWorld.Instance.InMenu)
                {
                    pressed = true;
                    GameWorld.Instance.InMenu = true;
                    switch (type)
                    {
                        case StructureType.Barracks:
                            BarracksClicked();
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
                else if (CollisionBox.Contains(mouseState.Position) && type == StructureType.Return)
                {
                    ReturnClicked();
                    GameWorld.Instance.InMenu = false;
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
    }
}
