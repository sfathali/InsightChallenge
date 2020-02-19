using System;
using System.Collections.Generic;
using BorderCrossingAnalysis.Models;

namespace BorderCrossingAnalysis.Helpers
{
    public static class AnalysisHelper
    {
        public static int CalculateAverage(DateTime? forDate, Dictionary<DateTime?, int> dateDictionary)
        {
            var average = 0;
            var i = 0;
            if (forDate.HasValue)
            {
                foreach (var key in dateDictionary.Keys)
                {
                    if (key.HasValue)
                    {
                        if (forDate.Value.Year == key.Value.Year)
                        {
                            if (forDate.Value.Month > key.Value.Month)
                            {
                                average += dateDictionary[key];
                                i++;
                            }
                        }
                    }
                }
            }

            if (average > 0)
            {
                average = Convert.ToInt32(average / i);
            }
            return average;
        }

        public static List<OutputModel> BuildOutputModel(Dictionary<string, Dictionary<string, Dictionary<DateTime?, int>>> dictionary)
        {
            var listOutput = new List<OutputModel>();
            foreach (var t in dictionary.Keys)
            {
                var measure = t;
                foreach (var r in dictionary[t].Keys)
                {
                    var port = r;
                    var b = dictionary[t][r];
                    foreach (var k in b)
                    {
                        var average = AnalysisHelper.CalculateAverage(k.Key, b);
                        var model = new OutputModel { Date = k.Key, Value = k.Value, Border = port, Measure = measure, Average = average };
                        listOutput.Add(model);
                    }
                }
            }

            return listOutput;
        }
    }
}
