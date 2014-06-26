using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphControlBasic
{
    public partial class BasicGraph : UserControl
    {
		#region Constructor
		
        public BasicGraph()
        {
            InitializeComponent();
        }
		
		#endregion
		
		#region Publics

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
            this.chart.ChartAreas[0].AxisX.Minimum = 0;
            this.chart.ChartAreas[0].AxisX.Maximum = xSize;

            this.chart.ChartAreas[0].AxisY.Minimum = 0;
            this.chart.ChartAreas[0].AxisY.Maximum = ySize;
        }

        public void NewSerie(String id, int[] y_data, Color colorSerie, SeriesChartType chartType)
        {
            this.chart.Series.Add(id);
            this.chart.Series[id].ChartType = chartType; ;
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

        public void NewSerie(String id, int[] y_data, Color colorSerie)
        {
            this.NewSerie(id, y_data, colorSerie, SeriesChartType.Column);
        }

        public void NewSerie(String id, double[] y_data, Color colorSerie)
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

        public void NewBubble(String id, double[] x_data, double[] y_data, Color colorSerie)
        {
            this.chart.Series.Add(id);
            
            this.chart.Series[id].ChartType = SeriesChartType.Funnel;
            this.chart.Series[id].ChartArea = "ChartArea1";
            this.chart.Series[id].Color = colorSerie;
            
            double yy,xx;
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                xx = x_data[idx];
                this.chart.Series[id].Points.AddXY(xx, yy);
            }

            this.chart.Legends[0].Enabled = true;

        }

        public void NewSerie(String id, double[] y_data, Color colorSerie, SeriesChartType sType)
        {
            this.chart.Series.Add(id);
            this.chart.Series[id].ChartType = sType;
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

        public void ChangeSerie(String id, double[] y_data, Color colorSerie, SeriesChartType sType)
        {
            this.chart.Series[id].Points.Clear();
            double yy;
            this.chart.Series[id].Points.Clear();
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                this.chart.Series[id].Points.AddXY(idx + 1, yy);
            }

            this.chart.Legends[0].Enabled = false;
        }

        public void ChangeSerie(String id, int[] y_data, Color colorSerie, SeriesChartType sType)
        {
            this.chart.Series[id].Points.Clear();
            int yy;
            for (int idx = 0; idx < y_data.Length; idx++)
            {
                yy = y_data[idx];
                this.chart.Series[id].Points.AddXY(idx + 1, yy);
            }

            this.chart.Legends[0].Enabled = false;
        }

        public void ChangeSerie(String id, int[] y_data, Color colorSerie)
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

        public void ChangeSerie(String id, double[] y_data, Color colorSerie)
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

        public void NewBubble2(String id, double x_data, double y_data, Color colorSerie)
        {
            this.chart.Series.Add(id);
            this.chart.Series[id].ChartType = SeriesChartType.Bubble;
            this.chart.Series[id].ChartArea = "ChartArea1";
            this.chart.Series[id].Color = colorSerie;
            
            this.chart.Series[id].Points.AddXY(x_data, y_data, 50);

            
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
    }
}
