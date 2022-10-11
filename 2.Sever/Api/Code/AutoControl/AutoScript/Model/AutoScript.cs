using System;
using System.Collections.Generic;

namespace XNYAPI.AutoControl.Script.Model
{
    public struct DataStream_Double
    {
        public List<double> points;

        public DataStream_Double(List<double> points)
        {
            this.points = points;
        }

        public double Avg()
        {
            double sum = 0;
            foreach (var pt in points)
                sum += pt ;
            return sum / points.Count;
        }

    }
    public struct DataStream_TimedDouble
    {
        public List<ValueTuple<DateTime, double>> points;

        public DataStream_TimedDouble(List<ValueTuple<DateTime, double>> points)
        {
            this.points = points;
        }

        public double Avg()
        {
            double sum = 0;
            foreach (var pt in points)
                sum += pt.Item2;
            return sum / points.Count;
        }

    }
    public class PageItem
    {
        public string Name;
        public string[] Parm;
    }
    public class ScriptPage
    {
        public int Step;
        public PageItem[] Items;
    }
    public class AutoScript
    {
        public ScriptPage[] Pages;
    }

}

