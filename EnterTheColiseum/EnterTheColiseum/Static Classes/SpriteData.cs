using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    static class SpriteData
    {
        //Property Fields
        //Layer depths
        static public float BackgroundDepth { get; internal set; } = 1f;
        static public float StructureDepth { get; internal set; } = 0.9f;
        static public float MenuDepth { get; internal set; } = 0.8f;
        static public float UIElementDepth { get; internal set; } = 0.7f;
        static public float Unassigned2Depth { get; internal set; } = 0.6f;
        static public float MiddlegroundDepth { get; internal set; } = 0.5f;
        static public float TrapDepth { get; internal set; } = 0.4f;
        static public float GladiatorDepth { get; internal set; } = 0.3f;
        static public float LabelDepth { get; internal set; } = 0.2f;
        static public float UnassignedDepth { get; internal set; } = 0.1f;
        //Sprite scales
        static public float GladiatorScale { get; internal set; } = 0.2f;

        //Constructor - Static class

        //Methods
    }
}
