using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class Vehicle
    {
        public enum RUMBO { NORTE, SUR, ESTE, OESTE }

        public enum TXRESULT { TXOK, COLLISION, NOTX}

        #region OOOO MEMBERS OOOOOOO

        private Trajectories trajectoryType;

        public Coordinate Limit;
        public double PendingPositionX; 
        public double PendingPositionY; 
        public double PendingVelocityX; 
        public double PendingVelocityY;

        public List<AccessCoordinate> RegionsUsed;

        #endregion

        #region OOOO PROPERTIES OOOO

        public int Id { get; set; }
        public VehicleType Type { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double LastPositionX { get; set; }
        public double LastPositionY { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        
        public Color VehicleColor { get; set; }

        public Trajectories TrajectoryType
        {
            get
            {
                return this.trajectoryType;
            }
            set
            {
                if (this.Type == VehicleType.LEADER)
                {
                    this.trajectoryType = value;
                }
            }
        }

        public MacTypes Mac { get; set; }

        public int Access { get; set; }

        public RUMBO Course { get; set; }

        public bool IsMovementPending { get; set; }
        
        public bool IsMacPending { get; set; }

        public bool IsTxPending { get; set; }

        public bool IsRxPending { get; set; }

        public int Retransmissions { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public Vehicle()
        {
            this.trajectoryType = Trajectories.FOLLOWER;

            this.Id = 0;

            this.PositionX = 1;
            this.PositionY = 1;
            this.LastPositionX = 1;
            this.LastPositionY = 1;
            this.VelocityX = 1;
            this.VelocityY = 1;

            this.PendingPositionX = 1;
            this.PendingPositionY = 1;
            this.PendingVelocityX = 1;
            this.PendingVelocityY = 1;

            this.VehicleColor = Color.Blue;

            this.Type = VehicleType.FOLLOWER;
            this.Access = -1;
            this.Course = RUMBO.ESTE;

            this.IsMovementPending = true;
            this.IsMacPending = true;
            this.IsTxPending = true;
            this.IsRxPending = true;

            this.RegionsUsed = new List<AccessCoordinate>();

            this.Retransmissions = 0;
            
        }

        public Vehicle(double positionX, double positionY)
            : this()
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public Vehicle(Coordinate initialPosition, int numId, Color color, VehicleType type, MacTypes mac, 
            Trajectories tray, Coordinate limit)
            : this(initialPosition.X, initialPosition.Y)
        {
            this.Id = numId;
            this.VehicleColor = color;
            this.Type = type;
            this.Mac = mac;
            this.TrajectoryType = tray;
            this.Limit = limit;
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public virtual TXRESULT TryTx(Spectrum spectrum)
        {
            TXRESULT txResult;
            if (this.Access <= 0)
            {
                txResult = TXRESULT.NOTX;
            }
            else
            {
                if (spectrum.CheckAndSetOccupancy(this.Access))
                {
                    spectrum.SetMessage(this.generateMessage(), this.Access);
                    txResult = TXRESULT.TXOK;
                }
                else
                {
                    spectrum.SetCollision(this.Access);
                    txResult = TXRESULT.COLLISION;
                }
            }
            
            return txResult;
        }
      
        public void ResetFrameCondition()
        {
            this.IsMovementPending = true;
            this.IsMacPending = true;
            this.IsTxPending = false;
            this.IsRxPending = true;
        }

        public void MovementTask()
        {
            if (this.IsMovementPending)
            {
                this.move();
                this.IsMovementPending = false;
            }
        }

        public virtual void MacTask(Spectrum spectrum, int currentRegion)
        {
            if (this.IsMacPending)
            {
                this.runMac(spectrum, currentRegion);
                this.IsMacPending = false;
                this.IsTxPending = true;
            }
        }

        public void RxTask(Spectrum spectrum, int currentRegion)
        {
            this.listen(spectrum, currentRegion);
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        #region ----- MAC -----------

        protected virtual void runMac(Spectrum spectrum, int currentRegion = 1)
        {
            int access = -1;

            if (this.Mac == MacTypes.RANDOM)
            {
                access = this.getRandomAccess(spectrum);
            }
            else if (this.Mac == MacTypes.ID)
            {
                access = this.Id;
            }
            else if (this.Mac == MacTypes.S_ALOHA)
            {
                access = this.getSalohaAccess(spectrum, currentRegion);
            }
            else if (this.Mac == MacTypes.S_ALOHA_BEB)
            {
                access = this.getBinaryExponentialBackoff(spectrum, currentRegion);
            }

            this.Access = access;

        }

        private int getRandomAccess(Spectrum spectrum, int initialNumber = 1)
        {
            int access;
            Random randomNumber = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            access = randomNumber.Next(initialNumber, spectrum.TotalRegions + 1);
            return access;
        }

        private int getRandomAccess(int maxNumber, int initialNumber = 1)
        {
            int access;
            Random randomNumber = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            access = randomNumber.Next(initialNumber, maxNumber + 1);
            return access;
        }

        private int getSalohaAccess(Spectrum spectrum, int currentRegion)
        {
            int access = this.getRandomAccess(spectrum, currentRegion);
            return access;
        }

        private int getBinaryExponentialBackoff(Spectrum spectrum, int currentRegion)
        {
            int maxNumber = (int)Math.Pow(2, this.Retransmissions);
            if (maxNumber > spectrum.TotalRegions)
            {
                maxNumber = spectrum.TotalRegions;
            }
            int access = this.getRandomAccess(maxNumber, currentRegion);
            return access;
        }

        #endregion

        #region ---- Movimiento -----

        private void move()
        {
            if (this.TrajectoryType == Trajectories.CCW)
            {
                Coordinate newPosition = this.getCcwNextPoint(new Coordinate(this.PositionX, this.PositionY));

                this.PositionX = newPosition.X;
                this.PositionY = newPosition.Y;
                this.LastPositionX = this.PositionX;
                this.LastPositionY = this.PositionY;
            }
            else if (this.TrajectoryType == Trajectories.DIAGONAL)
            {
                this.LastPositionX = this.PositionX;
                this.LastPositionY = this.PositionY;
                this.PositionX = this.PositionX + this.VelocityX;
                this.PositionY = this.PositionY + this.VelocityY;
            }
            else if (this.TrajectoryType == Trajectories.FOLLOWER)
            {
                this.LastPositionX = this.PositionX;
                this.LastPositionY = this.PositionY;
                this.PositionX = this.PendingPositionX;
                this.PositionY = this.PendingPositionY;
            }


        }

        private Coordinate getCcwNextPoint(Coordinate currentPosition)
        {
            Coordinate newPosition = new Coordinate();

            if (this.Course == RUMBO.ESTE)
            {
                newPosition.X = currentPosition.X + this.VelocityX;
                newPosition.Y = currentPosition.Y;
                if (newPosition.X >= this.Limit.X - 1)
                {
                    this.Course = RUMBO.NORTE;
                }
            }
            else if (this.Course == RUMBO.NORTE)
            {
                newPosition.X = currentPosition.X;
                newPosition.Y = currentPosition.Y + this.VelocityY;
                if (newPosition.Y >= this.Limit.Y - 1)
                {
                    this.Course = RUMBO.OESTE;
                }
            }
            else if (this.Course == RUMBO.OESTE)
            {
                newPosition.X = currentPosition.X - this.VelocityX;
                newPosition.Y = currentPosition.Y;
                if (newPosition.X <= 1)
                {
                    this.Course = RUMBO.SUR;
                }
            }
            else
            {
                newPosition.X = currentPosition.X;
                newPosition.Y = currentPosition.Y - this.VelocityY;
                if (newPosition.Y <= 1)
                {
                    this.Course = RUMBO.ESTE;
                }
            }

            return newPosition;
        }

        #endregion

        #region ---- Rx -------------

        protected virtual void listen(Spectrum spectrum, int currentRegion)
        {
            if (this.IsRxPending)
            {
                Message rxMessage = spectrum.Listen(currentRegion);
                if (rxMessage != null && rxMessage.Content != null)
                {
                    if (rxMessage.Destination == this.Id || rxMessage.Destination == 0)
                    {
                        this.processMessage(rxMessage);
                        spectrum.ClearMessage(currentRegion);
                        this.IsRxPending = false;
                    }
                }
            }
        }

        #endregion

        #region ---- Mensajes -------

        protected virtual Message generateMessage()
        {
            Message message = new Message();
            message.Destination = this.Id+1;
            message.Source = this.Id;
            message.Content = new ConvoyMessage(this.PositionX, this.PositionY);
            return message;
        }

        protected virtual void processMessage(Message rxMessage)
        {
            this.PendingPositionX = (rxMessage.Content as ConvoyMessage).PositionX;
            this.PendingPositionY = (rxMessage.Content as ConvoyMessage).PositionY;
        }

        #endregion

        #endregion

    }
}
