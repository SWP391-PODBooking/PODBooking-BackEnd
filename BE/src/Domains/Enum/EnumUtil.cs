namespace BE.src.Domains.Enum
{
    public static class EnumUtil
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }
    }
}
