using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Simits2
{
    public class VehicleNccma : VehicleTdmaGt
    {
        #region OOOO MEMBERS OOOOOOO
        
        private int explorationHistoryLength;

        private enum ExplorationType { ACCESS, EXPLORATION };

        #endregion

        #region OOOO PROPERTIES OOOO
        
        public double NonAccessMaximumValue { get; set; }
        
        //cost function values
        public double Weight { get; set; }
        public double ExplorationCost { get; set; }
        public double Slope { get; set; }
        public double Attenuation { get; set; }
        public double Displacement { get; set; }

        public int ExplorationHistoryLength 
        {
            get
            {
                return this.explorationHistoryLength;
            }
            set
            {
                this.explorationHistoryLength = value;
                this.ExplorationHistory = new List<int>(value);
                for (int idx = 0; idx < value; idx++)
                {
                    this.ExplorationHistory.Add(0);
                }
            }
        }
        public List<int> ExplorationHistory { get; set; }
        public double[] Cost { get; set; }
        public int[] ExplorationRegions { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public VehicleNccma(Coordinate initialPosition, int numId, Color color, VehicleType type, MacTypes mac,
            Trajectories tray, Coordinate limit, int timeDivisions, int frequencyDivisions) : 
            base(initialPosition, numId,color,type,mac,tray,limit,timeDivisions,frequencyDivisions)
        {
            this.Cost = new double[this.totalRegions-1]; //the auxiliary exploration is not performed on the transmission region
            this.ExplorationHistoryLength = 20;

            this.NonAccessMaximumValue = 4;

            this.Weight = 0.7;
            this.ExplorationCost = 0.2;
            this.Slope = 10.0;
            this.Attenuation = 0.9;
            this.Displacement = 10;

            this.NonZeroRatio = 1;
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        protected override void initEstimationMatrix()
        {
            Random randomValue = 
                new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            for (int idxF = 0; idxF < this.frequencySegments; idxF++)
            {
                for (int idxT = 0; idxT < this.timeSegments; idxT++)
                {
                    this.EstimationMatrix[idxT, idxF] = this.MinimumValue + randomValue.NextDouble() * this.MaximumValue;
                }
            }
            this.updateSummatory();
        }

        #endregion

        #region OOOO NCCMA OOOOOOOOO

        // cost=w•estimation+(1-w)•resources
        // resources=explored/total•resources_cost
        // estimation=1+-(1/π•tan^(-1)⁡〖((explored-(total-desp)/2)/slope)+tan^(-1)⁡((total-desp)/(2•slope)) 〗 )
        // desp=desplacement•total•history
        // history= ∑_(n=1)^(n=N)[(〖explored〗_n/total•e^(n•attenuation))/e^(N•attenuation)]

		/// <summary>
        /// Compute the number of explorations to achieve the minimum value in the cost function
        /// </summary>
        public int GetNumberOfExplorations()
        {
            double resources;
            double estimation;
            double totalDisplacement;
            double history;

            int explorableRegions = this.totalRegions - 1;

            for (int idx = 0; idx < explorableRegions; idx++)
            {
                double atn1;
                double atn2;
                int explored = idx + 1;
                history = getHistoryValue();
                totalDisplacement = this.Displacement * this.totalRegions * history;
                atn1 = Math.Atan(((double)explored - (((double)this.totalRegions - totalDisplacement) / 2)) / this.Slope);
                atn2 = Math.Atan(((double)this.totalRegions - totalDisplacement) / (2 * this.Slope));
                estimation = ((-1) * ((1 / Math.PI) * (atn1 + atn2))) + 1.0;
                resources = ((double)explored / (double)this.totalRegions) * this.ExplorationCost;
                this.Cost[idx] = this.Weight * (estimation) + (1 - this.Weight) * resources;
            }
            
            int numberOfExplorations = Array.IndexOf(this.Cost, this.Cost.Min()) + 1;
            

            return numberOfExplorations;
        }

        private double getHistoryValue()
        {
            double sum = 0.0;
            double total = ((double)this.timeSegments * this.frequencySegments);
            for (int idx = 0; idx < this.ExplorationHistoryLength; idx++)
            {
                double t1 = (double)this.ExplorationHistory[idx] / total;
                double t2 = Math.Exp((idx + 1.0) * this.Attenuation);
                double t3 = Math.Exp((double)this.ExplorationHistoryLength * this.Attenuation);
                sum += (t1 * t2 / t3);
            }
            return sum;
        }

		/// <summary>
        /// Obtains the indexes of the regions to explore
        /// </summary>
        public void FillExplorations(int numberOfExplorations)
        {
            this.ExplorationRegions = new int[numberOfExplorations];
            
            int counter = 0;
            while(counter < numberOfExplorations)
            {
                int accessExploration;
                Random randomNumber = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8),
                    System.Globalization.NumberStyles.HexNumber));
                accessExploration = randomNumber.Next(1, this.totalRegions + 1);
                if (accessExploration != this.Access && !this.ExplorationRegions.Contains(accessExploration))
                {
                    this.ExplorationRegions[counter] = accessExploration;
                    counter++;
                }

            }

        }

        private void Explore(Spectrum spectrum, int region)
        {
            if (spectrum.CheckOccupancy(region)) //busy region
            {
                this.applyBonusAndPenalties(BonusAndPenalties.BETA, ExplorationType.EXPLORATION, region);
            }
            else //free region
            {
                this.applyBonusAndPenalties(BonusAndPenalties.SIGMA, ExplorationType.EXPLORATION, region);
            }
            this.updateSummatory();
        }

        private void updateSummatory()
        {
            this.Summatory = 0;
            foreach (double element in this.EstimationMatrix)
            {
                this.Summatory += element;
            }
        }

        protected override void checkMaximums(double modifier)
        {
            KeyValuePair<int, int> indexes = Spectrum.GetIndexesFromAccess(this.Access,this.frequencySegments);
            double currentValue = this.EstimationMatrix[indexes.Key, indexes.Value];
            double modifiedValue = this.EstimationMatrix[indexes.Key, indexes.Value] * modifier;

            if (modifiedValue > this.MaximumValue)
            {
                modifiedValue = this.MaximumValue;
            }
         
            this.EstimationMatrix[indexes.Key, indexes.Value] = modifiedValue;
        }

        protected override void checkMinimums(double modifier)
        {
            KeyValuePair<int, int> indexes = Spectrum.GetIndexesFromAccess(this.Access, this.frequencySegments);
            double currentValue = this.EstimationMatrix[indexes.Key, indexes.Value];
            double modifiedValue = this.EstimationMatrix[indexes.Key, indexes.Value] * modifier;

            if (modifiedValue < this.MinimumValue)
            {
                modifiedValue = this.MinimumValue;
            }
            this.EstimationMatrix[indexes.Key, indexes.Value] = modifiedValue;
        }

        protected override void applyBonusAndPenalties(BonusAndPenalties bonusOrPenalty)
        {
            this.applyBonusAndPenalties(bonusOrPenalty, ExplorationType.ACCESS, this.Access);
        }

        private void applyBonusAndPenalties(BonusAndPenalties bonusOrPenalty, ExplorationType explorationType, int access)
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

            KeyValuePair<int, int> indices = Spectrum.GetIndexesFromAccess(access, this.frequencySegments);
            double modifiedValue = this.EstimationMatrix[indices.Key, indices.Value] * bonusOrPenaltyValue;
            double specificMaximum;
            if (explorationType == ExplorationType.ACCESS)
            {
                specificMaximum = this.MaximumValue;
            }
            else
            {
                specificMaximum = this.NonAccessMaximumValue;
            }

            if (modifiedValue < specificMaximum && modifiedValue > this.MinimumValue)
            {
                this.EstimationMatrix[indices.Key, indices.Value] = modifiedValue;
            }
            else if (modifiedValue < this.MinimumValue)
            {
                this.EstimationMatrix[indices.Key, indices.Value] = this.MinimumValue;
            }
            if (modifiedValue > specificMaximum)
            {
                this.EstimationMatrix[indices.Key, indices.Value] = specificMaximum;
            }

        }

        #endregion

        #region OOOO PROTECTED OOOOO

        protected override void listen(Spectrum spectrum, int currentRegion)
        {
            // complementary exploration
            if (this.ExplorationRegions != null)
            {
                if (this.ExplorationRegions.Contains(currentRegion))
                {
                    this.Explore(spectrum, currentRegion);
                }
            }

            base.listen(spectrum, currentRegion);

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

                //Calculation of the regions to explore
                int numberOfExplorations = this.GetNumberOfExplorations();
                this.ExplorationHistory.RemoveAt(0);
                this.ExplorationHistory.Add(numberOfExplorations);
                this.FillExplorations(numberOfExplorations);
            }
        }

        #endregion

    }
}
