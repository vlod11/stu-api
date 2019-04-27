using System;
using UniHub.WebApi.BLL.Helpers.Contract;

namespace UniHub.WebApi.BLL.Helpers
{
    public class DateHelper : IDateHelper
    {
        public DateTime GetDateTimeNow() => DateTime.Now;
        public DateTime GetDateTimeUtcNow() => DateTime.UtcNow;
    }
}