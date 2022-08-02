using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProductCRUD.Utils
{
    public static class DisplayUtil
    {
        public const string OnlyDateFormat = "dd/MM/yyyy";
        public const string OnlyDayAndMonthFormat = "dd/MM";
        public const string OnlyTimeFormat = @"HH\:mm";
        public const string OnlyTimeWithSecondsFormat = @"HH\:mm\:ss";
        public const string OnlyTimeWithMiliSecondsFormat = @"HH\:mm\:ss.FFF";
        public const string TimespanOnlyTimeFormat = @"hh\:mm";
        public const string TimespanOnlyTimeWithSecondsFormat = @"hh\:mm\:ss";
        public const string TimespanOnlyTimeWithMiliSecondsFormat = @"hh\:mm\:ss.FFF";

        public static string ToDisplay(this double? source)
            => source.HasValue ? source.Value.ToString("#,#0.00") : string.Empty;

        public static string ToDisplay(this double source)
            => source.ToString("#,#0.00");

        public static string ToDisplay(this double source, DoubleDisplayOptions options)
        {
            return options switch
            {
                DoubleDisplayOptions.ZeroDecimals => source.ToString("#0"),
                _ => ToDisplay(source),

            };
        }

        public static string ToDisplay(this decimal source)
            => source.ToString("#,#0.00");

        public static string ToDisplay(this decimal source, DecimalDisplayOptions options)
        {
            return options switch
            {
                DecimalDisplayOptions.ZeroDecimals => source.ToString("#0"),
                _ => ToDisplay(source),
            };
        }

        public static string ToDisplay(this DateTime source)
            => source.ToString("dd/MM/yyyy HH:mm");

        public static string ToDisplay(this DateTime source, DateTimeDisplayOptions options)
        {
            return options switch
            {
                DateTimeDisplayOptions.OnlyDate => source.ToString(OnlyDateFormat),
                DateTimeDisplayOptions.OnlyDayAndMonth => source.ToString(OnlyDayAndMonthFormat),
                DateTimeDisplayOptions.OnlyTime => source.ToString(OnlyTimeFormat),
                DateTimeDisplayOptions.OnlyTimeWithSeconds => source.ToString(OnlyTimeWithSecondsFormat),
                DateTimeDisplayOptions.OnlyTimeWithMiliSeconds => source.ToString(OnlyTimeWithMiliSecondsFormat),
                _ => ToDisplay(source),

            };
        }

        public static string ToDisplay(this DateTime? source)
            => source == null ? string.Empty : source.Value.ToDisplay();

        public static string ToDisplay(this DateTime? source, DateTimeDisplayOptions options)
        {
            if (source == null) return string.Empty;
            return source.Value.ToDisplay(options);
        }


        public static string ToDisplayPercentual(this double source)
            => (source * 100).ToString("#0.##");

        public static string ToDisplay(this TimeSpan source)
            => source.ToString(TimespanOnlyTimeFormat);
        public static string ToDisplay(this TimeSpan source, DateTimeDisplayOptions options)
        {
            return options switch
            {
                DateTimeDisplayOptions.OnlyTime => source.ToString(TimespanOnlyTimeFormat),
                DateTimeDisplayOptions.OnlyTimeWithSeconds => source.ToString(TimespanOnlyTimeWithSecondsFormat),
                DateTimeDisplayOptions.OnlyTimeWithMiliSeconds => source.ToString(TimespanOnlyTimeWithSecondsFormat),
                _ => ToDisplay(source),

            };
        }
    }

    public enum DateTimeDisplayOptions
    {
        Default,
        OnlyDate,
        OnlyDayAndMonth,
        OnlyTime,
        OnlyTimeWithSeconds,
        OnlyTimeWithMiliSeconds,
    }

    public enum DoubleDisplayOptions
    {
        Default,
        ZeroDecimals,
    }

    public enum DecimalDisplayOptions
    {
        Default,
        ZeroDecimals,
    }
}
