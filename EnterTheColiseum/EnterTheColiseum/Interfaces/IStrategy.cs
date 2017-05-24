using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    public enum Direction
    {
        Front,
        Back,
        Left,
        Right
    }
    public interface IStrategy
    {
        void Execute(ref Direction direction);
    }
}
