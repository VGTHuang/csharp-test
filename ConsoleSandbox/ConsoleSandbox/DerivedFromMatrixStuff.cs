using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class DerivedFromMatrixStuff
    {
        public class Vector : Matrix
        {
            public Vector(int rowCount) : base(rowCount, 1)
            {

            }
        }
    }
}
