using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simits2
{
    public class AutoRunner
    {
        
        #region OOOO PROPERTIES OOOO

        public List<Scenario> AutoScenarios { get; private set; }
        public int SimDelay { get; private set; }
        public int NumberOfUsers { get; private set; }
        public int CyclesToRun { get; private set; }

        #endregion

        #region OOOO MEMBERS OOOOOOO

        private Simits2 currentSimulator;

        #endregion

        #region OOOO BUILDERS OOOOOO

        public AutoRunner(Simits2 simulator, List<Scenario> scenarios, int simDelay, int numberOfUsers, int cyclesToRun)
        {
            this.currentSimulator = simulator;
            this.AutoScenarios = scenarios;
            this.SimDelay = simDelay;
            this.NumberOfUsers = numberOfUsers;
            this.CyclesToRun = cyclesToRun;
            this.currentSimulator.AutoRunStopped += currentSimulator_AutoRunStopped;
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public void RunScenarios()
        {
            if (this.AutoScenarios.Count > 0)
            {
                this.currentSimulator.ApplyScenario(this.AutoScenarios[0], this.SimDelay, this.NumberOfUsers);

                this.currentSimulator.StartAction(this.CyclesToRun);
            }
        }

        #endregion

        #region OOOO EVENTS OOOOOOOO

        void currentSimulator_AutoRunStopped(object sender, EventArgs e)
        {
            if (this.AutoScenarios.Count > 1)
            {
                this.AutoScenarios.RemoveAt(0);
                this.RunScenarios();
            }
            else if (this.AutoScenarios.Count == 1)
            {
                this.AutoScenarios.RemoveAt(0);
                this.RunScenarios();
                this.currentSimulator.CreateSuperChart();
            }
        }

        #endregion

    }
}
