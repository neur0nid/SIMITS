using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphControl3d
{
    public partial class Graph3d : UserControl
    {
        #region OOOO CONSTRUCTOR OOOOOO

        public Graph3d()
        {
            InitializeComponent();
        }

        #endregion

        #region OOOO PUBLICS OOOOOOO

        public void SetTitle(String titletext)
        {
            this.chart.Titles.Clear();
            this.chart.Titles.Add(titletext);
        }

        public void SetXAxisTitle(String titletext)
        {
            this.chart.ChartAreas[0].AxisX.Title = titletext;
        }

        public void SetYAxisTitle(String titletext)
        {
            this.chart.ChartAreas[0].AxisY.Title = titletext;
        }

        public void SetSize(double xSize, double ySize)
        {
            this.chart.ChartAreas[0].AxisX.Minimum = -1;
            this.chart.ChartAreas[0].AxisX.Maximum = xSize;

            this.chart.ChartAreas[0].AxisY.Minimum = 0;
            this.chart.ChartAreas[0].AxisY.Maximum = ySize;
        }

        public void SetSize(double xSize, double ySize, double zSize)
        {
            this.chart.ChartAreas[0].AxisX.Minimum = -1;
            this.chart.ChartAreas[0].AxisX.Maximum = xSize;

            this.chart.ChartAreas[0].AxisY.Minimum = 0;
            this.chart.ChartAreas[0].AxisY.Maximum = zSize;

            this.chart.ChartAreas[0].AxisX2.Minimum = 0;
            this.chart.ChartAreas[0].AxisX2.Maximum = ySize;
        }

        public void NewSerie(String id, double[,] yData, Color colorSerie)
        {
            int vLimit = yData.GetUpperBound(1);
            int hLimit = yData.GetUpperBound(0);
            double[] fila = new double[hLimit + 1];

            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    fila[idxT] = yData[idxT, idxF];
                }
                this.newSerie(id + idxF.ToString(), fila, colorSerie);
            }
        }

        public void ChangeSerie(String id, double[,] yData, Color colorSerie)
        {
            int vLimit = yData.GetUpperBound(1);
            int hLimit = yData.GetUpperBound(0);
            double[] fila = new double[hLimit + 1];
            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    fila[idxT] = yData[idxT, idxF];
                }
                this.changeSerie(id + idxF.ToString(), fila, colorSerie);
            }
        }

        public void NewSerie(String id, int[,] yData, Color colorSerie)
        {
            int vLimit = yData.GetUpperBound(1);
            int hLimit = yData.GetUpperBound(0);
            int[] fila = new int[hLimit+1];
            
            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    fila[idxT] = yData[idxT, idxF];
                }
                this.newSerie(id + idxF.ToString(), fila, colorSerie);
            }
        }

        public void ChangeSerie(String id, int[,] yData, Color colorSerie)
        {
            int vLimit = yData.GetUpperBound(1);
            int hLimit = yData.GetUpperBound(0);
            int[] fila = new int[hLimit+1];

            for (int idxF = 0; idxF <= vLimit; idxF++)
            {
                for (int idxT = 0; idxT <= hLimit; idxT++)
                {
                    fila[idxT] = yData[idxT, idxF];
                }
                this.changeSerie(id + idxF.ToString(), fila, colorSerie);
            }
        }

        public void NewPoint(String id, double x_data, double y_data, Color colorSerie)
        {
            this.chart.Series.Add(id);
            this.chart.Series[id].ChartType = SeriesChartType.Point;
            this.chart.Series[id].ChartArea = "ChartArea1";
            this.chart.Series[id].Color = colorSerie;
            this.chart.Series[id].MarkerStyle = MarkerStyle.Square;

            this.chart.Series[id].Points.AddXY(x_data, y_data);

            this.chart.Series[id].Points[0].Label = id;
            this.chart.Series[id].Points[0].MarkerSize = 8;

        }

        public void ChangePoint(String id, double x_data, double y_data, Color colorSerie)
        {
            if (colorSerie != Color.Empty)
                this.chart.Series[id].Color = colorSerie;

            this.chart.Series[id].Points.Clear();
            this.chart.Series[id].Points.AddXY(x_data, y_data);
            this.chart.Series[id].Points[0].Label = id;
            this.chart.Series[id].Points[0].MarkerSize = 8;
        }

        public void ClearSeries()
        {
            this.chart.Series.Clear();
        }

        #endregion

        #region OOOO PRIVATES OOOOOO

        private void changeSerie(String id, int[] y_data, Color colorSerie)
        {
            if (colorSerie != Color.Empty)
                this.chart.Series[id].Color = colorSerie;

            this.chart.Series[id].Points.Clear();

            int yy;
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                this.chart.Series[id].Points.AddXY(idx, yy);
            }
        }

        private void changeSerie(String id, double[] y_data, Color colorSerie)
        {
            if (colorSerie != Color.Empty)
                this.chart.Series[id].Color = colorSerie;
            this.chart.Series[id].Points.Clear();
            double yy;
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                this.chart.Series[id].Points.AddXY(idx, yy);
            }
        }


        private void newSerie(String id, int[] y_data, Color colorSerie)
        {
            this.chart.Series.Add(id);
            this.chart.Series[id].ChartType = SeriesChartType.Column;
            this.chart.Series[id].ChartArea = "ChartArea1";
            this.chart.Series[id].Color = colorSerie;

            int yy;
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                this.chart.Series[id].Points.AddXY(idx + 1, yy);
            }

            this.chart.Legends[0].Enabled = false;

        }

        private void newSerie(String id, double[] y_data, Color colorSerie)
        {
            this.chart.Series.Add(id);
            this.chart.Series[id].ChartType = SeriesChartType.Column;
            this.chart.Series[id].ChartArea = "ChartArea1";
            this.chart.Series[id].Color = colorSerie;

            double yy;
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                this.chart.Series[id].Points.AddXY(idx + 1, yy);
            }

            this.chart.Legends[0].Enabled = false;

        }

        #endregion

    }
}
