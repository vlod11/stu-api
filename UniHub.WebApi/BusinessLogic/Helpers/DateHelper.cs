using System;
using UniHub.WebApi.BusinessLogic.Helpers.Contract;

namespace UniHub.WebApi.BusinessLogic.Helpers
{
    public class DateHelper : IDateHelper
    {
        public DateTime GetDateTimeNow() => DateTime.Now;
        public DateTime GetDateTimeUtcNow() => DateTime.UtcNow;
    }
}