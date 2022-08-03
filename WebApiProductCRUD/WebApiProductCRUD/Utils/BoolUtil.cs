namespace WebApiProductCRUD.Utils
{
    public static class BoolUtil
    {
        public static bool NullTo(this bool? source)
        {
            if (null == source)
                return false;
            return source.Value;
        }

        public static bool NullTo(this bool? source, bool defaultValue)
        {
            if (null == source)
                return defaultValue;
            return source.Value;
        }
    }
}
