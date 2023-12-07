namespace TTT.Framework.Utils
{
    public static class StringHelper
    {
        public static bool IsString(this object? value)
        {
            if(value is null) return false;
            return value is string
                    || value is char;
        }
    }
}
