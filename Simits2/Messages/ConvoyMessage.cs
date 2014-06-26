using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class ConvoyMessage
    {
        #region OOOO PROPERTIES OOOO

        public double PositionX { get; set; }
        public double PositionY { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public ConvoyMessage(double positionX, double positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        #endregion
    }
}
