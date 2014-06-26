using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simits2
{
    public class FrameInfoMessage : ConvoyMessage
    {
        #region OOOO MEMBERS OOOOOOO

        private int timeSegments;
        private int frequencySegments;

        #endregion

        #region OOOO PROPERTIES OOOO

        public int[,] FrameInformation { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public FrameInfoMessage(double positionX, double positionY, int timeDivisions, int freqDivisions, int[,] frameInformation)
            : base(positionX, positionY)
        {
            this.timeSegments = timeDivisions;
            this.frequencySegments = freqDivisions;
            
            this.FrameInformation = new int[this.timeSegments, this.frequencySegments];
            for (int idxF = 0; idxF < this.frequencySegments; idxF++)
            {
                for (int idxT = 0; idxT < this.timeSegments; idxT++)
                {
                    this.FrameInformation[idxT, idxF] = frameInformation[idxT, idxF];
                }
            }
        }

        #endregion
    }
}
