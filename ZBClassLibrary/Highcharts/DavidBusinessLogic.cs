using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZbClassLibrary.Highcharts
{
    public class DavidBusinessLogic
    {

        //折线图
        public HighChartOptions getline(string renderTo, string Xtitle, string Ytitle, List<string> categories, List<Series> series)
        {
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                chart = new Chart()
                {
                    renderTo = renderTo,
                    type = ChartTypeEnum.pie.ToString()
                },
                title = new Title() { text = Xtitle },
                xAxis = new List<XAxis>() { 
                    new XAxis(){
                        categories = categories,
                        reversed = false,
                        opposite = false
                    }
                },
                yAxis = new YAxis() { title = new Title() { text = Ytitle } },
                tooltip = new ToolTip() { crosshairs = new List<bool>() { true, true }, shared = true },
                series = series
            };
            return currentChart;
        }
        /// <summary>
        /// 饼图
        /// </summary>
        public HighChartOptions getpie(string renderTo, string Xtitle, List<Series> series)
        {
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                chart = new Chart()
                {
                    renderTo = renderTo,
                    type = ChartTypeEnum.pie.ToString()
                },
                title = new Title() { text = Xtitle },
                //</b><br/>{series.name}:{point.y}
                tooltip = new ToolTip() { pointFormat = "项目数量: <b>{point.y}"},

                series = series
            };
            return currentChart;
        }
        /// <summary>
        /// 多柱状图
        /// </summary>
        public HighChartOptions getMulticolumn(string renderTo, string Xtitle, string Ytitle, List<String> categories, List<Series> series)
        {
            HighChartOptions currentChart = new HighChartOptions();
            currentChart = new HighChartOptions()
            {
                chart = new Chart()
                {
                    renderTo = renderTo
                },
                title = new Title() { text = Xtitle },
                xAxis = new List<XAxis>() { 
                    new XAxis(){
                        categories = categories,
                        reversed = false,
                        opposite = false
                    }
                },
                yAxis = new YAxis() { title = new Title() { text = Ytitle } },
                tooltip = new ToolTip() { crosshairs = new List<bool>() { true, false } },
                series = series
            };
            return currentChart;
        }
    }
}