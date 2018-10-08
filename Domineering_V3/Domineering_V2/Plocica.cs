using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domineering_V2
{
    class Plocica
    {
        public string igrac { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public override string ToString()
        {
            return string.Format("({0} {1}, {2}) ", igrac, x, y);
        }
    }
}
