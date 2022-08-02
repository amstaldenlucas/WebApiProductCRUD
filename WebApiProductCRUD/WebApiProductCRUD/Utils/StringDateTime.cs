namespace WebApiProductCRUD.Utils
{
    public class StringDateTime
    {
        public static DateTime Parse(string value)
            => Parse(value, new DateTime());

        public static DateTime Parse(string value, DateTime defaultValue)
        {
            if (value != null && value.Length == 14)
            {
                if (int.TryParse(value.Substring(0, 4), out int year)
                    && int.TryParse(value.Substring(4, 2), out int month)
                    && int.TryParse(value.Substring(6, 2), out int day)
                    && int.TryParse(value.Substring(8, 2), out int hour)
                    && int.TryParse(value.Substring(10, 2), out int min)
                    && int.TryParse(value.Substring(12, 2), out int sec))
                {
                    var result = new DateTime(year, month, day, hour, min, sec, DateTimeKind.Utc);
                    return result.AddSeconds(1);
                }
            }

            if (value != null && value.Length == 8)
            {
                if (int.TryParse(value.Substring(0, 4), out int year)
                    && int.TryParse(value.Substring(4, 2), out int month)
                    && int.TryParse(value.Substring(6, 2), out int day))
                {
                    return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
                }
            }

            return defaultValue;
        }
    }
}
