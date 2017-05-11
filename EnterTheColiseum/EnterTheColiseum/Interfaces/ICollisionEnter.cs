using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterTheColiseum
{
    interface ICollisionEnter
    {
        void OnCollisionEnter(Collider other);
    }
}
