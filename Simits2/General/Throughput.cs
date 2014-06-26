using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class Throughput
    {

        #region OOOO PROPERTIES OOOO

        public TransferRates TransferRate 
        { 
            get
            {
                return transferRate;
            }
            set
            {
                this.transferRate = value;
                this.calcModulationAndRate();
            }
        }
        public Modulations Modulation { get; private set; }
        public CodingRates CodingRate { get; private set; }
       
        public int MessageInfoSize
        {
            get
            {
                return messageInfoSize;
            }
            set
            {
                this.messageInfoSize = value;
                this.calcSizeAndDuration();
            }
        }
        public int MessageTotalSize { get; private set; }
        public double TimeSlotDuration { get; private set; }

        public Dictionary<CodingRates, double> CodingConversion { get; private set; }
        public Dictionary<TransferRates, double> RateConversion { get; private set; }

        #endregion

        #region OOOO MEMBERS OOOOOOO


        private TransferRates transferRate;
        private int messageInfoSize;

        #endregion

        #region OOOO BUILDERS OOOOOO

        public Throughput()
        {
            this.CodingConversion = new Dictionary<CodingRates,double>();
            this.CodingConversion.Add(CodingRates.DOSTERCIOS, 0.66666666);
            this.CodingConversion.Add(CodingRates.TRESCUARTOS, 0.75);
            this.CodingConversion.Add(CodingRates.UNMEDIO, 0.5);

            this.RateConversion = new Dictionary<TransferRates, double>();
            this.RateConversion.Add(TransferRates.TR3, 3);
            this.RateConversion.Add(TransferRates.TR4_5, 4.5);
            this.RateConversion.Add(TransferRates.TR6, 6);
            this.RateConversion.Add(TransferRates.TR9, 9);
            this.RateConversion.Add(TransferRates.TR12, 12);
            this.RateConversion.Add(TransferRates.TR18, 18);
            this.RateConversion.Add(TransferRates.TR24, 24);
            this.RateConversion.Add(TransferRates.TR27, 27);

            this.TransferRate = TransferRates.TR3;
            this.MessageInfoSize = 40;
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        private void calcModulationAndRate()
        {
            if (this.TransferRate == TransferRates.TR3)
            {
                this.Modulation = Modulations.BPSK;
                this.CodingRate = CodingRates.UNMEDIO;
            }
            else if (this.TransferRate == TransferRates.TR4_5)
            {
                this.Modulation = Modulations.BPSK;
                this.CodingRate = CodingRates.TRESCUARTOS;
            }
            else if (this.TransferRate == TransferRates.TR6)
            {
                this.Modulation = Modulations.QPSK;
                this.CodingRate = CodingRates.UNMEDIO;
            }
            else if (this.TransferRate == TransferRates.TR9)
            {
                this.Modulation = Modulations.QPSK;
                this.CodingRate = CodingRates.TRESCUARTOS;
            }
            else if (this.TransferRate == TransferRates.TR12)
            {
                this.Modulation = Modulations.QAM16;
                this.CodingRate = CodingRates.UNMEDIO;
            }
            else if (this.TransferRate == TransferRates.TR18)
            {
                this.Modulation = Modulations.QAM16;
                this.CodingRate = CodingRates.TRESCUARTOS;
            }
            else if (this.TransferRate == TransferRates.TR24)
            {
                this.Modulation = Modulations.QAM64;
                this.CodingRate = CodingRates.DOSTERCIOS;
            }
            else if (this.TransferRate == TransferRates.TR27)
            {
                this.Modulation = Modulations.QAM64;
                this.CodingRate = CodingRates.TRESCUARTOS;
            }
        }

        private void calcSizeAndDuration()
        {
            this.MessageTotalSize = (int)Math.Ceiling((double)this.MessageInfoSize / this.CodingConversion[this.CodingRate]);
            this.TimeSlotDuration =
                ((this.MessageTotalSize * 8) / (this.RateConversion[this.TransferRate] * 1000000)) * 1000000;
        }

        #endregion
    }
}
