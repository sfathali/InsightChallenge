using System;
using System.IO;
using BorderCrossingAnalysis.Constants;

namespace BorderCrossingAnalysis.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateInputPaths(string[] inputPaths)
        {
            // all input file paths validation goes here
            if (inputPaths.Length < 2)
            {
                Console.WriteLine(Messages.Error.SpecifyPaths);
                return false;
            }

            var inputPath = inputPaths[0];

            if (string.IsNullOrEmpty(inputPath))
            {
                Console.WriteLine(Messages.Error.InvalidInputFilePath);
                return false;
            }

            if (!File.Exists(inputPath))
            {
                Console.WriteLine(Messages.Error.InvalidInputFilePath);
                return false;
            }

            var inputFileExtension = Path.GetExtension(inputPath);

            if (!inputFileExtension.ToLower().Contains(Constans.CSVFileExtension))
            {
                Console.WriteLine(Messages.Error.InvalidInputExtension);
            }

            return true;
        }
    }
}
