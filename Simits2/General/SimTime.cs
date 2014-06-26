using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class SimTime
    {
        public int Cycles { get; set; }
        public int CyclesPerRegion { get; set; }
        public int Frames {get;set;}
        public int Regions { get; set; }
        public int CurrentRegion { get; set; }
        public int TimeSegments { get; set; }
        public int FrequencySegments { get; set; }
        public int TimeSlots { get; set; }
        public bool IsNewFrame { get; set; }
        public bool IsNewTimeSlot { get; set; }

        public SimTime()
        {
            this.TimeSegments = 0;
            this.FrequencySegments = 0;
            this.Cycles = 0;
            this.CyclesPerRegion = 1;
            this.Frames = 0;
            this.Regions = 0;
            this.CurrentRegion = 1;
            this.IsNewFrame = true;
            this.IsNewTimeSlot = true;
            this.TimeSlots = 0;
        }

        public void Reset()
        {
            this.Cycles = 0;
            this.Frames = 0;
            this.Regions = 0;
            this.CurrentRegion = 0;
        }

        public void Update()
        {
            this.Cycles++;
            
            int restoCycles = this.Cycles % this.CyclesPerRegion;
            if (restoCycles == 0)
            {
                this.Regions++;

                int regionsPerFrame = this.TimeSegments * this.FrequencySegments;
                int resto = this.Regions % regionsPerFrame;
                if (resto == 0)
                {
                    this.Frames++;
                    this.IsNewFrame = true;
                    this.CurrentRegion = 1;
                }
                else
                {
                    this.IsNewFrame = false;
                    this.CurrentRegion = resto+1;
                }
            }
            else
            {
                this.IsNewFrame = false;
            }

            int cyclesPerTimeSlot = this.CyclesPerRegion * this.FrequencySegments;
            int restoSlotCycles = this.Cycles % cyclesPerTimeSlot;
            if (restoSlotCycles == 0)
            {
                this.IsNewTimeSlot = true;
                this.TimeSlots++;
            }
            else
            {
                this.IsNewTimeSlot = false;
            }

        }
    }
}
