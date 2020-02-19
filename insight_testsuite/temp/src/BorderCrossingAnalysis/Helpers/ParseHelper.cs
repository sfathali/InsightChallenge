using System;
using System.Collections.Generic;

namespace BorderCrossingAnalysis.Helpers
{
    public static class ParseHelper
    {
        public static DateTime? ParseDateTime(string dateString)
        {
            if (DateTime.TryParse(dateString, out var newDate))
            {
                return newDate;
            }
            return null;
        }

        public static int ParseInt(string intString)
        {
            if (int.TryParse(intString, out var newInt))
            {
                return newInt;
            }
            return 0;
        }
    }
}
