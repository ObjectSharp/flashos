using System;
using System.Collections.Generic;
using System.Text;

namespace Flash.Core.Validation
{
    public static class CommonValidators
    {
        public static bool IsValidTicker(string ticker)
        {
            return !string.IsNullOrWhiteSpace(ticker) && ticker.Length == 4;
        }
    }
}
