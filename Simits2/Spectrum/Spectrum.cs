using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class Spectrum
    {
        #region OOOO PROPERTIES OOOO

        public int TimeSegments { get; private set; }
        public int FrequencySegments { get; private set; }
        public int[,] Occupancy { get; set; }
        public int[,] Collisions { get; set; }
        public Message[,] Information { get; set; }
        public int TotalRegions { get; private set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public Spectrum()
        {
            this.TimeSegments = 1;
            this.FrequencySegments = 1;
            this.Occupancy = new int[this.TimeSegments, this.FrequencySegments];
            this.Collisions = new int[this.TimeSegments, this.FrequencySegments];
            this.Information = new Message[this.TimeSegments, this.FrequencySegments];
            this.TotalRegions = 1;
        }

        public Spectrum(int timeSegments)
            : this()
        {
            this.TimeSegments = timeSegments;
            this.FrequencySegments = 1;
            this.Occupancy = new int[this.TimeSegments, this.FrequencySegments];
            this.Collisions = new int[this.TimeSegments, this.FrequencySegments];
            this.Information = new Message[this.TimeSegments, this.FrequencySegments];
            this.TotalRegions = this.TimeSegments;
        }

        public Spectrum(int timeSegments, int frequencySegments)
            : this(timeSegments)
        {
            this.FrequencySegments = frequencySegments;
            this.Occupancy = new int[this.TimeSegments, this.FrequencySegments];
            this.Collisions = new int[this.TimeSegments, this.FrequencySegments];
            this.Information = new Message[this.TimeSegments, this.FrequencySegments];
            this.TotalRegions = this.TimeSegments * this.FrequencySegments;
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public bool CheckAndSetOccupancy(int access)
        {
            if (access > 0)
            {
                KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(access);

                if (this.Occupancy[indexes.Key, indexes.Value] > 0)
                {
                    return false;
                }
                else
                {
                    this.Occupancy[indexes.Key, indexes.Value]++;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CheckOccupancy(int access)
        {
        	//returns true if the region defined by access is busy in matrix occupancy

            KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(access);

            if (this.Occupancy[indexes.Key, indexes.Value] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetOccupancy(int access)
        {
            if (access >= 1 && access <= this.TotalRegions)
            {
                KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(access);

                this.Occupancy[indexes.Key, indexes.Value]++;
            }
        }

        public void SetCollision(int access)
        {
            if (access >= 1 && access <= this.TotalRegions)
            {
                KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(access);

                this.Collisions[indexes.Key, indexes.Value]++;
            }
        }

        public Message Listen(int currentRegion)
        {
            if (currentRegion >= 1 && currentRegion <= this.TotalRegions)
            {
                KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(currentRegion);

                return this.Information[indexes.Key, indexes.Value];
            }
            else
            {
                return null;
            }
        }

        public void Reset()
        {
            //empty the information and occupancy matrix
            for (int idxF = 0; idxF < this.FrequencySegments; idxF++)
            {
                for (int idxT = 0; idxT < this.TimeSegments; idxT++)
                {
                    this.Occupancy[idxT, idxF] = 0;
                    this.Information[idxT, idxF] = new Message();
                }
            }
        }

        public void SetMessage(Message message, int access)
        {
            if (access >= 1 && access <= this.TotalRegions)
            {
                KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(access);

                this.Information[indexes.Key, indexes.Value] = message;
            }
        }

        public void ClearMessage(int access)
        {
            if (access >= 1 && access <= this.TotalRegions)
            {
                KeyValuePair<int, int> indexes = this.GetIndexesFromAccess(access);

                this.Information[indexes.Key, indexes.Value] = new Message();
            }
        }

        public KeyValuePair<int, int> GetIndexesFromAccess(int access)
        {
            int hIndex = (int)Math.Floor((double)((access - 1) / this.FrequencySegments));
            int vIndex = (access - 1) % this.FrequencySegments;
            KeyValuePair<int, int> indexes = new KeyValuePair<int, int>(hIndex, vIndex);
            return indexes;
        }

        #endregion

        #region OOOOO STATICS OOOOOO

        public static KeyValuePair<int, int> GetIndexesFromAccess(int access, int frequencySegments)
        {
            int hIndex = (int)Math.Floor((double)((access - 1) / frequencySegments));
            int vIndex = (access - 1) % frequencySegments;
            KeyValuePair<int, int> indexes = new KeyValuePair<int, int>(hIndex, vIndex);
            return indexes;
        }

        #endregion
    }
}
