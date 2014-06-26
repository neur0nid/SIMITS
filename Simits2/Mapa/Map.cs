using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class Map
    {
        #region OOOO MEMBERS OOOOOOO

        #endregion

        #region OOOO PROPERTIES OOOO

        public string MainTitle { get; set; }

        public string AxisLabelX { get; set; }
        public string AxisLabelY { get; set; }

        public double SizeX { get; set; }
        public double SizeY { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public Map()
        {
            this.SizeX = 20;
            this.SizeY = 20;

            this.AxisLabelX = "x [m]";
            this.AxisLabelY = "y [m]";

            this.MainTitle = "Map";
        }

        public Map(double sizeX, double sizeY)
            :this()
        {
            this.SizeX = sizeX;
            this.SizeY = sizeY;
        }

        #endregion
    }
}
