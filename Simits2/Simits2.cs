using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Simits2
{

    #region OOOO PUBLIC ENUMS OOOO

    public enum VehicleType { LEADER, FOLLOWER };
    public enum Trajectories { CCW, DIAGONAL, FOLLOWER };
    public enum MacTypes { TDMA_GT, S_ALOHA, S_ALOHA_BEB, NCCMA, RR_ALOHA, RANDOM, ID };
    public enum TransferRates { TR3, TR4_5, TR6, TR9, TR12, TR18, TR24, TR27 };
    public enum Modulations { BPSK, QPSK, QAM64, QAM16 };
    public enum CodingRates { UNMEDIO, DOSTERCIOS, TRESCUARTOS };

    #endregion

    public partial class Simits2 : Form
    {

        #region OOOO CONSTANTS OOOOO

        private const int SPECIAL_MODE_TIME_SEGMENTS = 10;
        private const int SPECIAL_MODE_FREQ_SEGMENTS = 6;
        private const int SPECIAL_MODE_VEHICLES = 46;
        private const bool INSERT_INTERFERENCES = true;
        private const int INTERFERENCE_FRACTION = 20;
        private const int SIZER = 3000;

        private const int SPECIAL_MODE_CYCLES_PER_REGION = 3;
        private const int SPECIAL_MODE_ITERATIONS = (SIZER * SPECIAL_MODE_TIME_SEGMENTS * SPECIAL_MODE_FREQ_SEGMENTS) / SPECIAL_MODE_CYCLES_PER_REGION;
        private const int SPECIAL_MODE_SIM_DELAY = 2;
        private const int SPECIAL_MODE_X_DIM = 30;
        private const int SPECIAL_MODE_Y_DIM = 30;

        #endregion

        #region OOOO MEMBERS OOOOOOO

        private Scenario scenario;

        private System.Timers.Timer simulationTimer;

        private System.Windows.Forms.Timer updateTimer;

        private bool isRunning;
        private bool isAutoMode;

        private int simDelay;

        private Results resultsContainer;
        private int collisions;
        private int successTx;
        private int noTx;

        private Dictionary<string, GraphControl3d.Graph3d> eamGraphs;
        private Dictionary<string, GraphControlBasic.BasicGraph> historyGraphs;
        private Dictionary<string, GraphControlBasic.BasicGraph> costGraphs;

        private Dictionary<string, CheckedListBox> specificRegions;

        private Dictionary<string, List<double>> storedValuesOfThr;
        private Dictionary<string, List<int>> storedValuesOfCollisions;


        private bool specialInterferencesEnabled;

        #endregion

        #region OOOO CONSTRUCTOR OOO

        public Simits2()
        {
            InitializeComponent();
            this.configComboTrajectories();
            this.configComboMac();
            this.scenario = new Scenario();
            this.configCombosThroughput();
            this.resultsContainer = new Results();
            this.collisions = 0;
            this.successTx = 0;
            this.noTx = 0;

            this.gridVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridVehicles.DataSource = this.scenario.Vehicles;

            this.btStop.Enabled = false;
            this.btStartSimulation.Enabled = false;
            this.btWrite.Enabled = false;

            this.eamGraphs = new Dictionary<string, GraphControl3d.Graph3d>();
            this.historyGraphs = new Dictionary<string, GraphControlBasic.BasicGraph>();
            this.costGraphs = new Dictionary<string, GraphControlBasic.BasicGraph>();
            this.specificRegions = new Dictionary<string, CheckedListBox>();

            this.simDelay = 1000;

            this.storedValuesOfThr = new Dictionary<string, List<double>>();
            this.storedValuesOfCollisions = new Dictionary<string, List<int>>();

            this.updateTimer = new Timer();
            this.updateTimer.Interval = 20;
            this.updateTimer.Tick += updateTimer_Tick;
            this.updateTimer.Start();

            this.setStatusOff();
            this.simulationTimer = new System.Timers.Timer();
            this.simulationTimer.Elapsed += simulationTimer_Elapsed;
        }

        #endregion

        #region OOOO EVENTS OOOOOOOO

        public event EventHandler AutoRunStopped;

        protected virtual void OnAutoRunStopped(EventArgs e)
        {
            if (AutoRunStopped != null)
                AutoRunStopped(this, e);
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            if (this.isRunning && !this.isAutoMode)
            {
                this.update();
            }
            else if (this.isRunning && this.isAutoMode)
            {
                this.updateSummary();
            }
        }

        void simulationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.scenario.SimulationTime.Update();

            if (this.scenario.SimulationTime.IsNewTimeSlot)
            {
                this.resultsContainer.AddValues(this.scenario.SimulationTime.TimeSlots, this.collisions, this.successTx, this.noTx);
            }

            if (this.scenario.SimulationTime.IsNewFrame)
            {
                foreach (Vehicle vh in this.scenario.Vehicles)
                {
                    vh.ResetFrameCondition();
                }
                this.keepInterferencesOccupancy();
            }

            foreach (Vehicle vh in this.scenario.Vehicles)
            {
                Task.Factory.StartNew(vehicleAction, vh);
            }
        }

        #endregion

        #region OOOO BUTTONS OOOOOOO

        private void btApplyScenario_Click(object sender, EventArgs e)
        {
            this.applyScenario();
            this.afterApplyScenarioConfiguration();
        }

        private void btStartSimulation_Click(object sender, EventArgs e)
        {
            this.isAutoMode = false;
            this.startAction();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            this.stopAction();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.configModulationAndCoding();
            this.configSizeAndDuration();
        }

        private void numMessageSize_ValueChanged(object sender, EventArgs e)
        {
            this.configSizeAndDuration();
        }

        private void vehiclesMacTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.configSizeAndDuration();
        }

        private void numVehicles_ValueChanged(object sender, EventArgs e)
        {
            this.configSizeAndDuration();
        }

        private void btWrite_Click(object sender, EventArgs e)
        {
            this.writeResults();
        }

        private void btSpecial_Click(object sender, EventArgs e)
        {
            this.setStatusOK("Special mode");
            this.specialMode();
        }

        private void btSetSpecIntrf_Click(object sender, EventArgs e)
        {
            this.setSpecificInterferences();
        }

        private void btSetAllInterferences_Click(object sender, EventArgs e)
        {
            this.setAllFreeRegionsinterferences();
        }

        private void btSetAllRandomInterferences_Click(object sender, EventArgs e)
        {
            this.setAllUsersPlusRandomInterferences();
        }

        private void btSetAllRandomInterferences2_Click(object sender, EventArgs e)
        {
            this.setAllButUsersRandomInterferences();
        }

        private void btClearInterferences_Click(object sender, EventArgs e)
        {
            this.scenario.InterferenceSpectrum.Reset();
        }

        private void btSetCurrentInterferences_Click(object sender, EventArgs e)
        {
            this.setUsersInterferences();
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        #region ------- start & stop -------

        public void StartAction(int maximumCycles)
        {
            this.startAction();
            System.Threading.Thread waitCyclesThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.waitSpecialModeCycles));
            waitCyclesThread.IsBackground = true;
            waitCyclesThread.Start(maximumCycles);
        }

        private void waitSpecialModeCycles(object argument)
        {
            int cyclesToWait = (int)argument;
            while (this.scenario.SimulationTime.Cycles <= cyclesToWait)
            {
                if (INSERT_INTERFERENCES)
                {
                    if (this.scenario.SimulationTime.Cycles == cyclesToWait / INTERFERENCE_FRACTION)
                    {
                        if (!this.specialInterferencesEnabled)
                        {
                            this.setAllUsersPlusRandomInterferences();
                            Console.WriteLine("setting interferences for " + this.scenario.Name +
                                " at cycle..: " + this.scenario.SimulationTime.Cycles);
                            this.specialInterferencesEnabled = true;
                        }
                    }
                }

                System.Threading.Thread.Sleep(this.simDelay);
            }
            this.stopAction();

            int cyclesPerTimeSlot = this.scenario.SimulationTime.CyclesPerRegion * this.scenario.SimulationTime.FrequencySegments;
            int elements = cyclesToWait / cyclesPerTimeSlot;

            this.writeResults(elements);
            this.OnAutoRunStopped(new EventArgs());

        }

        private void startAction()
        {
            this.startNotification();
            this.startSimulation();
        }

        private void startNotification()
        {
            this.setStatusOn();

            if (this.btStop.InvokeRequired)
            {
                this.btStop.BeginInvoke((MethodInvoker)delegate { this.btStop.Enabled = true; });
            }
            else
            {
                this.btStop.Enabled = true;
            }

            if (this.btStartSimulation.InvokeRequired)
            {
                this.btStartSimulation.BeginInvoke((MethodInvoker)delegate { this.btStartSimulation.Enabled = false; });
            }
            else
            {
                this.btStartSimulation.Enabled = false;
            }

            if (this.btApplyScenario.InvokeRequired)
            {
                this.btApplyScenario.BeginInvoke((MethodInvoker)delegate { this.btApplyScenario.Enabled = false; });
            }
            else
            {
                this.btApplyScenario.Enabled = false;
            }
        }

        private void stopAction()
        {
            this.stopNotification();
            this.stopSimulation();

        }

        private void stopNotification()
        {
            this.setStatusOff();

            if (this.btStop.InvokeRequired)
            {
                this.btStop.BeginInvoke((MethodInvoker)delegate { this.btStop.Enabled = false; });
            }
            else
            {
                this.btStop.Enabled = false;
            }

            if (this.btStartSimulation.InvokeRequired)
            {
                this.btStartSimulation.BeginInvoke((MethodInvoker)delegate { this.btStartSimulation.Enabled = true; });
            }
            else
            {
                this.btStartSimulation.Enabled = true;
            }

            if (this.btApplyScenario.InvokeRequired)
            {
                this.btApplyScenario.BeginInvoke((MethodInvoker)delegate { this.btApplyScenario.Enabled = true; });
            }
            else
            {
                this.btApplyScenario.Enabled = true;
            }

            if (this.btWrite.InvokeRequired)
            {
                this.btWrite.BeginInvoke((MethodInvoker)delegate { this.btWrite.Enabled = true; });
            }
            else
            {
                this.btWrite.Enabled = true;
            }
        }

        #endregion

        #region ------- form config --------

        private void configComboTrajectories()
        {
            foreach (string trajectory in Enum.GetNames(typeof(Trajectories)))
            {
                if (trajectory != "FOLLOWER")
                {
                    this.leaderTrajectories.Items.Add(trajectory);
                }
            }
            this.leaderTrajectories.SelectedIndex = 0;
        }

        private void configComboMac()
        {
            foreach (string mac in Enum.GetNames(typeof(MacTypes)))
            {
                this.vehiclesMacTypes.Items.Add(mac);
            }
            this.vehiclesMacTypes.SelectedIndex = 1;
        }

        private void configCombosThroughput()
        {
            this.comboTransfer.SelectedIndex = 0;
            this.configModulationAndCoding();
            this.configSizeAndDuration();
        }

        #endregion

        #region ----- mmessage & slots -----

        private void configModulationAndCoding()
        {
            if (this.comboTransfer.SelectedIndex == 0) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR3;
            else if (this.comboTransfer.SelectedIndex == 1) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR4_5;
            else if (this.comboTransfer.SelectedIndex == 2) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR6;
            else if (this.comboTransfer.SelectedIndex == 3) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR9;
            else if (this.comboTransfer.SelectedIndex == 4) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR12;
            else if (this.comboTransfer.SelectedIndex == 5) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR18;
            else if (this.comboTransfer.SelectedIndex == 6) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR24;
            else if (this.comboTransfer.SelectedIndex == 7) this.scenario.ThroughputConfig.TransferRate = TransferRates.TR27;
            else this.scenario.ThroughputConfig.TransferRate = TransferRates.TR3;

            if (this.scenario.ThroughputConfig.Modulation == Modulations.BPSK) this.comboModulation.SelectedIndex = 0;
            else if (this.scenario.ThroughputConfig.Modulation == Modulations.QPSK) this.comboModulation.SelectedIndex = 1;
            else if (this.scenario.ThroughputConfig.Modulation == Modulations.QAM16) this.comboModulation.SelectedIndex = 2;
            else if (this.scenario.ThroughputConfig.Modulation == Modulations.QAM64) this.comboModulation.SelectedIndex = 3;
            else this.comboModulation.SelectedIndex = 0;

            if (this.scenario.ThroughputConfig.CodingRate == CodingRates.UNMEDIO) this.comboCoding.SelectedIndex = 0;
            else if (this.scenario.ThroughputConfig.CodingRate == CodingRates.DOSTERCIOS) this.comboCoding.SelectedIndex = 1;
            else if (this.scenario.ThroughputConfig.CodingRate == CodingRates.TRESCUARTOS) this.comboCoding.SelectedIndex = 2;
            else this.comboModulation.SelectedIndex = 0;

        }

        private void configSizeAndDuration()
        {
            if (this.scenario != null)
            {
                MacTypes vehicleMac = (MacTypes)Enum.Parse(typeof(MacTypes), this.vehiclesMacTypes.SelectedItem.ToString());
                if (vehicleMac == MacTypes.RR_ALOHA)
                {
                    int rrAlohaoverhead = 2 * (int)this.numVehicles.Value; //bytes
                    this.scenario.ThroughputConfig.MessageInfoSize = (int)this.numMessageSize.Value + rrAlohaoverhead;
                    this.setStatusInfo("Applying RR-Aloha overhead [" + rrAlohaoverhead + " bytes] to message total size");
                    this.tbTotalSize.BackColor = Color.Orange;
                }
                else
                {
                    this.scenario.ThroughputConfig.MessageInfoSize = (int)this.numMessageSize.Value;
                    this.tbTotalSize.BackColor = SystemColors.Window;
                }
                this.tbTotalSize.Text = this.scenario.ThroughputConfig.MessageTotalSize.ToString();
                this.tbSlotDuration.Text = this.scenario.ThroughputConfig.TimeSlotDuration.ToString();
            }
        }

        #endregion

        #region  ---- initial conditions ----

        private void DrawInitialMap()
        {
            this.grMapMap.SetSize(this.scenario.MainMap.SizeX, this.scenario.MainMap.SizeY);

            this.grMapMap.SetTitle(this.scenario.MainMap.MainTitle);
            this.grMapMap.SetXAxisTitle(this.scenario.MainMap.AxisLabelX);
            this.grMapMap.SetYAxisTitle(this.scenario.MainMap.AxisLabelY);

            this.grMapMap.ClearSeries();
            foreach (Vehicle vehicle in this.scenario.Vehicles)
            {
                this.grMapMap.NewPoint(vehicle.Id.ToString(), vehicle.PositionX, vehicle.PositionY, vehicle.VehicleColor);
            }
        }

        private void DrawInitialSpectrum()
        {
            int[] filaSpectrum = new int[this.scenario.MainSpectrum.TimeSegments];
            int[] filaCollision = new int[this.scenario.MainSpectrum.TimeSegments];
            this.grSpectrumMap.ClearSeries();
            this.grCollisionMap.ClearSeries();
            this.grInterferences.ClearSeries();

            this.grSpectrumMap.NewSerie("spectrum", this.scenario.MainSpectrum.Occupancy, Color.Orange);
            this.grInterferences.NewSerie("interferences", this.scenario.InterferenceSpectrum.Occupancy, Color.LightSlateGray);
            this.grCollisionMap.NewSerie("collisions", this.scenario.MainSpectrum.Collisions, Color.Orange);

            this.grSpectrumMap.SetTitle("Spectrum occupancy");
            this.grSpectrumMap.SetXAxisTitle("Time slots");
            this.grSpectrumMap.SetYAxisTitle("Frequency channels");

            this.grInterferences.SetTitle("Interferences");
            this.grInterferences.SetXAxisTitle("Time slots");
            this.grInterferences.SetYAxisTitle("Frequency channels");

            this.grCollisionMap.SetTitle("Collisions");
            this.grCollisionMap.SetXAxisTitle("Time slots");
            this.grCollisionMap.SetYAxisTitle("Frequency channels");

            this.grSpectrumMap.SetSize(this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments, 2);
            this.grInterferences.SetSize(this.scenario.InterferenceSpectrum.TimeSegments, this.scenario.InterferenceSpectrum.FrequencySegments, 2);
            this.grCollisionMap.SetSize(this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments);
        }

        #endregion

        #region -------- scenario ----------

        private void applyScenario()
        {

            #region --- map & charts ---

            this.scenario.MainMap = new Map((double)this.numXSize.Value, (double)this.numYSize.Value);
            Coordinate limits = new Coordinate((double)this.numXSize.Value, (double)this.numYSize.Value);
            this.scenario.MainSpectrum = new Spectrum((int)this.numTimeSlots.Value, (int)this.numFreqSlots.Value);
            this.scenario.InterferenceSpectrum = new Spectrum((int)this.numTimeSlots.Value, (int)this.numFreqSlots.Value);
            this.scenario.SimulationTime.TimeSegments = this.scenario.MainSpectrum.TimeSegments;
            this.scenario.SimulationTime.FrequencySegments = this.scenario.MainSpectrum.FrequencySegments;
            this.scenario.SimulationTime.CyclesPerRegion = (int)this.numCyclesPerFrame.Value;

            #endregion

            this.simDelay = (int)this.numSimDelay.Value;

            MacTypes mac = (MacTypes)Enum.Parse(typeof(MacTypes), this.vehiclesMacTypes.SelectedItem.ToString());
            this.scenario.Name = mac.ToString();

            this.configureInterferences();

            this.scenario.Vehicles.Clear();
            this.clearTabMatrix();
            this.clearTabHistory();
            this.clearTabCost();

            #region --- inicialización de vehículos ---

            MacTypes vehicleMac = (MacTypes)Enum.Parse(typeof(MacTypes), this.vehiclesMacTypes.SelectedItem.ToString());

            for (int idx = 1; idx <= this.numVehicles.Value; idx++)
            {
                #region NCCMA

                if (vehicleMac == MacTypes.NCCMA)
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new VehicleNccma(new Coordinate(1, 1), idx, Color.Blue, VehicleType.LEADER, vehicleMac, Trajectories.CCW, limits,
                            this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new VehicleNccma(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.FOLLOWER,
                                vehicleMac, Trajectories.FOLLOWER, limits, this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }

                }

                #endregion

                #region TDMAGT

                else if (vehicleMac == MacTypes.TDMA_GT)
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new VehicleTdmaGt(new Coordinate(1, 1), idx, Color.Blue, VehicleType.LEADER, vehicleMac, Trajectories.CCW, limits,
                            this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new VehicleTdmaGt(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.FOLLOWER,
                                vehicleMac, Trajectories.FOLLOWER, limits, this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }

                }

                #endregion

                #region RR-ALOHA

                else if (vehicleMac == MacTypes.RR_ALOHA)
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new VehicleRrAloha(new Coordinate(1, 1), idx, Color.Blue, VehicleType.LEADER, vehicleMac, Trajectories.CCW, limits,
                            this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new VehicleRrAloha(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.FOLLOWER,
                                vehicleMac, Trajectories.FOLLOWER, limits, this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }

                }

                #endregion

                #region OTHERS

                else
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new Vehicle(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.LEADER, vehicleMac, Trajectories.CCW, limits));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new Vehicle(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.FOLLOWER, vehicleMac, Trajectories.FOLLOWER, limits));
                    }
                }

                #endregion
            }

            #endregion

            this.initMatrixGraphs();
            this.initHistoryGraphs();
            this.initCostGraphs();

            this.configureResultsContainer();

        }

        public void ApplyScenario(Scenario inputScenario, int inputSimDelay, int numberOfUsers)
        {

            this.scenario = inputScenario;
            Coordinate limits = new Coordinate(this.scenario.MainMap.SizeX, this.scenario.MainMap.SizeY);

            this.simDelay = inputSimDelay;

            this.configureInterferences();

            this.scenario.Vehicles.Clear();
            this.clearTabMatrix();
            this.clearTabHistory();
            this.clearTabCost();

            #region --- inicialización de vehículos ---

            MacTypes vehicleMac = this.scenario.MediumAccessControl;

            for (int idx = 1; idx <= numberOfUsers; idx++)
            {
                #region NCCMA

                if (vehicleMac == MacTypes.NCCMA)
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new VehicleNccma(new Coordinate(1, 1), idx, Color.Blue, VehicleType.LEADER,
                            vehicleMac, Trajectories.CCW, limits, this.scenario.MainSpectrum.TimeSegments,
                            this.scenario.MainSpectrum.FrequencySegments));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new VehicleNccma(new Coordinate(1, 1), idx, Color.SteelBlue,
                            VehicleType.FOLLOWER, vehicleMac, Trajectories.FOLLOWER, limits,
                            this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }

                }

                #endregion

                #region TDMAGT

                else if (vehicleMac == MacTypes.TDMA_GT)
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new VehicleTdmaGt(new Coordinate(1, 1), idx, Color.Blue,
                            VehicleType.LEADER, vehicleMac, Trajectories.CCW, limits, this.scenario.MainSpectrum.TimeSegments,
                            this.scenario.MainSpectrum.FrequencySegments));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new VehicleTdmaGt(new Coordinate(1, 1), idx, Color.SteelBlue,
                            VehicleType.FOLLOWER, vehicleMac, Trajectories.FOLLOWER, limits,
                            this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }

                }

                #endregion

                #region RR-ALOHA

                else if (vehicleMac == MacTypes.RR_ALOHA)
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new VehicleRrAloha(new Coordinate(1, 1), idx, Color.Blue, VehicleType.LEADER,
                            vehicleMac, Trajectories.CCW, limits, this.scenario.MainSpectrum.TimeSegments,
                            this.scenario.MainSpectrum.FrequencySegments));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new VehicleRrAloha(new Coordinate(1, 1), idx, Color.SteelBlue,
                            VehicleType.FOLLOWER, vehicleMac, Trajectories.FOLLOWER, limits,
                            this.scenario.MainSpectrum.TimeSegments, this.scenario.MainSpectrum.FrequencySegments));
                    }

                }

                #endregion

                #region OTHERS

                else
                {
                    if (idx == 1)
                    {
                        this.scenario.Vehicles.Add(new Vehicle(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.LEADER,
                            vehicleMac, Trajectories.CCW, limits));
                    }
                    else
                    {
                        this.scenario.Vehicles.Add(new Vehicle(new Coordinate(1, 1), idx, Color.SteelBlue, VehicleType.FOLLOWER,
                            vehicleMac, Trajectories.FOLLOWER, limits));
                    }
                }

                #endregion
            }

            #endregion

            this.initMatrixGraphs();
            this.initHistoryGraphs();
            this.initCostGraphs();

            this.specialInterferencesEnabled = false;

            this.configureResultsContainer();
            this.afterApplyScenarioConfiguration();

        }

        private void afterApplyScenarioConfiguration()
        {
            this.gridVehicles.Refresh();
            this.DrawInitialMap();
            this.DrawInitialSpectrum();

            this.setStatusOK("Scenario configuration applied.");

            if (this.btStop.InvokeRequired)
            {
                this.btStop.BeginInvoke((MethodInvoker)delegate { this.btStop.Enabled = false; });
            }
            else
            {
                this.btStop.Enabled = false;
            }

            if (this.btWrite.InvokeRequired)
            {
                this.btWrite.BeginInvoke((MethodInvoker)delegate { this.btWrite.Enabled = false; });
            }
            else
            {
                this.btWrite.Enabled = false;

            }

            if (this.btStartSimulation.InvokeRequired)
            {
                this.btStartSimulation.BeginInvoke((MethodInvoker)delegate { this.btStartSimulation.Enabled = true; });
            }
            else
            {
                this.btStartSimulation.Enabled = true;
            }

            if (this.btApplyScenario.InvokeRequired)
            {
                this.btApplyScenario.BeginInvoke((MethodInvoker)delegate { this.btApplyScenario.Enabled = true; });
            }
            else
            {
                this.btApplyScenario.Enabled = true;
            }
        }

        #endregion

        #region ---------- update ----------

        private void update()
        {
            this.updateGrid();
            this.updateMap();
            this.updateSpectrumChart();
            this.updateSummary();
            this.updateResultsGraphs();
            this.updateMatrixGraphs();
            this.updateHistoryGraphs();
            this.updateCostGraphs();
        }

        private void updateGrid()
        {
            this.gridVehicles.Refresh();
        }

        private void updateMap()
        {
            foreach (Vehicle vehicle in this.scenario.Vehicles)
            {
                this.grMapMap.ChangePoint(vehicle.Id.ToString(), vehicle.PositionX, vehicle.PositionY, vehicle.VehicleColor);
            }
        }

        private void updateSpectrumChart()
        {
            this.grSpectrumMap.ChangeSerie("spectrum", this.scenario.MainSpectrum.Occupancy, Color.Orange);
            this.grCollisionMap.ChangeSerie("collisions", this.scenario.MainSpectrum.Collisions, Color.IndianRed);
            this.grInterferences.ChangeSerie("interferences", this.scenario.InterferenceSpectrum.Occupancy, Color.LightSlateGray);
        }

        private void updateSummary()
        {
            this.simulatedCyclesText.Text = this.scenario.SimulationTime.Cycles.ToString();
            this.simulatedFramesText.Text = this.scenario.SimulationTime.Frames.ToString();
            this.simulatedRegionsText.Text = this.scenario.SimulationTime.Regions.ToString();
            this.currentRegionText.Text = this.scenario.SimulationTime.CurrentRegion.ToString();
        }

        #endregion

        #region -------- status bar --------

        private void setStatus(Color color, string text)
        {
            if (this.panelStatus.InvokeRequired)
            {
                this.panelStatus.BeginInvoke((MethodInvoker)delegate { this.panelStatus.BackColor = color; });
            }
            else
            {
                this.panelStatus.BackColor = color;
            }

            if (this.labelStatus.InvokeRequired)
            {
                this.labelStatus.BeginInvoke((MethodInvoker)delegate { this.labelStatus.Text = text; });
            }
            else
            {
                this.labelStatus.Text = text;
            }
        }

        private void setStatusOn()
        {
            this.isRunning = true;

            if (this.isAutoMode)
            {
                this.setStatus(Color.Goldenrod, "Simulation in progress (auto mode)...");
            }
            else
            {
                this.setStatus(Color.Goldenrod, "Simulation in progress...");
            }
        }

        private void setStatusOff()
        {
            this.isRunning = false;
            this.setStatus(Color.LightGray, "Simulation stopped");
        }

        private void setStatusOK(string message)
        {
            this.setStatus(Color.LightSeaGreen, message);
        }

        private void setStatusWorking()
        {
            this.isRunning = true;
            this.setStatus(Color.Goldenrod, "Work in progress...");
        }

        private void setStatusInfo(string message)
        {
            this.setStatus(Color.Orange, message);
        }

        #endregion

        #region -------- simulation --------

        private void startSimulation()
        {
            this.scenario.SimulationTime.Reset();
            this.simulationTimer.Interval = this.simDelay;
            this.simulationTimer.Start();
        }

        private void stopSimulation()
        {
            this.simulationTimer.Stop();
        }

        private void vehicleAction(object vh)
        {
            Vehicle vehicle = (Vehicle)vh;

            int subRegion;
            subRegion = this.scenario.SimulationTime.Cycles % this.scenario.SimulationTime.CyclesPerRegion;

            if (subRegion == 0)
            {
                vehicle.MovementTask();
                vehicle.MacTask(this.scenario.MainSpectrum, this.scenario.SimulationTime.CurrentRegion);
            }
            else if (subRegion == 1)
            {
                if (this.scenario.SimulationTime.CurrentRegion == vehicle.Access)
                {
                    this.txTask(vehicle);
                }
            }
            else
            {
                vehicle.RxTask(this.scenario.MainSpectrum, this.scenario.SimulationTime.CurrentRegion);
            }
        }

        private void txTask(Vehicle vehicle)
        {
            if (vehicle.IsTxPending)
            {
                Vehicle.TXRESULT txResult = vehicle.TryTx(this.scenario.MainSpectrum);
                vehicle.IsTxPending = false;
                vehicle.IsMacPending = false;
                if (txResult == Vehicle.TXRESULT.TXOK)
                {
                    this.successTx++;
                    vehicle.Retransmissions = 0;
                    vehicle.RegionsUsed.Add(new AccessCoordinate(vehicle.Access,
                        this.scenario.SimulationTime.Frames, this.successTx));
                }
                else if (txResult == Vehicle.TXRESULT.COLLISION)
                {
                    this.collisions++;
                    vehicle.Retransmissions++;
                    vehicle.RegionsUsed.Add(new AccessCoordinate(vehicle.Access * (-1),
                        this.scenario.SimulationTime.Frames, this.successTx));
                    if (vehicle.Mac == MacTypes.S_ALOHA)
                    {
                        if (this.scenario.SimulationTime.CurrentRegion == this.scenario.MainSpectrum.TotalRegions)
                        {
                            this.noTx++;
                        }
                        else
                        {
                            vehicle.IsMacPending = true;
                        }
                    }
                    if (vehicle.Mac == MacTypes.RR_ALOHA)
                    {
                        //This mechanism should be implemented by an upper layer of the protocol as RR-Aloha is only able to
                        //detect collisions by processing the information received in the information frame. In case of an
                        //external interference, the information frame does not contain information about the collision.
                        VehicleRrAloha rralohaVh = vehicle as VehicleRrAloha;
                        KeyValuePair<int, int> indexes = Spectrum.GetIndexesFromAccess(rralohaVh.Access, this.scenario.MainSpectrum.FrequencySegments);
                        rralohaVh.FrameInformation[indexes.Key, indexes.Value] = 0;
                        rralohaVh.Access = 0;
                        rralohaVh.IsMacPending = true;
                    }
                }
                else if (txResult == Vehicle.TXRESULT.NOTX)
                {
                    this.noTx++;
                    vehicle.RegionsUsed.Add(new AccessCoordinate(-999,
                        this.scenario.SimulationTime.Frames, this.successTx));
                }
            }


        }

        #endregion

        #region ---------- results ---------

        private void updateResultsGraphs()
        {
            this.graphCollisions.SetTitle("Unsuccessful transmissions");
            this.graphCollisions.ClearSeries();
            this.graphCollisions.SetXAxisTitle("Simulated time slots");
            this.graphCollisions.SetYAxisTitle("Number of unsuccessful transmissions");

            int[] dataCollisions = new int[this.resultsContainer.Collisions.Count];
            dataCollisions = this.resultsContainer.GetCollisions();
            this.graphCollisions.NewSerie("Collisions", dataCollisions,
                Color.Red, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine);

            int[] dataNotxs = new int[this.resultsContainer.NoTxs.Count];
            dataNotxs = this.resultsContainer.GetNoTxs();
            this.graphCollisions.NewSerie("No transmissions", dataNotxs,
                Color.Brown, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine);


            this.graphThroughput.SetTitle("Total throughput");
            this.graphThroughput.ClearSeries();
            this.graphThroughput.SetXAxisTitle("Simulated time slots");
            this.graphThroughput.SetYAxisTitle("Throughput per channel [Mbps]");

            double[] dataThroughput = new double[this.resultsContainer.Throughput.Count];
            dataThroughput = this.resultsContainer.GetThroughput();
            this.graphThroughput.NewSerie("Throughput", dataThroughput,
                Color.Blue, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine);

            double[] maxThroughput = new double[this.resultsContainer.Throughput.Count];
            maxThroughput = this.resultsContainer.GetMaxThroughput();
            this.graphThroughput.NewSerie("Max", maxThroughput,
                Color.DarkGreen, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine);

        }

        private void configureResultsContainer()
        {
            this.resultsContainer.TransferRate =
                this.scenario.ThroughputConfig.RateConversion[this.scenario.ThroughputConfig.TransferRate];
            this.resultsContainer.MessageSize = this.scenario.ThroughputConfig.MessageTotalSize;
            this.resultsContainer.CyclesPerRegion = this.scenario.SimulationTime.CyclesPerRegion;
            this.resultsContainer.RegionsPerTimeSlot = this.scenario.MainSpectrum.FrequencySegments;
            this.resultsContainer.TimeSlotDuration = this.scenario.ThroughputConfig.TimeSlotDuration;
            this.resultsContainer.NumberOfUsers = this.scenario.Vehicles.Count;
            this.resultsContainer.RegionsPerFrame = this.scenario.MainSpectrum.TotalRegions;

            this.resultsContainer.TimeSlots.Clear();
            this.successTx = 0;
            this.resultsContainer.SuccessTxs.Clear();
            this.resultsContainer.Throughput.Clear();
            this.collisions = 0;
            this.resultsContainer.Collisions.Clear();
            this.noTx = 0;
            this.resultsContainer.NoTxs.Clear();
        }

        private void writeResults()
        {
            this.setStatusWorking();
            Writer writer = new Writer(this.scenario, this.resultsContainer);
            string thrFile = writer.WriteThroughputResults().Item1;
            string accessFile = writer.WriteAccessResults();
            this.setStatusOK("The results file have been generated");
        }

        private void writeResults(int elements)
        {
            this.setStatusWorking();
            Writer writer = new Writer(this.scenario, this.resultsContainer);
            Tuple<string, List<double>, List<int>> tupla = writer.WriteThroughputResults();
            string thrFile = tupla.Item1;
            this.storedValuesOfThr.Add(this.scenario.Name, tupla.Item2);
            this.storedValuesOfCollisions.Add(this.scenario.Name, tupla.Item3);
            //this.createThrExcelChart(thrFile, elements);
            //this.createThrExcelChart2(thrFile, elements);

            string accessFile = writer.WriteAccessResults();
            this.setStatusOK("The results file have been generated");
        }

        private void createThrExcelChart(string thrFile, int elements)
        {
            Excel.Application excelApp = new Excel.Application();
            object missingValue = System.Reflection.Missing.Value;
            Excel.Workbook excelBook = excelApp.Workbooks.Open(thrFile);
            Excel.Worksheet excelSheet = (Excel.Worksheet)excelBook.Worksheets.get_Item(1);

            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)excelSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(340, 100, 600, 300);
            Excel.Chart chartPage = myChart.Chart;

            string finalRange = "B" + (elements + 12).ToString();
            chartRange = excelSheet.UsedRange.get_Range("D12", finalRange);
            chartPage.SetSourceData(chartRange, missingValue);
            chartPage.ChartType = Excel.XlChartType.xlLine;

            string excelFile = thrFile.Replace(".csv", ".xls");
            excelBook.SaveAs(excelFile, Excel.XlFileFormat.xlWorkbookNormal, missingValue,
                missingValue, missingValue, missingValue, Excel.XlSaveAsAccessMode.xlExclusive, missingValue,
                missingValue, missingValue, missingValue, missingValue);
            excelBook.Close(true, missingValue, missingValue);
            excelApp.Quit();

            this.releaseObject(excelSheet);
            this.releaseObject(excelBook);
            this.releaseObject(excelApp);
        }

        private void createThrExcelChart2(string thrFile, int elements)
        {
            Excel.Application excelApp = new Excel.Application();
            object missingValue = System.Reflection.Missing.Value;
            Excel.Workbook excelBook = excelApp.Workbooks.Open(thrFile);
            string excelFile = thrFile.Replace(".csv", ".xls");
            excelBook.SaveAs(excelFile, Excel.XlFileFormat.xlWorkbookNormal, missingValue,
                missingValue, missingValue, missingValue, Excel.XlSaveAsAccessMode.xlExclusive);
            this.releaseObject(excelBook);
            this.releaseObject(excelApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception raised while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public void CreateSuperChart()
        {
            Writer writer = new Writer(this.scenario, this.resultsContainer);
            string file = writer.WriteAllThroughputResults(this.storedValuesOfThr, this.storedValuesOfCollisions);
            this.createAllThrExcelChart(file, this.resultsContainer.Throughput.Count);
        }

        private void createAllThrExcelChart(string thrFile, int elements)
        {
            Excel.Application excelApp = new Excel.Application();
            object missingValue = System.Reflection.Missing.Value;
            Excel.Workbook excelBook = excelApp.Workbooks.Open(thrFile);
            Excel.Worksheet excelSheet = (Excel.Worksheet)excelBook.Worksheets.get_Item(1);

            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)excelSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(340, 100, 600, 300);
            Excel.Chart chartPage = myChart.Chart;

            string finalRange = "A" + (elements + 12).ToString();
            chartRange = excelSheet.UsedRange.get_Range("D12", finalRange);

            chartPage.SetSourceData(chartRange, missingValue);


            chartPage.HasTitle = true;
            chartPage.ChartTitle.Text = "Throughput comparison";

            var yAxis = (Excel.Axis)chartPage.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
            yAxis.HasTitle = true;
            yAxis.AxisTitle.Text = "Mean throughput per channel [Mbps]";
            yAxis.AxisTitle.Orientation = Excel.XlOrientation.xlUpward;

            var xAxis = (Excel.Axis)chartPage.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
            xAxis.HasTitle = true;
            xAxis.AxisTitle.Text = "Simulated multi-frames";
            xAxis.AxisTitle.Orientation = Excel.XlOrientation.xlHorizontal;

            chartPage.ChartType = Excel.XlChartType.xlLine;

            string excelFile = thrFile.Replace(".csv", ".xls");
            excelBook.Application.DisplayAlerts = false;
            excelBook.SaveAs(excelFile, Excel.XlFileFormat.xlWorkbookNormal, missingValue,
                missingValue, missingValue, missingValue, Excel.XlSaveAsAccessMode.xlExclusive, missingValue,
                missingValue, missingValue, missingValue, missingValue);
            excelBook.Close(true, missingValue, missingValue);
            excelApp.Quit();

            this.releaseObject(excelSheet);
            this.releaseObject(excelBook);
            this.releaseObject(excelApp);
        }

        #endregion

        #region ------- eam - graphs -------

        private void initMatrixGraphs()
        {
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                if (idx == 0)
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleTdmaGt) || this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleTdmaGt vhTdma = this.scenario.Vehicles[idx] as VehicleTdmaGt;
                        this.g3dUserMatrix1.NewSerie("eam", vhTdma.EstimationMatrix, Color.MediumPurple);
                    }
                }
                else
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleTdmaGt) || this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleTdmaGt vhTdma = this.scenario.Vehicles[idx] as VehicleTdmaGt;
                        this.generateMatrixTab(idx + 1);
                        string currentGraph = "g3dUserMatrix" + (idx + 1).ToString();
                        this.eamGraphs[currentGraph].NewSerie("eam", vhTdma.EstimationMatrix, Color.MediumPurple);
                    }
                }
            }
        }

        private void initHistoryGraphs()
        {
            this.grHistory1.ClearSeries();
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                if (idx == 0)
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        this.grHistory1.NewSerie("history", vhNccma.ExplorationHistory.ToArray(),
                            Color.Sienna, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column);
                    }
                }
                else
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        this.generateHistoryTab(idx + 1);
                        string currentGraph = "grHistory" + (idx + 1).ToString();
                        this.historyGraphs[currentGraph].NewSerie("history", vhNccma.ExplorationHistory.ToArray(),
                            Color.Sienna, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column);
                    }
                }
            }
        }

        private void initCostGraphs()
        {
            this.grCost1.ClearSeries();
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                if (idx == 0)
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        this.grCost1.NewSerie("cost", vhNccma.Cost.ToArray(),
                            Color.DeepSkyBlue, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar);
                    }
                }
                else
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        this.generateCostTab(idx + 1);
                        string currentGraph = "grCost" + (idx + 1).ToString();
                        this.costGraphs[currentGraph].NewSerie("cost", vhNccma.Cost,
                            Color.DeepSkyBlue, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar);
                    }
                }
            }
        }

        private void updateMatrixGraphs()
        {
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                if (idx == 0)
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleTdmaGt) || this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleTdmaGt vhTdma = this.scenario.Vehicles[idx] as VehicleTdmaGt;
                        this.g3dUserMatrix1.ChangeSerie("eam", vhTdma.EstimationMatrix, Color.MediumPurple);
                    }
                }
                else
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleTdmaGt) || this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleTdmaGt vhTdma = this.scenario.Vehicles[idx] as VehicleTdmaGt;
                        string currentGraph = "g3dUserMatrix" + (idx + 1).ToString();
                        this.eamGraphs[currentGraph].ChangeSerie("eam", vhTdma.EstimationMatrix, Color.MediumPurple);
                    }
                }
            }
        }

        private void updateHistoryGraphs()
        {
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                if (idx == 0)
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        this.grHistory1.ChangeSerie("history", vhNccma.ExplorationHistory.ToArray(), Color.Sienna);
                    }
                }
                else
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        string currentGraph = "grHistory" + (idx + 1).ToString();
                        this.historyGraphs[currentGraph].ChangeSerie("history", vhNccma.ExplorationHistory.ToArray(), Color.Sienna);
                    }
                }
            }
        }

        private void updateCostGraphs()
        {
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                if (idx == 0)
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        this.grCost1.ChangeSerie("cost", vhNccma.Cost, Color.DeepSkyBlue);
                    }
                }
                else
                {
                    if (this.scenario.Vehicles[idx].GetType() == typeof(VehicleNccma))
                    {
                        VehicleNccma vhNccma = this.scenario.Vehicles[idx] as VehicleNccma;
                        string currentGraph = "grCost" + (idx + 1).ToString();
                        this.costGraphs[currentGraph].ChangeSerie("cost", vhNccma.Cost, Color.DeepSkyBlue);
                    }
                }
            }
        }

        private void generateHistoryTab(int index)
        {
            // 
            // grHistory
            //
            GraphControlBasic.BasicGraph grHistory = new GraphControlBasic.BasicGraph();
            grHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            grHistory.Location = new System.Drawing.Point(3, 3);
            grHistory.Name = "grHistory" + index.ToString();
            grHistory.Size = new System.Drawing.Size(800, 383);
            grHistory.TabIndex = 0;
            // 
            // tabHistory
            // 
            TabPage tabPage = new TabPage();
            tabPage.Controls.Add(grHistory);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabHistory" + index.ToString();
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(806, 389);
            tabPage.TabIndex = 0;
            tabPage.Text = "User " + index.ToString();
            tabPage.UseVisualStyleBackColor = true;

            //add to tabcontrol
            this.tcHistory.Controls.Add(tabPage);

            this.historyGraphs.Add(grHistory.Name, grHistory);

        }

        private void generateCostTab(int index)
        {
            // 
            // grCost
            //
            GraphControlBasic.BasicGraph grCost = new GraphControlBasic.BasicGraph();
            grCost.Dock = System.Windows.Forms.DockStyle.Fill;
            grCost.Location = new System.Drawing.Point(3, 3);
            grCost.Name = "grCost" + index.ToString();
            grCost.Size = new System.Drawing.Size(800, 383);
            grCost.TabIndex = 0;
            // 
            // tabCost
            // 
            TabPage tabPage = new TabPage();
            tabPage.Controls.Add(grCost);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabCost" + index.ToString();
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(806, 389);
            tabPage.TabIndex = 0;
            tabPage.Text = "User " + index.ToString();
            tabPage.UseVisualStyleBackColor = true;

            //add to tabcontrol
            this.tcExplorationCost.Controls.Add(tabPage);

            this.costGraphs.Add(grCost.Name, grCost);

        }

        private void generateMatrixTab(int index)
        {
            // graph
            GraphControl3d.Graph3d graph3d = new GraphControl3d.Graph3d();
            graph3d.Dock = System.Windows.Forms.DockStyle.Fill;
            graph3d.Location = new System.Drawing.Point(3, 3);
            graph3d.Name = "g3dUserMatrix" + index.ToString();
            graph3d.Size = new System.Drawing.Size(800, 383);
            graph3d.TabIndex = 0;

            // tab
            TabPage tabPage = new TabPage();
            tabPage.Controls.Add(graph3d);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = "tabUser" + index.ToString();
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(806, 389);
            tabPage.TabIndex = 1;
            tabPage.Text = "User " + index.ToString();
            tabPage.UseVisualStyleBackColor = true;

            //add to tabcontrol
            this.tcUsersMatrix.Controls.Add(tabPage);

            this.eamGraphs.Add(graph3d.Name, graph3d);
        }

        private void clearTabMatrix()
        {
            while (this.tcUsersMatrix.TabPages.Count > 1)
            {
                this.tcUsersMatrix.TabPages.RemoveAt(this.tcUsersMatrix.TabPages.Count - 1);
            }
            this.g3dUserMatrix1.ClearSeries();
            this.eamGraphs.Clear();
        }

        private void clearTabHistory()
        {
            while (this.tcHistory.TabPages.Count > 1)
            {
                this.tcHistory.TabPages.RemoveAt(this.tcHistory.TabPages.Count - 1);
            }
            this.grHistory1.ClearSeries();
            this.historyGraphs.Clear();
        }

        private void clearTabCost()
        {
            while (this.tcExplorationCost.TabPages.Count > 1)
            {
                this.tcExplorationCost.TabPages.RemoveAt(this.tcExplorationCost.TabPages.Count - 1);
            }
            this.grCost1.ClearSeries();
            this.costGraphs.Clear();
        }

        #endregion

        #region ------ interferencias ------

        private void keepInterferencesOccupancy()
        {

            int vLimit = this.scenario.MainSpectrum.Occupancy.GetUpperBound(1);
            int hLimit = this.scenario.MainSpectrum.Occupancy.GetUpperBound(0);
            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    this.scenario.MainSpectrum.Occupancy[idxT, idxF] = this.scenario.InterferenceSpectrum.Occupancy[idxT, idxF];
                }
            }
        }

        private void configureInterferences()
        {
            this.specificRegions.Clear();
            this.tableLayoutSpecificRegions.Controls.Clear();
            this.tableLayoutSpecificRegions.ColumnStyles.Clear();


            this.tableLayoutSpecificRegions.ColumnCount = this.scenario.MainSpectrum.TimeSegments;
            this.tableLayoutSpecificRegions.RowCount = 1;
            for (int idxT = 0; idxT < this.scenario.MainSpectrum.TimeSegments; idxT++)
            {
                CheckedListBox checkList = new CheckedListBox();
                checkList.Dock = DockStyle.Fill;
                for (int idxF = 0; idxF < this.scenario.MainSpectrum.FrequencySegments; idxF++)
                {
                    int region = idxT * this.scenario.MainSpectrum.FrequencySegments + idxF + 1;
                    checkList.Items.Add(region.ToString());
                }
                this.specificRegions.Add(idxT.ToString(), checkList);
                this.tableLayoutSpecificRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / this.scenario.MainSpectrum.TimeSegments));
                this.tableLayoutSpecificRegions.Controls.Add(checkList, idxT, 0);
            }
        }

        private void setSpecificInterferences()
        {
            this.scenario.InterferenceSpectrum.Reset();
            foreach (KeyValuePair<string, CheckedListBox> element in this.specificRegions)
            {
                foreach (var item in element.Value.CheckedItems)
                {
                    int access = int.Parse(item.ToString());
                    this.scenario.InterferenceSpectrum.SetOccupancy(access);
                    this.scenario.MainSpectrum.SetOccupancy(access);
                }
            }
        }

        private void setAllFreeRegionsinterferences()
        {
            this.scenario.InterferenceSpectrum.Reset();
            for (int idx = 1; idx <= this.scenario.MainSpectrum.TotalRegions; idx++)
            {
                if (!this.scenario.MainSpectrum.CheckOccupancy(idx))
                {
                    this.scenario.InterferenceSpectrum.SetOccupancy(idx);
                    this.scenario.MainSpectrum.SetOccupancy(idx);
                }
            }
        }

        private void setAllUsersPlusRandomInterferences()
        {
            Random randomNumber = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int numberOfInterferences = this.scenario.MainSpectrum.TotalRegions - this.scenario.Vehicles.Count;
            int counter = 0;
            foreach (Vehicle vehicle in this.scenario.Vehicles)
            {
                if (counter < numberOfInterferences)
                {
                    int access = vehicle.Access;
                    this.scenario.InterferenceSpectrum.SetOccupancy(access);
                    this.scenario.MainSpectrum.SetOccupancy(access);
                    counter++;
                }
            }

            while (counter < numberOfInterferences)
            {
                int access = randomNumber.Next(1, this.scenario.MainSpectrum.TotalRegions);
                if (!this.scenario.MainSpectrum.CheckOccupancy(access))
                {
                    this.scenario.InterferenceSpectrum.SetOccupancy(access);
                    this.scenario.MainSpectrum.SetOccupancy(access);
                    counter++;
                }
            }
        }

        private void setAllButUsersRandomInterferences()
        {
            Random randomNumber = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int numberOfInterferences = this.scenario.MainSpectrum.TotalRegions - this.scenario.Vehicles.Count;
            int counter = 0;
            while (counter < numberOfInterferences)
            {
                int access = randomNumber.Next(1, this.scenario.MainSpectrum.TotalRegions);
                if (!this.scenario.MainSpectrum.CheckOccupancy(access))
                {
                    this.scenario.InterferenceSpectrum.SetOccupancy(access);
                    this.scenario.MainSpectrum.SetOccupancy(access);
                    counter++;
                }
            }
        }

        private void setUsersInterferences()
        {
            for (int idx = 0; idx < this.scenario.Vehicles.Count; idx++)
            {
                this.scenario.InterferenceSpectrum.SetOccupancy(this.scenario.Vehicles[idx].Access);
                this.scenario.MainSpectrum.SetOccupancy(this.scenario.Vehicles[idx].Access);

            }
        }

        #endregion

        #region ------ Special Mode --------

        private void specialMode()
        {
            List<Scenario> autoScenarios = new List<Scenario>();
            autoScenarios.Add(this.setSpecialModeScenario("S_ALOHA", MacTypes.S_ALOHA));
            autoScenarios.Add(this.setSpecialModeScenario("RR_ALOHA", MacTypes.RR_ALOHA));
            autoScenarios.Add(this.setSpecialModeScenario("NCCMA", MacTypes.NCCMA));
            autoScenarios.Add(this.setSpecialModeScenario("ID", MacTypes.ID));
            autoScenarios.Add(this.setSpecialModeScenario("RANDOM", MacTypes.NCCMA));

            this.isAutoMode = true;
            AutoRunner runner = new AutoRunner(this, autoScenarios, SPECIAL_MODE_SIM_DELAY, SPECIAL_MODE_VEHICLES, SPECIAL_MODE_ITERATIONS);
            this.storedValuesOfThr.Clear();
            this.storedValuesOfCollisions.Clear();
            runner.RunScenarios();
        }

        private Scenario setSpecialModeScenario(string title, MacTypes mac)
        {
            Scenario specialModeScenario = new Scenario(title);
            specialModeScenario.MediumAccessControl = mac;
            specialModeScenario.MainMap = new Map(SPECIAL_MODE_X_DIM, SPECIAL_MODE_Y_DIM);
            specialModeScenario.MainSpectrum = new Spectrum(SPECIAL_MODE_TIME_SEGMENTS, SPECIAL_MODE_FREQ_SEGMENTS);
            specialModeScenario.InterferenceSpectrum = new Spectrum(SPECIAL_MODE_TIME_SEGMENTS, SPECIAL_MODE_FREQ_SEGMENTS);
            specialModeScenario.SimulationTime.TimeSegments = SPECIAL_MODE_TIME_SEGMENTS;
            specialModeScenario.SimulationTime.FrequencySegments = SPECIAL_MODE_FREQ_SEGMENTS;
            specialModeScenario.SimulationTime.CyclesPerRegion = SPECIAL_MODE_CYCLES_PER_REGION;
            this.ApplyScenario(specialModeScenario, SPECIAL_MODE_SIM_DELAY, SPECIAL_MODE_VEHICLES);
            this.afterApplyScenarioConfiguration();
            return specialModeScenario;

        }

        #endregion

        #endregion
    }
}
