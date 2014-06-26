using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    class Results
    {
        #region OOOO PROPERTIES OOOO

        public List<int> TimeSlots { get; set; }
        public List<int> Collisions { get; set; }
        public List<int> SuccessTxs { get; set; }
        public List<int> NoTxs { get; set; }
        public List<double> Throughput { get; set; }

        public double TransferRate { get; set; }
        public int MessageSize { get; set; }
        public int CyclesPerRegion { get; set; }
        public int RegionsPerTimeSlot { get; set; }
        public double TimeSlotDuration { get; set; }
        public int NumberOfUsers { get; set; }
        public int RegionsPerFrame { get; set; }
        
        #endregion

        #region OOOO BUILDERS OOOOOO

        public Results()
        {
            this.TimeSlots = new List<int>();
            this.Collisions = new List<int>();
            this.SuccessTxs = new List<int>();
            this.NoTxs = new List<int>();
            this.Throughput = new List<double>();

            this.TransferRate = 0;
            this.MessageSize = 0;
            this.CyclesPerRegion = 0;
            this.RegionsPerTimeSlot = 0;
            this.TimeSlotDuration = 0;
            this.NumberOfUsers = 0;
            this.RegionsPerFrame = 0;
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public void AddValues(int timeSlot, int collisions, int successTxs, int noTx)
        {
            this.TimeSlots.Add(timeSlot);
            this.Collisions.Add(collisions);
            this.SuccessTxs.Add(successTxs);
            this.NoTxs.Add(noTx);
            double throughput = this.calcThroughput();
            this.Throughput.Add(throughput);
        }

        public int[] GetCollisions()
        {
            try
            {
                return this.Collisions.ToArray();
            }
            catch (Exception ex)
            {
                int[] errorArray = new int[1];
                return errorArray;
            }
        }

        public int[] GetNoTxs()
        {
            try
            {
                return this.NoTxs.ToArray();
            }
            catch (Exception ex)
            {
                int[] errorArray = new int[1];
                return errorArray;
            }
        }

        public double[] GetThroughput()
        {
            try
            {
                return this.Throughput.ToArray();
            }
            catch (Exception ex)
            {
                double[] errorArray = new double[1];
                return errorArray;
            }
        }

        public double[] GetMaxThroughput()
        {
            try
            {
                double maxThroughput = this.calcMaxThroughput();
                double[] maxData = new double[this.Throughput.Count];
                for (int index = 0; index < maxData.Length; index++)
                {
                    maxData[index] = maxThroughput;
                }
                return maxData;
            }
            catch (Exception ex)
            {
                double[] errorArray = new double[1];
                return errorArray;
            }
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        private double calcThroughput()
        {
            // mean throughput per frequency channel
            double resultThroughout = 0.0;

            double timePassed = (double)this.TimeSlots.Count * this.TimeSlotDuration; //us
            
            double bytesSent = this.SuccessTxs[this.SuccessTxs.Count - 1] * this.MessageSize; //bytes
            
            double bytesSentPerUser = bytesSent / this.NumberOfUsers;

            resultThroughout = (bytesSent * 8) / timePassed; //Mbps

            resultThroughout = resultThroughout / this.RegionsPerTimeSlot;//throughput per channel
            return resultThroughout;
        }

        private double calcMaxThroughput()
        {
            //maximum throughput per frame
            double maxThroughout = 0.0;

            maxThroughout = 
                this.TransferRate * ((double)this.NumberOfUsers / (double)this.RegionsPerFrame);

            return maxThroughout;
        }

        #endregion

    }
}
