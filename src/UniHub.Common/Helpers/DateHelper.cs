using System;
using UniHub.Common.Helpers.Contract;

namespace UniHub.Common.Helpers
{
    public class DateHelper : IDateHelper
    {
        public DateTime GetDateTimeNow() => DateTime.Now;

        public DateTime GetDateTimeUtcNow() => DateTime.UtcNow;
    }
}