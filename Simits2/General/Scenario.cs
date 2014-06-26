using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class Scenario
    {
        #region OOOO PROPERTIES OOOO

        public string Name { get; set; }

        public MacTypes MediumAccessControl { get; set; }

        public Map MainMap { get; set; }
        public Spectrum MainSpectrum { get; set; }
        public Spectrum InterferenceSpectrum { get; set; }
        public BindingList<Vehicle> Vehicles { get; set; }

        public SimTime SimulationTime { get; set; }

        public Throughput ThroughputConfig { get; set; }

        #endregion

        #region OOOO BUILDERS OOOOOO

        public Scenario()
        {
            this.Name = string.Empty;
            this.MediumAccessControl = MacTypes.ID;
            this.MainMap = new Map();
            this.MainSpectrum = new Spectrum();
            this.InterferenceSpectrum = new Spectrum();
            this.Vehicles = new BindingList<Vehicle>();
            this.SimulationTime = new SimTime();
            this.ThroughputConfig = new Throughput();
        }

        public Scenario(string name)
            : this()
        {
            this.Name = name;
        }

        #endregion
    }
}
