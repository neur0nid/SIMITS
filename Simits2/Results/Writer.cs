using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Simits2
{
    class Writer
    {

        #region OOOO PROPERTIES OOOO

        public Scenario Scenario { get; set; }
        public Results ResultsContainer { get; set; }
        public string FieldSeparator { get; private set; }

        #endregion

        #region OOOO BUILDERS OOOOOO
        
        public Writer(Scenario scenario, Results results)
        {
            this.Scenario = scenario;
            this.ResultsContainer = results;
            this.FieldSeparator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public Tuple<string, List<double>, List<int>> WriteThroughputResults()
        {
            string currentFolder = Directory.GetCurrentDirectory();
            string resultsFolder = currentFolder + "\\results";
            if (!Directory.Exists(resultsFolder))
            {
                Directory.CreateDirectory(resultsFolder);
            }
            string file = this.Scenario.Name + 
                "_" + this.Scenario.MainSpectrum.TimeSegments + 
                "_" + this.Scenario.MainSpectrum.FrequencySegments + 
                "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                "_throughput" +
                ".csv";

            string resultsFile = resultsFolder + "\\" + file;

            List<double> thrValues = new List<double>();
            List<int> collisionsValues = new List<int>();


            using (StreamWriter strwrt = new StreamWriter(resultsFile))
            {
                string dateLine = "Throughput results written at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                strwrt.WriteLine(dateLine);
                strwrt.WriteLine();

                string header = "MAC " + this.FieldSeparator +
                    "Time slots " + this.FieldSeparator +
                    "Frequency slots " + this.FieldSeparator +
                    "Number of users " + this.FieldSeparator;

                strwrt.WriteLine(header);

                string headerContent = this.Scenario.Name +
                    this.FieldSeparator + this.Scenario.MainSpectrum.TimeSegments +
                    this.FieldSeparator + this.Scenario.MainSpectrum.FrequencySegments +
                    this.FieldSeparator + this.Scenario.Vehicles.Count;
                strwrt.WriteLine(headerContent);
                strwrt.WriteLine();

                string headerThroughput = "Transfer rate [Mbps] " + this.FieldSeparator +
                    "Modulation " + this.FieldSeparator +
                    "Coding Rate " + this.FieldSeparator + 
                    "Maximum throughput [Mbps] " + this.FieldSeparator;

                strwrt.WriteLine(headerThroughput);
                string throughputContent =
                    this.Scenario.ThroughputConfig.RateConversion[this.Scenario.ThroughputConfig.TransferRate] +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.Modulation.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.CodingConversion[this.Scenario.ThroughputConfig.CodingRate] +
                    this.FieldSeparator + this.ResultsContainer.GetMaxThroughput()[0].ToString();
                strwrt.WriteLine(throughputContent);
                strwrt.WriteLine();

                string headerMessage = "Information size [bytes] " + this.FieldSeparator + 
                    "Total size [bytes] " + this.FieldSeparator +
                    "Time slot duration [us] " + this.FieldSeparator;

                strwrt.WriteLine(headerMessage);
                string messageContent = this.Scenario.ThroughputConfig.MessageInfoSize.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.MessageTotalSize.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.TimeSlotDuration.ToString();
                strwrt.WriteLine(messageContent);
                strwrt.WriteLine();

                string collisionsHeader = "Simulated time slots " + this.FieldSeparator +
                    "Collisions " + this.FieldSeparator +
                    "No trasmissions " + this.FieldSeparator +
                    "Throughput per channel " + this.FieldSeparator;

                strwrt.WriteLine(collisionsHeader);

                int[] dataCollisions = new int[this.ResultsContainer.Collisions.Count];
                dataCollisions = this.ResultsContainer.GetCollisions();
                int[] dataNoTxs = new int[this.ResultsContainer.NoTxs.Count];
                dataNoTxs = this.ResultsContainer.GetNoTxs();
                double[] dataThroughput = new double[this.ResultsContainer.Throughput.Count];
                dataThroughput = this.ResultsContainer.GetThroughput();

                for (int idx = 0; idx < dataCollisions.Length; idx++)
                {
                    string content = idx.ToString() +
                        this.FieldSeparator + dataCollisions[idx].ToString() +
                        this.FieldSeparator + dataNoTxs[idx].ToString() +
                        this.FieldSeparator + dataThroughput[idx].ToString();
                    strwrt.WriteLine(content);
                    thrValues.Add(dataThroughput[idx]);
                    collisionsValues.Add(dataCollisions[idx]);

                }
                strwrt.WriteLine();
            }

            Tuple<string, List<double>, List<int>> retorno = new Tuple<string, List<double>, List<int>>(resultsFile, thrValues, collisionsValues);
            return retorno;

        }

        public string WriteAllThroughputResults(Dictionary<string, List<double>> thrValues, Dictionary<string, List<int>> collisionValues)
        {
            string currentFolder = Directory.GetCurrentDirectory();
            string resultsFolder = currentFolder + "\\results";
            if (!Directory.Exists(resultsFolder))
            {
                Directory.CreateDirectory(resultsFolder);
            }
            string file = "All_Throughputs" +
                "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                ".csv";

            string resultsFile = resultsFolder + "\\" + file;

            using (StreamWriter strwrt = new StreamWriter(resultsFile))
            {
                string dateLine = "All Throughput results written at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                strwrt.WriteLine(dateLine);
                strwrt.WriteLine();

                string header = "MAC " + this.FieldSeparator +
                    "Time slots " + this.FieldSeparator +
                    "Frequency slots " + this.FieldSeparator +
                    "Number of users " + this.FieldSeparator;

                strwrt.WriteLine(header);

                string headerContent = this.Scenario.Name +
                    this.FieldSeparator + this.Scenario.MainSpectrum.TimeSegments +
                    this.FieldSeparator + this.Scenario.MainSpectrum.FrequencySegments +
                    this.FieldSeparator + this.Scenario.Vehicles.Count;
                strwrt.WriteLine(headerContent);
                strwrt.WriteLine();

                string headerThroughput = "Transfer rate [Mbps] " + this.FieldSeparator +
                    "Modulation " + this.FieldSeparator +
                    "Coding Rate " + this.FieldSeparator +
                    "Maximum throughput [Mbps] " + this.FieldSeparator;

                double maximumThroughput = this.ResultsContainer.GetMaxThroughput()[0];
                strwrt.WriteLine(headerThroughput);
                string throughputContent =
                    this.Scenario.ThroughputConfig.RateConversion[this.Scenario.ThroughputConfig.TransferRate] +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.Modulation.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.CodingConversion[this.Scenario.ThroughputConfig.CodingRate] +
                    this.FieldSeparator + maximumThroughput.ToString();
                strwrt.WriteLine(throughputContent);
                strwrt.WriteLine();

                string headerMessage = "Information size [bytes] " + this.FieldSeparator +
                    "Total size [bytes] " + this.FieldSeparator +
                    "Time slot duration [us] " + this.FieldSeparator;

                strwrt.WriteLine(headerMessage);
                string messageContent = this.Scenario.ThroughputConfig.MessageInfoSize.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.MessageTotalSize.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.TimeSlotDuration.ToString();
                strwrt.WriteLine(messageContent);
                strwrt.WriteLine();

                string dataHeader = "Maximum Throughput per multiframe" + this.FieldSeparator;
                foreach (KeyValuePair<string, List<double>> kvp in thrValues)
                {
                    dataHeader += kvp.Key + this.FieldSeparator + "Collisions for " + kvp.Key + this.FieldSeparator;
                }
                strwrt.WriteLine(dataHeader);

                List<string> names = new List<string>();
                foreach (KeyValuePair<string, List<double>> kvp in thrValues)
                {
                    names.Add(kvp.Key);
                }

                string line = string.Empty;
                for (int idx = 0; idx < thrValues[names[0]].Count; idx++)
                {
                    line = maximumThroughput.ToString() + this.FieldSeparator;
                    for (int idx2 = 0; idx2 < names.Count; idx2++)
                    {
                        line += thrValues[names[idx2]][idx] + this.FieldSeparator +
                            collisionValues[names[idx2]][idx] + this.FieldSeparator;
                    }
                    strwrt.WriteLine(line);
                    line = string.Empty;
                }
                strwrt.WriteLine(line);

                strwrt.WriteLine();
            }

            return resultsFile;
        }

        public string WriteAccessResults()
        {
            string currentFolder = Directory.GetCurrentDirectory();
            string resultsFolder = currentFolder + "\\results";
            if (!Directory.Exists(resultsFolder))
            {
                Directory.CreateDirectory(resultsFolder);
            }
            string file = this.Scenario.Name +
                "_" + this.Scenario.MainSpectrum.TimeSegments +
                "_" + this.Scenario.MainSpectrum.FrequencySegments +
                "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                "_access" +
                ".csv";

            string resultsFile = resultsFolder + "\\" + file;

            using (StreamWriter strwrt = new StreamWriter(resultsFile))
            {
                string dateLine = "Access results written at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                strwrt.WriteLine(dateLine);
                strwrt.WriteLine();

                string header = "MAC" + this.FieldSeparator + 
                    "Time slots " + this.FieldSeparator +
                    "Frequency slots " + this.FieldSeparator +
                    "Number of users " + this.FieldSeparator;

                strwrt.WriteLine(header);

                string headerContent = this.Scenario.Name +
                    this.FieldSeparator + this.Scenario.MainSpectrum.TimeSegments +
                    this.FieldSeparator + this.Scenario.MainSpectrum.FrequencySegments +
                    this.FieldSeparator + this.Scenario.Vehicles.Count;
                strwrt.WriteLine(headerContent);
                strwrt.WriteLine();

                string headerThroughput = "Transfer rate [Mbps] " + this.FieldSeparator +
                    "Modulation " + this.FieldSeparator +
                    "Coding Rate " + this.FieldSeparator +
                    "Maximum throughput [Mbps] " + this.FieldSeparator;

                strwrt.WriteLine(headerThroughput);
                string throughputContent =
                    this.Scenario.ThroughputConfig.RateConversion[this.Scenario.ThroughputConfig.TransferRate] +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.Modulation.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.CodingConversion[this.Scenario.ThroughputConfig.CodingRate] +
                    this.FieldSeparator + this.ResultsContainer.GetMaxThroughput()[0].ToString();
                strwrt.WriteLine(throughputContent);
                strwrt.WriteLine();

                string headerMessage = "Information size [bytes] " + this.FieldSeparator +
                    "Total size [bytes] " + this.FieldSeparator +
                    "Time slot duration [us] " + this.FieldSeparator;

                strwrt.WriteLine(headerMessage);
                string messageContent = this.Scenario.ThroughputConfig.MessageInfoSize.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.MessageTotalSize.ToString() +
                    this.FieldSeparator + this.Scenario.ThroughputConfig.TimeSlotDuration.ToString();
                strwrt.WriteLine(messageContent);
                strwrt.WriteLine();

                string regionsHeader = "Vehicle id " + this.FieldSeparator +
                    "Region used " + this.FieldSeparator +
                    "Frame " + this.FieldSeparator +
                    "TxSuccess " + this.FieldSeparator;

                strwrt.WriteLine(regionsHeader);

                foreach (Vehicle vh in this.Scenario.Vehicles)
                {
                    for (int idx = 0; idx < vh.RegionsUsed.Count; idx++)
                    {
                        string content = vh.Id.ToString() +
                            this.FieldSeparator + vh.RegionsUsed[idx].Access.ToString() +
                            this.FieldSeparator + vh.RegionsUsed[idx].Frame.ToString() +
                            this.FieldSeparator + vh.RegionsUsed[idx].SuccessTx.ToString();
                        strwrt.WriteLine(content);
                    }
                }

            }

            return resultsFile;
        }

        #endregion

    }
}
