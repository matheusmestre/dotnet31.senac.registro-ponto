using System;
using System.Collections.Generic;
using System.Text;

namespace Senac.RegistroPonto.Domain.Extensions
{
    public static class DatetimeExtensions
    {
        public static DateTimeOffset ConverterParaTimezone(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZone)
        {
            var offset = timeZone.GetUtcOffset(DateTimeOffset.UtcNow);
            var newDateTimeOffset = dateTimeOffset.ToOffset(offset);

            return newDateTimeOffset;
        }
        public static DateTimeOffset ConverterParaUTC(this DateTimeOffset dateTimeOffset)
        {
            var newDateTimeOffset = dateTimeOffset.ToOffset(TimeSpan.Zero); 
            return newDateTimeOffset;
        }
    }
}
