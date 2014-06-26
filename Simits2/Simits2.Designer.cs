namespace Simits2
{
    partial class Simits2
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tpScenario = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboCoding = new System.Windows.Forms.ComboBox();
            this.comboModulation = new System.Windows.Forms.ComboBox();
            this.comboTransfer = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.currentRegionText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.simulatedRegionsText = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.simulatedFramesText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.simulatedCyclesText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbSlotDuration = new System.Windows.Forms.TextBox();
            this.tbTotalSize = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numMessageSize = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gpMapConfig = new System.Windows.Forms.GroupBox();
            this.numYSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numXSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.gpSpectrumConfig = new System.Windows.Forms.GroupBox();
            this.numFreqSlots = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numTimeSlots = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.gpVehiclesConfig = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.vehiclesMacTypes = new System.Windows.Forms.ComboBox();
            this.numVehicles = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gpInfoConfig = new System.Windows.Forms.GroupBox();
            this.leaderTrajectories = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gpMacConfig = new System.Windows.Forms.GroupBox();
            this.gpTimeConfig = new System.Windows.Forms.GroupBox();
            this.numCyclesPerFrame = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numSimDelay = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tpMap = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grCollisionMap = new GraphControl3d.Graph3d();
            this.grSpectrumMap = new GraphControl3d.Graph3d();
            this.grMapMap = new GraphControlBasic.BasicGraph();
            this.tabVehicles = new System.Windows.Forms.TabPage();
            this.gridVehicles = new System.Windows.Forms.DataGridView();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.graphCollisions = new GraphControlBasic.BasicGraph();
            this.graphThroughput = new GraphControlBasic.BasicGraph();
            this.tabInterferences = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.grInterferences = new GraphControl3d.Graph3d();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btSetSpecIntrf = new System.Windows.Forms.Button();
            this.tableLayoutSpecificRegions = new System.Windows.Forms.TableLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btSetCurrentInterferences = new System.Windows.Forms.Button();
            this.btSetAllRandomInterferences2 = new System.Windows.Forms.Button();
            this.btClearInterferences = new System.Windows.Forms.Button();
            this.btSetAllRandomInterferences = new System.Windows.Forms.Button();
            this.btSetAllInterferences = new System.Windows.Forms.Button();
            this.tabMatrix = new System.Windows.Forms.TabPage();
            this.tcUsersMatrix = new System.Windows.Forms.TabControl();
            this.tabUser1 = new System.Windows.Forms.TabPage();
            this.g3dUserMatrix1 = new GraphControl3d.Graph3d();
            this.tabHistories = new System.Windows.Forms.TabPage();
            this.tcHistory = new System.Windows.Forms.TabControl();
            this.tabHistory1 = new System.Windows.Forms.TabPage();
            this.grHistory1 = new GraphControlBasic.BasicGraph();
            this.tabCosts = new System.Windows.Forms.TabPage();
            this.tcExplorationCost = new System.Windows.Forms.TabControl();
            this.tabUser1Cost = new System.Windows.Forms.TabPage();
            this.grCost1 = new GraphControlBasic.BasicGraph();
            this.btStartSimulation = new System.Windows.Forms.Button();
            this.btApplyScenario = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.btStop = new System.Windows.Forms.Button();
            this.btWrite = new System.Windows.Forms.Button();
            this.btSpecial = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.tpScenario.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMessageSize)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.gpMapConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXSize)).BeginInit();
            this.gpSpectrumConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFreqSlots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeSlots)).BeginInit();
            this.gpVehiclesConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVehicles)).BeginInit();
            this.gpInfoConfig.SuspendLayout();
            this.gpTimeConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCyclesPerFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSimDelay)).BeginInit();
            this.tpMap.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVehicles)).BeginInit();
            this.tabResults.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabInterferences.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabMatrix.SuspendLayout();
            this.tcUsersMatrix.SuspendLayout();
            this.tabUser1.SuspendLayout();
            this.tabHistories.SuspendLayout();
            this.tcHistory.SuspendLayout();
            this.tabHistory1.SuspendLayout();
            this.tabCosts.SuspendLayout();
            this.tcExplorationCost.SuspendLayout();
            this.tabUser1Cost.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.tpScenario);
            this.mainTabControl.Controls.Add(this.tpMap);
            this.mainTabControl.Controls.Add(this.tabVehicles);
            this.mainTabControl.Controls.Add(this.tabResults);
            this.mainTabControl.Controls.Add(this.tabInterferences);
            this.mainTabControl.Controls.Add(this.tabMatrix);
            this.mainTabControl.Controls.Add(this.tabHistories);
            this.mainTabControl.Controls.Add(this.tabCosts);
            this.mainTabControl.Location = new System.Drawing.Point(12, 41);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(828, 447);
            this.mainTabControl.TabIndex = 1;
            // 
            // tpScenario
            // 
            this.tpScenario.Controls.Add(this.tableLayoutPanel4);
            this.tpScenario.Controls.Add(this.tableLayoutPanel1);
            this.tpScenario.Location = new System.Drawing.Point(4, 22);
            this.tpScenario.Name = "tpScenario";
            this.tpScenario.Padding = new System.Windows.Forms.Padding(3);
            this.tpScenario.Size = new System.Drawing.Size(820, 421);
            this.tpScenario.TabIndex = 0;
            this.tpScenario.Text = "Scenario";
            this.tpScenario.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(476, 6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.71318F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.28682F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(338, 409);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.comboCoding);
            this.groupBox2.Controls.Add(this.comboModulation);
            this.groupBox2.Controls.Add(this.comboTransfer);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 130);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Throughput configuration";
            // 
            // comboCoding
            // 
            this.comboCoding.Enabled = false;
            this.comboCoding.FormattingEnabled = true;
            this.comboCoding.Items.AddRange(new object[] {
            "1/2",
            "2/3",
            "3/4"});
            this.comboCoding.Location = new System.Drawing.Point(135, 86);
            this.comboCoding.Name = "comboCoding";
            this.comboCoding.Size = new System.Drawing.Size(75, 21);
            this.comboCoding.TabIndex = 8;
            // 
            // comboModulation
            // 
            this.comboModulation.Enabled = false;
            this.comboModulation.FormattingEnabled = true;
            this.comboModulation.Items.AddRange(new object[] {
            "BPSK",
            "QPSK",
            "16-QAM",
            "64-QAM"});
            this.comboModulation.Location = new System.Drawing.Point(135, 59);
            this.comboModulation.Name = "comboModulation";
            this.comboModulation.Size = new System.Drawing.Size(75, 21);
            this.comboModulation.TabIndex = 7;
            // 
            // comboTransfer
            // 
            this.comboTransfer.FormattingEnabled = true;
            this.comboTransfer.Items.AddRange(new object[] {
            "3",
            "4.5",
            "6",
            "9",
            "12",
            "18",
            "24",
            "27"});
            this.comboTransfer.Location = new System.Drawing.Point(135, 32);
            this.comboTransfer.Name = "comboTransfer";
            this.comboTransfer.Size = new System.Drawing.Size(75, 21);
            this.comboTransfer.TabIndex = 6;
            this.comboTransfer.SelectedIndexChanged += new System.EventHandler(this.comboTransfer_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Modulation:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 89);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Coding rate:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 35);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(112, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Transfer rate [Mbps]:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.currentRegionText);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.simulatedRegionsText);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.simulatedFramesText);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.simulatedCyclesText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(3, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 145);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulation summary";
            // 
            // currentRegionText
            // 
            this.currentRegionText.Location = new System.Drawing.Point(126, 113);
            this.currentRegionText.Name = "currentRegionText";
            this.currentRegionText.ReadOnly = true;
            this.currentRegionText.Size = new System.Drawing.Size(60, 22);
            this.currentRegionText.TabIndex = 7;
            this.currentRegionText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Current region:";
            // 
            // simulatedRegionsText
            // 
            this.simulatedRegionsText.Location = new System.Drawing.Point(126, 59);
            this.simulatedRegionsText.Name = "simulatedRegionsText";
            this.simulatedRegionsText.ReadOnly = true;
            this.simulatedRegionsText.Size = new System.Drawing.Size(60, 22);
            this.simulatedRegionsText.TabIndex = 5;
            this.simulatedRegionsText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Simulated regions:";
            // 
            // simulatedFramesText
            // 
            this.simulatedFramesText.Location = new System.Drawing.Point(126, 86);
            this.simulatedFramesText.Name = "simulatedFramesText";
            this.simulatedFramesText.ReadOnly = true;
            this.simulatedFramesText.Size = new System.Drawing.Size(60, 22);
            this.simulatedFramesText.TabIndex = 3;
            this.simulatedFramesText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Simulated frames:";
            // 
            // simulatedCyclesText
            // 
            this.simulatedCyclesText.Location = new System.Drawing.Point(126, 32);
            this.simulatedCyclesText.Name = "simulatedCyclesText";
            this.simulatedCyclesText.ReadOnly = true;
            this.simulatedCyclesText.Size = new System.Drawing.Size(60, 22);
            this.simulatedCyclesText.TabIndex = 1;
            this.simulatedCyclesText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Simulated cycles:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbSlotDuration);
            this.groupBox3.Controls.Add(this.tbTotalSize);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.numMessageSize);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 139);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 116);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message configuration";
            // 
            // tbSlotDuration
            // 
            this.tbSlotDuration.Location = new System.Drawing.Point(200, 81);
            this.tbSlotDuration.Name = "tbSlotDuration";
            this.tbSlotDuration.ReadOnly = true;
            this.tbSlotDuration.Size = new System.Drawing.Size(75, 22);
            this.tbSlotDuration.TabIndex = 10;
            this.tbSlotDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTotalSize
            // 
            this.tbTotalSize.Location = new System.Drawing.Point(200, 53);
            this.tbTotalSize.Name = "tbTotalSize";
            this.tbTotalSize.ReadOnly = true;
            this.tbTotalSize.Size = new System.Drawing.Size(75, 22);
            this.tbTotalSize.TabIndex = 9;
            this.tbTotalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(17, 84);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 13);
            this.label19.TabIndex = 8;
            this.label19.Text = "Time slot duration [us]";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(140, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Message total size [bytes]:";
            // 
            // numMessageSize
            // 
            this.numMessageSize.Location = new System.Drawing.Point(200, 26);
            this.numMessageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMessageSize.Name = "numMessageSize";
            this.numMessageSize.Size = new System.Drawing.Size(75, 22);
            this.numMessageSize.TabIndex = 5;
            this.numMessageSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numMessageSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numMessageSize.ValueChanged += new System.EventHandler(this.numMessageSize_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Message information size [bytes]:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gpMapConfig, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gpSpectrumConfig, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gpVehiclesConfig, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gpInfoConfig, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gpMacConfig, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.gpTimeConfig, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 409);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gpMapConfig
            // 
            this.gpMapConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpMapConfig.Controls.Add(this.numYSize);
            this.gpMapConfig.Controls.Add(this.label4);
            this.gpMapConfig.Controls.Add(this.numXSize);
            this.gpMapConfig.Controls.Add(this.label3);
            this.gpMapConfig.Location = new System.Drawing.Point(3, 3);
            this.gpMapConfig.Name = "gpMapConfig";
            this.gpMapConfig.Size = new System.Drawing.Size(458, 55);
            this.gpMapConfig.TabIndex = 0;
            this.gpMapConfig.TabStop = false;
            this.gpMapConfig.Text = "Map configuration";
            // 
            // numYSize
            // 
            this.numYSize.Location = new System.Drawing.Point(211, 23);
            this.numYSize.Name = "numYSize";
            this.numYSize.Size = new System.Drawing.Size(51, 22);
            this.numYSize.TabIndex = 7;
            this.numYSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numYSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y size [m]";
            // 
            // numXSize
            // 
            this.numXSize.Location = new System.Drawing.Point(84, 23);
            this.numXSize.Name = "numXSize";
            this.numXSize.Size = new System.Drawing.Size(51, 22);
            this.numXSize.TabIndex = 5;
            this.numXSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numXSize.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "X size [m]";
            // 
            // gpSpectrumConfig
            // 
            this.gpSpectrumConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpSpectrumConfig.Controls.Add(this.numFreqSlots);
            this.gpSpectrumConfig.Controls.Add(this.label9);
            this.gpSpectrumConfig.Controls.Add(this.numTimeSlots);
            this.gpSpectrumConfig.Controls.Add(this.label2);
            this.gpSpectrumConfig.Location = new System.Drawing.Point(3, 64);
            this.gpSpectrumConfig.Name = "gpSpectrumConfig";
            this.gpSpectrumConfig.Size = new System.Drawing.Size(458, 55);
            this.gpSpectrumConfig.TabIndex = 1;
            this.gpSpectrumConfig.TabStop = false;
            this.gpSpectrumConfig.Text = "Spectrum configuration";
            // 
            // numFreqSlots
            // 
            this.numFreqSlots.Location = new System.Drawing.Point(377, 23);
            this.numFreqSlots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFreqSlots.Name = "numFreqSlots";
            this.numFreqSlots.Size = new System.Drawing.Size(51, 22);
            this.numFreqSlots.TabIndex = 7;
            this.numFreqSlots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFreqSlots.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(225, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Number of frequency slots:";
            // 
            // numTimeSlots
            // 
            this.numTimeSlots.Location = new System.Drawing.Point(148, 23);
            this.numTimeSlots.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTimeSlots.Name = "numTimeSlots";
            this.numTimeSlots.Size = new System.Drawing.Size(51, 22);
            this.numTimeSlots.TabIndex = 5;
            this.numTimeSlots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTimeSlots.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of time slots:";
            // 
            // gpVehiclesConfig
            // 
            this.gpVehiclesConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpVehiclesConfig.Controls.Add(this.label6);
            this.gpVehiclesConfig.Controls.Add(this.vehiclesMacTypes);
            this.gpVehiclesConfig.Controls.Add(this.numVehicles);
            this.gpVehiclesConfig.Controls.Add(this.label1);
            this.gpVehiclesConfig.Location = new System.Drawing.Point(3, 125);
            this.gpVehiclesConfig.Name = "gpVehiclesConfig";
            this.gpVehiclesConfig.Size = new System.Drawing.Size(458, 55);
            this.gpVehiclesConfig.TabIndex = 2;
            this.gpVehiclesConfig.TabStop = false;
            this.gpVehiclesConfig.Text = "Vehicles configuration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(225, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Type of MAC:";
            // 
            // vehiclesMacTypes
            // 
            this.vehiclesMacTypes.FormattingEnabled = true;
            this.vehiclesMacTypes.Location = new System.Drawing.Point(305, 20);
            this.vehiclesMacTypes.Name = "vehiclesMacTypes";
            this.vehiclesMacTypes.Size = new System.Drawing.Size(123, 21);
            this.vehiclesMacTypes.TabIndex = 5;
            this.vehiclesMacTypes.SelectedIndexChanged += new System.EventHandler(this.vehiclesMacTypes_SelectedIndexChanged);
            // 
            // numVehicles
            // 
            this.numVehicles.Location = new System.Drawing.Point(148, 21);
            this.numVehicles.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVehicles.Name = "numVehicles";
            this.numVehicles.Size = new System.Drawing.Size(51, 22);
            this.numVehicles.TabIndex = 3;
            this.numVehicles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numVehicles.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numVehicles.ValueChanged += new System.EventHandler(this.numVehicles_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of vehicles:";
            // 
            // gpInfoConfig
            // 
            this.gpInfoConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpInfoConfig.Controls.Add(this.leaderTrajectories);
            this.gpInfoConfig.Controls.Add(this.label5);
            this.gpInfoConfig.Location = new System.Drawing.Point(3, 186);
            this.gpInfoConfig.Name = "gpInfoConfig";
            this.gpInfoConfig.Size = new System.Drawing.Size(458, 55);
            this.gpInfoConfig.TabIndex = 3;
            this.gpInfoConfig.TabStop = false;
            this.gpInfoConfig.Text = "Vehicles information configuration";
            // 
            // leaderTrajectories
            // 
            this.leaderTrajectories.FormattingEnabled = true;
            this.leaderTrajectories.Location = new System.Drawing.Point(211, 22);
            this.leaderTrajectories.Name = "leaderTrajectories";
            this.leaderTrajectories.Size = new System.Drawing.Size(217, 21);
            this.leaderTrajectories.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Type of leader trajectory:";
            // 
            // gpMacConfig
            // 
            this.gpMacConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpMacConfig.Location = new System.Drawing.Point(3, 247);
            this.gpMacConfig.Name = "gpMacConfig";
            this.gpMacConfig.Size = new System.Drawing.Size(458, 96);
            this.gpMacConfig.TabIndex = 4;
            this.gpMacConfig.TabStop = false;
            this.gpMacConfig.Text = "Medium access control configuration";
            // 
            // gpTimeConfig
            // 
            this.gpTimeConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpTimeConfig.Controls.Add(this.numCyclesPerFrame);
            this.gpTimeConfig.Controls.Add(this.label13);
            this.gpTimeConfig.Controls.Add(this.numSimDelay);
            this.gpTimeConfig.Controls.Add(this.label8);
            this.gpTimeConfig.Location = new System.Drawing.Point(3, 349);
            this.gpTimeConfig.Name = "gpTimeConfig";
            this.gpTimeConfig.Size = new System.Drawing.Size(458, 57);
            this.gpTimeConfig.TabIndex = 5;
            this.gpTimeConfig.TabStop = false;
            this.gpTimeConfig.Text = "Simulation time configuration";
            // 
            // numCyclesPerFrame
            // 
            this.numCyclesPerFrame.Location = new System.Drawing.Point(249, 27);
            this.numCyclesPerFrame.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCyclesPerFrame.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numCyclesPerFrame.Name = "numCyclesPerFrame";
            this.numCyclesPerFrame.Size = new System.Drawing.Size(51, 22);
            this.numCyclesPerFrame.TabIndex = 9;
            this.numCyclesPerFrame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCyclesPerFrame.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(153, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Cycles per frame";
            // 
            // numSimDelay
            // 
            this.numSimDelay.Location = new System.Drawing.Point(84, 27);
            this.numSimDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSimDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSimDelay.Name = "numSimDelay";
            this.numSimDelay.Size = new System.Drawing.Size(51, 22);
            this.numSimDelay.TabIndex = 7;
            this.numSimDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSimDelay.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Delay [ms]";
            // 
            // tpMap
            // 
            this.tpMap.Controls.Add(this.tableLayoutPanel2);
            this.tpMap.Location = new System.Drawing.Point(4, 22);
            this.tpMap.Name = "tpMap";
            this.tpMap.Padding = new System.Windows.Forms.Padding(3);
            this.tpMap.Size = new System.Drawing.Size(820, 421);
            this.tpMap.TabIndex = 1;
            this.tpMap.Text = "Map";
            this.tpMap.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.grMapMap, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(808, 438);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.grCollisionMap, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.grSpectrumMap, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(568, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(237, 432);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // grCollisionMap
            // 
            this.grCollisionMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grCollisionMap.Location = new System.Drawing.Point(3, 219);
            this.grCollisionMap.Name = "grCollisionMap";
            this.grCollisionMap.Size = new System.Drawing.Size(231, 210);
            this.grCollisionMap.TabIndex = 2;
            // 
            // grSpectrumMap
            // 
            this.grSpectrumMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grSpectrumMap.Location = new System.Drawing.Point(3, 3);
            this.grSpectrumMap.Name = "grSpectrumMap";
            this.grSpectrumMap.Size = new System.Drawing.Size(231, 210);
            this.grSpectrumMap.TabIndex = 1;
            // 
            // grMapMap
            // 
            this.grMapMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grMapMap.Location = new System.Drawing.Point(3, 3);
            this.grMapMap.Name = "grMapMap";
            this.grMapMap.Size = new System.Drawing.Size(559, 432);
            this.grMapMap.TabIndex = 1;
            // 
            // tabVehicles
            // 
            this.tabVehicles.Controls.Add(this.gridVehicles);
            this.tabVehicles.Location = new System.Drawing.Point(4, 22);
            this.tabVehicles.Name = "tabVehicles";
            this.tabVehicles.Padding = new System.Windows.Forms.Padding(3);
            this.tabVehicles.Size = new System.Drawing.Size(820, 421);
            this.tabVehicles.TabIndex = 2;
            this.tabVehicles.Text = "Vehicles";
            this.tabVehicles.UseVisualStyleBackColor = true;
            // 
            // gridVehicles
            // 
            this.gridVehicles.AllowUserToAddRows = false;
            this.gridVehicles.AllowUserToDeleteRows = false;
            this.gridVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVehicles.Location = new System.Drawing.Point(3, 3);
            this.gridVehicles.Name = "gridVehicles";
            this.gridVehicles.Size = new System.Drawing.Size(814, 415);
            this.gridVehicles.TabIndex = 0;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.tableLayoutPanel5);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults.Size = new System.Drawing.Size(820, 421);
            this.tabResults.TabIndex = 3;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.graphCollisions, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.graphThroughput, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(814, 415);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // graphCollisions
            // 
            this.graphCollisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphCollisions.Location = new System.Drawing.Point(3, 3);
            this.graphCollisions.Name = "graphCollisions";
            this.graphCollisions.Size = new System.Drawing.Size(808, 201);
            this.graphCollisions.TabIndex = 0;
            // 
            // graphThroughput
            // 
            this.graphThroughput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphThroughput.Location = new System.Drawing.Point(3, 210);
            this.graphThroughput.Name = "graphThroughput";
            this.graphThroughput.Size = new System.Drawing.Size(808, 202);
            this.graphThroughput.TabIndex = 1;
            // 
            // tabInterferences
            // 
            this.tabInterferences.Controls.Add(this.tableLayoutPanel6);
            this.tabInterferences.Location = new System.Drawing.Point(4, 22);
            this.tabInterferences.Name = "tabInterferences";
            this.tabInterferences.Padding = new System.Windows.Forms.Padding(3);
            this.tabInterferences.Size = new System.Drawing.Size(820, 421);
            this.tabInterferences.TabIndex = 5;
            this.tabInterferences.Text = "Interferences";
            this.tabInterferences.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.grInterferences, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(814, 415);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // grInterferences
            // 
            this.grInterferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grInterferences.Location = new System.Drawing.Point(410, 3);
            this.grInterferences.Name = "grInterferences";
            this.grInterferences.Size = new System.Drawing.Size(401, 409);
            this.grInterferences.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(401, 409);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btSetSpecIntrf);
            this.tabPage1.Controls.Add(this.tableLayoutSpecificRegions);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(393, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Specific regions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btSetSpecIntrf
            // 
            this.btSetSpecIntrf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetSpecIntrf.Location = new System.Drawing.Point(312, 354);
            this.btSetSpecIntrf.Name = "btSetSpecIntrf";
            this.btSetSpecIntrf.Size = new System.Drawing.Size(75, 23);
            this.btSetSpecIntrf.TabIndex = 1;
            this.btSetSpecIntrf.Text = "Set";
            this.btSetSpecIntrf.UseVisualStyleBackColor = true;
            this.btSetSpecIntrf.Click += new System.EventHandler(this.btSetSpecIntrf_Click);
            // 
            // tableLayoutSpecificRegions
            // 
            this.tableLayoutSpecificRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutSpecificRegions.ColumnCount = 2;
            this.tableLayoutSpecificRegions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutSpecificRegions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutSpecificRegions.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutSpecificRegions.Name = "tableLayoutSpecificRegions";
            this.tableLayoutSpecificRegions.RowCount = 1;
            this.tableLayoutSpecificRegions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutSpecificRegions.Size = new System.Drawing.Size(381, 342);
            this.tableLayoutSpecificRegions.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btSetCurrentInterferences);
            this.tabPage2.Controls.Add(this.btSetAllRandomInterferences2);
            this.tabPage2.Controls.Add(this.btClearInterferences);
            this.tabPage2.Controls.Add(this.btSetAllRandomInterferences);
            this.tabPage2.Controls.Add(this.btSetAllInterferences);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(393, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fixed regions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btSetCurrentInterferences
            // 
            this.btSetCurrentInterferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetCurrentInterferences.Location = new System.Drawing.Point(6, 298);
            this.btSetCurrentInterferences.Name = "btSetCurrentInterferences";
            this.btSetCurrentInterferences.Size = new System.Drawing.Size(381, 23);
            this.btSetCurrentInterferences.TabIndex = 6;
            this.btSetCurrentInterferences.Text = "Set interferences in current access regions";
            this.btSetCurrentInterferences.UseVisualStyleBackColor = true;
            this.btSetCurrentInterferences.Click += new System.EventHandler(this.btSetCurrentInterferences_Click);
            // 
            // btSetAllRandomInterferences2
            // 
            this.btSetAllRandomInterferences2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetAllRandomInterferences2.Location = new System.Drawing.Point(6, 251);
            this.btSetAllRandomInterferences2.Name = "btSetAllRandomInterferences2";
            this.btSetAllRandomInterferences2.Size = new System.Drawing.Size(381, 41);
            this.btSetAllRandomInterferences2.TabIndex = 5;
            this.btSetAllRandomInterferences2.Text = "Set all but users n random interferences , being n total regions - number of user" +
    "s";
            this.btSetAllRandomInterferences2.UseVisualStyleBackColor = true;
            this.btSetAllRandomInterferences2.Click += new System.EventHandler(this.btSetAllRandomInterferences2_Click);
            // 
            // btClearInterferences
            // 
            this.btClearInterferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearInterferences.Location = new System.Drawing.Point(6, 354);
            this.btClearInterferences.Name = "btClearInterferences";
            this.btClearInterferences.Size = new System.Drawing.Size(381, 23);
            this.btClearInterferences.TabIndex = 4;
            this.btClearInterferences.Text = "Clear interferences";
            this.btClearInterferences.UseVisualStyleBackColor = true;
            this.btClearInterferences.Click += new System.EventHandler(this.btClearInterferences_Click);
            // 
            // btSetAllRandomInterferences
            // 
            this.btSetAllRandomInterferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetAllRandomInterferences.Location = new System.Drawing.Point(6, 6);
            this.btSetAllRandomInterferences.Name = "btSetAllRandomInterferences";
            this.btSetAllRandomInterferences.Size = new System.Drawing.Size(381, 23);
            this.btSetAllRandomInterferences.TabIndex = 3;
            this.btSetAllRandomInterferences.Text = "Set random interferences , being n total regions - number of users";
            this.btSetAllRandomInterferences.UseVisualStyleBackColor = true;
            this.btSetAllRandomInterferences.Click += new System.EventHandler(this.btSetAllRandomInterferences_Click);
            // 
            // btSetAllInterferences
            // 
            this.btSetAllInterferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btSetAllInterferences.Location = new System.Drawing.Point(6, 179);
            this.btSetAllInterferences.Name = "btSetAllInterferences";
            this.btSetAllInterferences.Size = new System.Drawing.Size(381, 23);
            this.btSetAllInterferences.TabIndex = 2;
            this.btSetAllInterferences.Text = "Set interferences in all free regions";
            this.btSetAllInterferences.UseVisualStyleBackColor = true;
            this.btSetAllInterferences.Click += new System.EventHandler(this.btSetAllInterferences_Click);
            // 
            // tabMatrix
            // 
            this.tabMatrix.Controls.Add(this.tcUsersMatrix);
            this.tabMatrix.Location = new System.Drawing.Point(4, 22);
            this.tabMatrix.Name = "tabMatrix";
            this.tabMatrix.Padding = new System.Windows.Forms.Padding(3);
            this.tabMatrix.Size = new System.Drawing.Size(820, 421);
            this.tabMatrix.TabIndex = 4;
            this.tabMatrix.Text = "Estimation matrix";
            this.tabMatrix.UseVisualStyleBackColor = true;
            // 
            // tcUsersMatrix
            // 
            this.tcUsersMatrix.Controls.Add(this.tabUser1);
            this.tcUsersMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcUsersMatrix.Location = new System.Drawing.Point(3, 3);
            this.tcUsersMatrix.Name = "tcUsersMatrix";
            this.tcUsersMatrix.SelectedIndex = 0;
            this.tcUsersMatrix.Size = new System.Drawing.Size(814, 415);
            this.tcUsersMatrix.TabIndex = 0;
            // 
            // tabUser1
            // 
            this.tabUser1.Controls.Add(this.g3dUserMatrix1);
            this.tabUser1.Location = new System.Drawing.Point(4, 22);
            this.tabUser1.Name = "tabUser1";
            this.tabUser1.Padding = new System.Windows.Forms.Padding(3);
            this.tabUser1.Size = new System.Drawing.Size(806, 389);
            this.tabUser1.TabIndex = 1;
            this.tabUser1.Text = "User 1";
            this.tabUser1.UseVisualStyleBackColor = true;
            // 
            // g3dUserMatrix1
            // 
            this.g3dUserMatrix1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.g3dUserMatrix1.Location = new System.Drawing.Point(3, 3);
            this.g3dUserMatrix1.Name = "g3dUserMatrix1";
            this.g3dUserMatrix1.Size = new System.Drawing.Size(800, 383);
            this.g3dUserMatrix1.TabIndex = 0;
            // 
            // tabHistories
            // 
            this.tabHistories.Controls.Add(this.tcHistory);
            this.tabHistories.Location = new System.Drawing.Point(4, 22);
            this.tabHistories.Name = "tabHistories";
            this.tabHistories.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistories.Size = new System.Drawing.Size(820, 421);
            this.tabHistories.TabIndex = 6;
            this.tabHistories.Text = "Exploration history";
            this.tabHistories.UseVisualStyleBackColor = true;
            // 
            // tcHistory
            // 
            this.tcHistory.Controls.Add(this.tabHistory1);
            this.tcHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHistory.Location = new System.Drawing.Point(3, 3);
            this.tcHistory.Name = "tcHistory";
            this.tcHistory.SelectedIndex = 0;
            this.tcHistory.Size = new System.Drawing.Size(814, 415);
            this.tcHistory.TabIndex = 0;
            // 
            // tabHistory1
            // 
            this.tabHistory1.Controls.Add(this.grHistory1);
            this.tabHistory1.Location = new System.Drawing.Point(4, 22);
            this.tabHistory1.Name = "tabHistory1";
            this.tabHistory1.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory1.Size = new System.Drawing.Size(806, 389);
            this.tabHistory1.TabIndex = 0;
            this.tabHistory1.Text = "User 1";
            this.tabHistory1.UseVisualStyleBackColor = true;
            // 
            // grHistory1
            // 
            this.grHistory1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grHistory1.Location = new System.Drawing.Point(3, 3);
            this.grHistory1.Name = "grHistory1";
            this.grHistory1.Size = new System.Drawing.Size(800, 383);
            this.grHistory1.TabIndex = 0;
            // 
            // tabCosts
            // 
            this.tabCosts.Controls.Add(this.tcExplorationCost);
            this.tabCosts.Location = new System.Drawing.Point(4, 22);
            this.tabCosts.Name = "tabCosts";
            this.tabCosts.Padding = new System.Windows.Forms.Padding(3);
            this.tabCosts.Size = new System.Drawing.Size(820, 421);
            this.tabCosts.TabIndex = 7;
            this.tabCosts.Text = "Exploration Cost";
            this.tabCosts.UseVisualStyleBackColor = true;
            // 
            // tcExplorationCost
            // 
            this.tcExplorationCost.Controls.Add(this.tabUser1Cost);
            this.tcExplorationCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcExplorationCost.Location = new System.Drawing.Point(3, 3);
            this.tcExplorationCost.Name = "tcExplorationCost";
            this.tcExplorationCost.SelectedIndex = 0;
            this.tcExplorationCost.Size = new System.Drawing.Size(814, 415);
            this.tcExplorationCost.TabIndex = 1;
            // 
            // tabUser1Cost
            // 
            this.tabUser1Cost.Controls.Add(this.grCost1);
            this.tabUser1Cost.Location = new System.Drawing.Point(4, 22);
            this.tabUser1Cost.Name = "tabUser1Cost";
            this.tabUser1Cost.Padding = new System.Windows.Forms.Padding(3);
            this.tabUser1Cost.Size = new System.Drawing.Size(806, 389);
            this.tabUser1Cost.TabIndex = 0;
            this.tabUser1Cost.Text = "User 1";
            this.tabUser1Cost.UseVisualStyleBackColor = true;
            // 
            // grCost1
            // 
            this.grCost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grCost1.Location = new System.Drawing.Point(3, 3);
            this.grCost1.Name = "grCost1";
            this.grCost1.Size = new System.Drawing.Size(800, 383);
            this.grCost1.TabIndex = 0;
            // 
            // btStartSimulation
            // 
            this.btStartSimulation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStartSimulation.Location = new System.Drawing.Point(593, 494);
            this.btStartSimulation.Name = "btStartSimulation";
            this.btStartSimulation.Size = new System.Drawing.Size(75, 23);
            this.btStartSimulation.TabIndex = 2;
            this.btStartSimulation.Text = "Start";
            this.btStartSimulation.UseVisualStyleBackColor = true;
            this.btStartSimulation.Click += new System.EventHandler(this.btStartSimulation_Click);
            // 
            // btApplyScenario
            // 
            this.btApplyScenario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btApplyScenario.Location = new System.Drawing.Point(674, 494);
            this.btApplyScenario.Name = "btApplyScenario";
            this.btApplyScenario.Size = new System.Drawing.Size(75, 23);
            this.btApplyScenario.TabIndex = 1;
            this.btApplyScenario.Text = "Apply";
            this.btApplyScenario.UseVisualStyleBackColor = true;
            this.btApplyScenario.Click += new System.EventHandler(this.btApplyScenario_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Location = new System.Drawing.Point(755, 494);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(75, 23);
            this.btClose.TabIndex = 3;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // panelStatus
            // 
            this.panelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatus.Controls.Add(this.labelStatus);
            this.panelStatus.Location = new System.Drawing.Point(12, 12);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(828, 23);
            this.panelStatus.TabIndex = 4;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(6, 4);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(38, 13);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "label6";
            // 
            // btStop
            // 
            this.btStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btStop.Location = new System.Drawing.Point(512, 494);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 5;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // btWrite
            // 
            this.btWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btWrite.Location = new System.Drawing.Point(431, 494);
            this.btWrite.Name = "btWrite";
            this.btWrite.Size = new System.Drawing.Size(75, 23);
            this.btWrite.TabIndex = 6;
            this.btWrite.Text = "Write";
            this.btWrite.UseVisualStyleBackColor = true;
            this.btWrite.Click += new System.EventHandler(this.btWrite_Click);
            // 
            // btSpecial
            // 
            this.btSpecial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSpecial.Location = new System.Drawing.Point(350, 494);
            this.btSpecial.Name = "btSpecial";
            this.btSpecial.Size = new System.Drawing.Size(75, 23);
            this.btSpecial.TabIndex = 7;
            this.btSpecial.Text = "Special";
            this.btSpecial.UseVisualStyleBackColor = true;
            this.btSpecial.Click += new System.EventHandler(this.btSpecial_Click);
            // 
            // Simits2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(852, 529);
            this.Controls.Add(this.btSpecial);
            this.Controls.Add(this.btWrite);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btStartSimulation);
            this.Controls.Add(this.btApplyScenario);
            this.Controls.Add(this.mainTabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Simits2";
            this.Text = "SimITS2";
            this.mainTabControl.ResumeLayout(false);
            this.tpScenario.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMessageSize)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gpMapConfig.ResumeLayout(false);
            this.gpMapConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXSize)).EndInit();
            this.gpSpectrumConfig.ResumeLayout(false);
            this.gpSpectrumConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFreqSlots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeSlots)).EndInit();
            this.gpVehiclesConfig.ResumeLayout(false);
            this.gpVehiclesConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVehicles)).EndInit();
            this.gpInfoConfig.ResumeLayout(false);
            this.gpInfoConfig.PerformLayout();
            this.gpTimeConfig.ResumeLayout(false);
            this.gpTimeConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCyclesPerFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSimDelay)).EndInit();
            this.tpMap.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabVehicles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVehicles)).EndInit();
            this.tabResults.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tabInterferences.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabMatrix.ResumeLayout(false);
            this.tcUsersMatrix.ResumeLayout(false);
            this.tabUser1.ResumeLayout(false);
            this.tabHistories.ResumeLayout(false);
            this.tcHistory.ResumeLayout(false);
            this.tabHistory1.ResumeLayout(false);
            this.tabCosts.ResumeLayout(false);
            this.tcExplorationCost.ResumeLayout(false);
            this.tabUser1Cost.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tpScenario;
        private System.Windows.Forms.TabPage tpMap;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gpMapConfig;
        private System.Windows.Forms.NumericUpDown numYSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numXSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gpSpectrumConfig;
        private System.Windows.Forms.NumericUpDown numFreqSlots;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numTimeSlots;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpVehiclesConfig;
        private System.Windows.Forms.NumericUpDown numVehicles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpInfoConfig;
        private System.Windows.Forms.GroupBox gpMacConfig;
        private System.Windows.Forms.GroupBox gpTimeConfig;
        private System.Windows.Forms.Button btApplyScenario;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private GraphControl3d.Graph3d grSpectrumMap;
        private GraphControlBasic.BasicGraph grMapMap;
        private System.Windows.Forms.ComboBox leaderTrajectories;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btStartSimulation;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.TabPage tabVehicles;
        private System.Windows.Forms.DataGridView gridVehicles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox vehiclesMacTypes;
        private System.Windows.Forms.NumericUpDown numSimDelay;
        private System.Windows.Forms.Label label8;
        private GraphControl3d.Graph3d grCollisionMap;
        private System.Windows.Forms.NumericUpDown numCyclesPerFrame;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox currentRegionText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox simulatedRegionsText;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox simulatedFramesText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox simulatedCyclesText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboCoding;
        private System.Windows.Forms.ComboBox comboModulation;
        private System.Windows.Forms.ComboBox comboTransfer;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numMessageSize;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbSlotDuration;
        private System.Windows.Forms.TextBox tbTotalSize;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private GraphControlBasic.BasicGraph graphCollisions;
        private GraphControlBasic.BasicGraph graphThroughput;
        private System.Windows.Forms.Button btWrite;
        private System.Windows.Forms.TabPage tabMatrix;
        private System.Windows.Forms.TabControl tcUsersMatrix;
        private System.Windows.Forms.TabPage tabUser1;
        private GraphControl3d.Graph3d g3dUserMatrix1;
        private System.Windows.Forms.TabPage tabInterferences;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private GraphControl3d.Graph3d grInterferences;
        private System.Windows.Forms.TabPage tabHistories;
        private System.Windows.Forms.TabControl tcHistory;
        private System.Windows.Forms.TabPage tabHistory1;
        private GraphControlBasic.BasicGraph grHistory1;
        private System.Windows.Forms.TabPage tabCosts;
        private System.Windows.Forms.TabControl tcExplorationCost;
        private System.Windows.Forms.TabPage tabUser1Cost;
        private GraphControlBasic.BasicGraph grCost1;
        private System.Windows.Forms.Button btSpecial;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btSetSpecIntrf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutSpecificRegions;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btClearInterferences;
        private System.Windows.Forms.Button btSetAllRandomInterferences;
        private System.Windows.Forms.Button btSetAllInterferences;
        private System.Windows.Forms.Button btSetAllRandomInterferences2;
        private System.Windows.Forms.Button btSetCurrentInterferences;
    }
}

