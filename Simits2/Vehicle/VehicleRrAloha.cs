using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Simits2
{
    class VehicleRrAloha : Vehicle
    {
        #region OOOO MEMBERS OOOOOOO

        private int timeSegments;
        private int frequencySegments;

        #endregion

        #region OOOO PROPERTIES OOOO

        public int[,] FrameInformation { get; set; }

        public bool AlreadyTx { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public VehicleRrAloha(Coordinate initialPosition, int numId, Color color, VehicleType type,
            MacTypes mac, Trajectories tray, Coordinate limit, int timeDivisions, int frequencyDivisions)
            : base(initialPosition, numId, color, type, mac, tray, limit)
        {
            this.timeSegments = timeDivisions;
            this.frequencySegments = frequencyDivisions;
            this.FrameInformation = new int[this.timeSegments, this.frequencySegments];
            this.AlreadyTx = false;
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        #endregion

        #region OOOO PRIVATES OOOOOO

        #region ---- MAC ------------

        protected override void runMac(Spectrum spectrum, int currentRegion = 1)
        {
            int access = -1;
            access = this.getRrAlohaAccess();
            this.Access = access;
        }

        private int getRrAlohaAccess()
        {
			int access = -1;
			if (this.Access > 0) //maintains the transmission region
			{
				access = this.Access;
			}
			else
			{
				List<int> availableRegions = this.getFreeRegions();
				if (availableRegions.Count <= 0)
				{
					access = 0; //no available regions
				}
				else
				{
					Random randomNumber =
						new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
					int accessIndex = randomNumber.Next(0, availableRegions.Count - 1);
					access = availableRegions[accessIndex];
				}
			}

			if (this.Access > 0) // information frame updating
			{
				KeyValuePair<int, int> accessIndexes = Spectrum.GetIndexesFromAccess(access, this.frequencySegments);
				this.FrameInformation[accessIndexes.Key, accessIndexes.Value] = this.Id;
			}

			return access;
            
        }

        private List<int> getFreeRegions()
        {
            List<int> freeRegions = new List<int>();
            int totalRegions = this.frequencySegments * this.timeSegments;
            for (int idx = 1; idx <= totalRegions; idx++)
            {
                KeyValuePair<int, int> indexes = Spectrum.GetIndexesFromAccess(idx, this.frequencySegments);
                if (this.FrameInformation[indexes.Key, indexes.Value] == 0)
                {
                    freeRegions.Add(idx);
                }
            }
            return freeRegions;
        }

        #endregion

        #region ---- Mensajes -------

        protected override Message generateMessage()
        {
            Message message = new Message();
            message.Destination = this.Id + 1;
            message.Source = this.Id;
            message.Content = new FrameInfoMessage(this.PositionX, this.PositionY, this.timeSegments, 
                this.frequencySegments, this.FrameInformation);
            return message;
        }

        #endregion

        #region ---- listen ---------

        protected override void listen(Spectrum spectrum, int currentRegion)
        {
            if (this.IsRxPending)
            {
                Message rxMessage = spectrum.Listen(currentRegion);
                if (rxMessage != null && rxMessage.Content != null)
                {
                    this.processMessage(rxMessage);
                    this.IsRxPending = true; //RR-Aloha always listen to messages to process the information frame
                }
            }
        }

        protected override void processMessage(Message rxMessage)
        {
            if (rxMessage.Destination == this.Id)
            {
                this.PendingPositionX = (rxMessage.Content as FrameInfoMessage).PositionX;
                this.PendingPositionY = (rxMessage.Content as FrameInfoMessage).PositionY;
            }

            int[,] messageFrameInformation = (rxMessage.Content as FrameInfoMessage).FrameInformation;

            int totalRegions = this.frequencySegments * this.timeSegments;
            for (int idx = 1; idx <= totalRegions; idx++)
            {
                KeyValuePair<int, int> indexes = Spectrum.GetIndexesFromAccess(idx, this.frequencySegments);
                int incomingValue = messageFrameInformation[indexes.Key, indexes.Value];
                if (incomingValue == 0)
                {
                    if (this.FrameInformation[indexes.Key, indexes.Value] == rxMessage.Source)
                    {
                        //vehicle that has released the region
                        this.FrameInformation[indexes.Key, indexes.Value] = incomingValue;
                    }
                }
                else //(incomingValue != 0)
                {
                    if (this.FrameInformation[indexes.Key, indexes.Value] == this.Id) //selected region for this vehicle
                    {
                        if (incomingValue != this.Id)
                        {
                            //This vehicle must select a new region because a previous collision
                            this.Access = 0; //force the selection of a new region
                            this.IsMacPending = true;
                        }
                    }
                    this.FrameInformation[indexes.Key, indexes.Value] = incomingValue;
                }
            }
        }

        #endregion

        #endregion
    }
}
