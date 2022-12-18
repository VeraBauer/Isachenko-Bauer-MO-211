using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship__Server
{
    internal interface IStrategy<T>
    {
        public T Execute();
    }
}
