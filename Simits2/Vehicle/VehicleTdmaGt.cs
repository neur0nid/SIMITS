using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class VehicleTdmaGt : Vehicle
    {
        #region OOOO MEMBERS OOOOOOO

        protected enum BonusAndPenalties { ALFA, BETA, RHO, SIGMA };

        protected int timeSegments;
        protected int frequencySegments;
        protected int totalRegions;

        private int lastAcess;

        #endregion

        #region OOOO PROPERTIES OOOO

        public double Alfa { get; set; } // 0 < alfa < 1
        public double Beta { get; set; } // 0 < beta < 1
        public double Rho { get; set; } // 1 < rho
        public double Sigma { get; set; } // 1 < sigma

        public double MaximumValue { get; set; }
        public double MinimumValue { get; set; }
        public double Summatory { get; set; }
        public double NonZeroRatio { get; set; } // 0 < nonZeroRatio < 1

        public double[,] EstimationMatrix { get; set; }


        #endregion

        #region OOOO BUILDERS OOOOOO

        public VehicleTdmaGt(Coordinate initialPosition, int numId, Color color, VehicleType type, MacTypes mac, 
            Trajectories tray, Coordinate limit, int timeDivisions, int frequencyDivisions)
        {

            this.Id = numId;
            this.VehicleColor = color;
            this.Type = type;
            this.Mac = mac;

            this.PositionX = initialPosition.X;
            this.PositionY = initialPosition.Y;

            this.TrajectoryType = tray;
            this.Limit = limit;

            this.timeSegments = timeDivisions;
            this.frequencySegments = frequencyDivisions;
            this.totalRegions = this.timeSegments * this.frequencySegments;

            this.Alfa = 0.4;
            this.Beta = 0.3;
            this.Rho = 1.4;
            this.Sigma = 1.3;

            this.MaximumValue = 10;
            this.MinimumValue = 0.2;
            this.Summatory = this.MaximumValue + this.totalRegions * this.MinimumValue;
            this.NonZeroRatio = 0.5;

            this.lastAcess = -1;

            this.EstimationMatrix = new double[this.timeSegments, this.frequencySegments];

            this.initEstimationMatrix();

        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public override void MacTask(Spectrum spectrum, int currentRegion)
        {
            if (this.IsMacPending)
            {
                this.runMac(spectrum, currentRegion);
                this.IsMacPending = false;
                this.IsTxPending = true;
            }
        }

        public override TXRESULT TryTx(Spectrum spectrum)
        {
            TXRESULT txResult;
            if (this.Access == -1)
            {
                txResult = TXRESULT.NOTX;
            }
            else
            {
                bool txCheckingResult = spectrum.CheckAndSetOccupancy(this.Access);
                if (txCheckingResult)
                {
                    spectrum.SetMessage(this.generateMessage(), this.Access);
                    txResult = TXRESULT.TXOK;
                    if (this.Access == this.lastAcess)
                    {
                        this.applyBonusAndPenalties(BonusAndPenalties.RHO);
                    }
                    else
                    {
                        this.applyBonusAndPenalties(BonusAndPenalties.SIGMA);
                    }
                }
                else
                {
                    spectrum.SetCollision(this.Access);
                    txResult = TXRESULT.COLLISION;
                    if (this.Access == this.lastAcess)
                    {
                        this.applyBonusAndPenalties(BonusAndPenalties.ALFA);
                    }
                    else
                    {
                        this.applyBonusAndPenalties(BonusAndPenalties.BETA);
                    }
                }
            }
            this.lastAcess = Access;
            return txResult;
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        protected virtual void initEstimationMatrix()
        {
            int totalRegions = this.EstimationMatrix.Length;

            int nonZeroRegions = (int)Math.Floor(totalRegions * this.NonZeroRatio);

            this.checkNonZeroRatio();
            this.checkInitialSummatory();
        }

        protected override void runMac(Spectrum spectrum, int currentRegion = 1)
        {
            this.Access = this.getTdmaGtAccess(spectrum);
        }

        private int getTdmaGtAccess(Spectrum spectrum)
        {
            int access = -1;
            int indexT = 0;
            int indexF = 0;

            double maxValue = 0;

            int vLimit = this.EstimationMatrix.GetUpperBound(1);
            int hLimit = this.EstimationMatrix.GetUpperBound(0);
            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    if (this.EstimationMatrix[idxT, idxF] > maxValue)
                    {
                        maxValue = this.EstimationMatrix[idxT, idxF];
                        indexT = idxT;
                        indexF = idxF;
                    }
                }
            }

            access = this.getAccessFromIndexes(indexT, indexF);

            return access;
        }

        private int getAccessFromIndexes(int idxT, int idxF)
        {
            int access = -1;
            access = (idxT * (this.EstimationMatrix.GetUpperBound(1) + 1) + idxF) + 1;
            return access;
        }

        private KeyValuePair<int, int> getIndexesFromAccess(int access)
        {
            int hIndex = (int)Math.Floor((double)((access - 1) / this.frequencySegments));
            int vIndex = (access - 1) % this.frequencySegments;
            KeyValuePair<int, int> indexes = new KeyValuePair<int, int>(hIndex, vIndex);
            return indexes;
        }

        protected virtual void applyBonusAndPenalties(BonusAndPenalties bonusOrPenalty)
        {
            double bonusOrPenaltyValue = 0;
            if (bonusOrPenalty == BonusAndPenalties.ALFA)
            {
                bonusOrPenaltyValue = this.Alfa;
            }
            else if (bonusOrPenalty == BonusAndPenalties.BETA)
            {
                bonusOrPenaltyValue = this.Beta;
            }
            else if (bonusOrPenalty == BonusAndPenalties.RHO)
            {
                bonusOrPenaltyValue = this.Rho;
            }
            else if (bonusOrPenalty == BonusAndPenalties.SIGMA)
            {
                bonusOrPenaltyValue = this.Sigma;
            }
            
            if (bonusOrPenaltyValue > 1)
            {
                this.checkMaximums(bonusOrPenaltyValue);
            }
            else if (bonusOrPenaltyValue < 1)
            {
                this.checkMinimums(bonusOrPenaltyValue);
            }

            this.checkNonZeroRatio();
            this.checkSummatory();
        }

        protected virtual void checkMaximums(double modifier)
        {
            KeyValuePair<int, int> indexes = this.getIndexesFromAccess(this.Access);
            double currentValue = this.EstimationMatrix[indexes.Key, indexes.Value];
            double modifiedValue = this.EstimationMatrix[indexes.Key, indexes.Value] * modifier;

            double difference = 0.0;

            if (modifiedValue > this.MaximumValue)
            {
                modifiedValue = this.MaximumValue;
            }
            if (modifiedValue > currentValue)
            {
                this.EstimationMatrix[indexes.Key, indexes.Value] = modifiedValue;
                difference = modifiedValue - currentValue;
                if (difference > 0)
                {
                    this.substractExcess(difference);
                }
            }
        }

        protected virtual void checkMinimums(double modifier)
        {
            KeyValuePair<int, int> indexes = this.getIndexesFromAccess(this.Access);
            double currentValue = this.EstimationMatrix[indexes.Key, indexes.Value];
            double modifiedValue = this.EstimationMatrix[indexes.Key, indexes.Value] * modifier;

            double difference = 0.0;

            if (modifiedValue < this.MinimumValue)
            {
                modifiedValue = this.MinimumValue;
            }
            if (modifiedValue < currentValue)
            {
                this.EstimationMatrix[indexes.Key, indexes.Value] = modifiedValue;
                difference = currentValue - modifiedValue;
                if (difference > 0)
                {
                    this.addExcess(difference);
                }
            }
        }

        private void checkNonZeroRatio()
        {
            List<KeyValuePair<int, int>> nonZeroElements = this.getNonZeroElements();
            int nonZeroElementsInMatrix = (int)Math.Floor(this.EstimationMatrix.Length * this.NonZeroRatio);
            if (nonZeroElements.Count > nonZeroElementsInMatrix)
            {
                do
                {
                    this.substractElement();
                    nonZeroElements = this.getNonZeroElements();
                }
                while (nonZeroElements.Count > nonZeroElementsInMatrix);
            }
            else if (nonZeroElements.Count < nonZeroElementsInMatrix)
            {
                do
                {
                    this.addElement();
                    nonZeroElements = this.getNonZeroElements();
                }
                while (nonZeroElements.Count < nonZeroElementsInMatrix-1);
            }
        }

        private void checkSummatory()
        {
            if (this.Summatory != 0)
            {
                double currentSummatory = this.getMatrixSummatory();
                if (currentSummatory < this.Summatory)
                {
                    this.addExcess(this.Summatory - currentSummatory);
                }
                else if (currentSummatory > this.Summatory)
                {
                    this.substractExcess(currentSummatory - this.Summatory);
                }
            }
        }

        private void checkInitialSummatory()
        {
            double currentSummatory = this.getMatrixSummatory();
            if (currentSummatory < this.Summatory)
            {
                this.addExcessToNonZeroElements(this.Summatory - currentSummatory);
            }
            else if (currentSummatory > this.Summatory)
            {
                this.substractExcess(currentSummatory - this.Summatory);
            }
        }

        private double getMatrixSummatory()
        {
            double summatory = 0.0;
            foreach (double element in this.EstimationMatrix)
            {
                summatory += element;
            }
            return summatory;
        }

        private void substractExcess(double excess)
        {
            Random valueRandom = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            List<KeyValuePair<int,int>> nonZeroElements = this.getNonZeroElements();
            if (nonZeroElements.Count > 0)
            {
                KeyValuePair<int, int> randomSelectedindexes = nonZeroElements[valueRandom.Next(nonZeroElements.Count)];
                if (this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] > excess)
                {
                    this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] -= excess;
                }
                else
                {
                    excess = this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value];
                    this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] = 0;
                    this.substractExcess(excess);
                }
            }
        }

        private void substractElement()
        {
            Random valueRandom = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            List<KeyValuePair<int, int>> nonZeroElements = this.getNonZeroElements();
            KeyValuePair<int, int> randomSelectedindexes = nonZeroElements[valueRandom.Next(nonZeroElements.Count)];
            double excess = this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value];
            this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] = 0;
            double currentSumatory = this.getMatrixSummatory();
            if (currentSumatory < this.Summatory)
            {
                this.addExcess(excess);
            }
        }

        private void addElement()
        {
            Random valueRandom = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            List<KeyValuePair<int, int>> zeroElements = this.getZeroElements();
            if (zeroElements.Count > 0)
            {
                KeyValuePair<int, int> randomSelectedindexes = zeroElements[valueRandom.Next(zeroElements.Count)];
                this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] = this.MinimumValue;
                double currentSumatory = this.getMatrixSummatory();
                if (currentSumatory > this.Summatory)
                {
                    this.substractExcess(this.MinimumValue);
                }
            }
        }

        private void addExcess(double excess)
        {
            Random valueRandom = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            List<KeyValuePair<int, int>> zeroElements = this.getZeroElements();
            if (zeroElements.Count > 0)
            {
                KeyValuePair<int, int> randomSelectedindexes = zeroElements[valueRandom.Next(zeroElements.Count)];
                this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] += excess;
            }
        }

        private void addExcessToNonZeroElements(double excess)
        {
            Random valueRandom = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            List<KeyValuePair<int, int>> nonZeroElements = this.getNonZeroElements();
            if (nonZeroElements.Count > 0)
            {
                KeyValuePair<int, int> randomSelectedindexes = nonZeroElements[valueRandom.Next(nonZeroElements.Count)];
                if (this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] + excess <= this.MaximumValue)
                {
                    this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] += excess;
                }
                else
                {
                    double currentValue = this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value];
                    this.EstimationMatrix[randomSelectedindexes.Key, randomSelectedindexes.Value] = this.MaximumValue - this.MinimumValue;
                    excess = excess - ((this.MaximumValue - this.MinimumValue) - currentValue);
                    this.addExcessToNonZeroElements(excess);
                }
            }
        }

        private List<KeyValuePair<int, int>> getNonZeroElements()
        {
            List<KeyValuePair<int, int>> nonZeroElements = new List<KeyValuePair<int, int>>();

            int vLimit = this.EstimationMatrix.GetUpperBound(1);
            int hLimit = this.EstimationMatrix.GetUpperBound(0);
            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    if (this.EstimationMatrix[idxT, idxF] >= this.MinimumValue)
                    {
                        nonZeroElements.Add(new KeyValuePair<int, int>(idxT, idxF));
                    }
                }
            }

            KeyValuePair<int, int> accesPosition = this.getIndexesFromAccess(this.Access);
            nonZeroElements.Remove(accesPosition);
            return nonZeroElements;
        }

        private List<KeyValuePair<int, int>> getZeroElements()
        {
            List<KeyValuePair<int, int>> zeroElements = new List<KeyValuePair<int, int>>();

            int vLimit = this.EstimationMatrix.GetUpperBound(1);
            int hLimit = this.EstimationMatrix.GetUpperBound(0);
            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    if (this.EstimationMatrix[idxT, idxF] < this.MinimumValue)
                    {
                        zeroElements.Add(new KeyValuePair<int, int>(idxT, idxF));
                    }
                }
            }

            return zeroElements;
        }

        #endregion
    }
}
