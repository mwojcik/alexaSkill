using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.Request;

namespace PerfectGym
{
    public static class AlexaExtensions
    {
        public static string ToLogFormattedString(this Slot slot)
        {
            return $"Slot {slot.Name}: {slot.Value}";
        }
    }
}
