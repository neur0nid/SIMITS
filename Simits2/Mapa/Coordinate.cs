using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Coordinate()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Coordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
