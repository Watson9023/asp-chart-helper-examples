using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chartTest
{
    public class KeyValuePair
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }

    public class AspChartHelper
    {
        public enum AspChartType
        {
            Column,
            Bar,
            Line,
            Pie,
            Spline,
            SplineArea
        }

        public string ChartTitle { get; set; }
        public AspChartType aspChartType { get; set; }
        public List<AspChartSeries> xAndyValues { get; set; }
        public bool  AddLegend { get; set; }

        public System.Web.Helpers.Chart GetChart()
        {
            var isChartOk = false;
            var sbError = new System.Text.StringBuilder("");


            var chart = new System.Web.Helpers.Chart(width: 600, height: 400, theme: System.Web.Helpers.ChartTheme.Vanilla)
                        .AddTitle(ChartTitle);

            xAndyValues = xAndyValues ?? new List<AspChartSeries>(); 

            foreach(var series in xAndyValues)
            {
                if(series == null)
                {
                    continue;
                }
                var xValues = series.SeriesValue
                                .Select(kvp => kvp.Key.ToString())
                                .ToArray();
                var yValues = series.SeriesValue
                                .Select(kvp => kvp.Value)
                                .ToArray();
                if((xValues == null ) || (yValues == null))
                {
                    continue;
                }
                else
                {
                    isChartOk = true;
                }
                chart.AddSeries(
                    name: series.SeriesTitle ,
                    chartType: aspChartType.ToString() , 
                    xValue: xValues,
                    yValues: yValues
                );
                
            }

            if (isChartOk)
            {
                if (AddLegend)
                {
                    chart.AddLegend();
                }
            }
            
            return chart;
        }

    }
    public class AspChartSeries
    {
        public string SeriesTitle { get; set; }
        /// <summary>
        /// string for name , object for value
        /// </summary>
        public List<KeyValuePair> SeriesValue { get; set; }
    }
}