using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BorderCrossingAnalysis.Constants;
using BorderCrossingAnalysis.Helpers;

namespace BorderCrossingAnalysis
{
    static class BorderCrossingAnalysis
    {
        static void Main(string[] args)
        {
            if (!ValidationHelper.ValidateInputPaths(args))
            {
                DefaultConsoleMessage();
                return;
            }
            var inputDictionary = new Dictionary<string, Dictionary<string, Dictionary<DateTime?, int>>>();
            try
            {
                using (var stream = File.OpenRead(args[0]))
                using (var reader = new StreamReader(stream))
                {
                    var data = CsvParser.ParseHeadAndTail(reader, ',', '"');
                    var inputLines = data.Item2;

                    foreach (var line in inputLines)
                    {
                        var measure = line[5];
                        var border = line[3];
                        var date = ParseHelper.ParseDateTime(line[4]);
                        var value = ParseHelper.ParseInt(line[6]);
                        if (inputDictionary.TryGetValue(measure, out var portDictionary))
                        {
                            if (portDictionary.TryGetValue(border, out var dateDictionary))
                            {
                                if (dateDictionary.TryGetValue(date ?? throw new InvalidOperationException(),
                                    out var valueFromDictionary))
                                {
                                    dateDictionary[date] = valueFromDictionary + value;
                                }
                                else
                                {
                                    dateDictionary.Add(date, value);
                                }
                            }
                            else
                            {
                                var dateDic = new Dictionary<DateTime?, int> { { date, value } };
                                portDictionary.Add(border, dateDic);
                            }
                        }
                        else
                        {
                            var dateDictionary = new Dictionary<DateTime?, int> { { date, value } };
                            var portDic = new Dictionary<string, Dictionary<DateTime?, int>> { { border, dateDictionary } };
                            inputDictionary.Add(measure, portDic);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var listOutput = AnalysisHelper.BuildOutputModel(inputDictionary);
                listOutput.Sort();
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(GetHeaderLine());
                foreach (var te in listOutput)
                {
                    var date = te.Date.HasValue ? te.Date.Value.ToString("MM/dd/yyyy hh:mm:ss tt") : Messages.Error.InvalidDate;
                    stringBuilder.AppendLine($"{te.Border},{date},{te.Measure},{te.Value},{te.Average}");

                }
                File.WriteAllText(args[1], stringBuilder.ToString());
        }

        private static void DefaultConsoleMessage()
        {
            Console.WriteLine(Messages.Default.PressAnyKey);
            Console.ReadKey();
        }

        private static string GetHeaderLine()
        {
            return $"{Header.Border},{Header.Date},{Header.Measure},{Header.Value},{Header.Average}";
        }
    }
}
