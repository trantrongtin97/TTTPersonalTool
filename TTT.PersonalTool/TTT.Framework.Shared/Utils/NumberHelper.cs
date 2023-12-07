namespace TTT.Framework.Utils;

public static class NumberHelper
{
    public static bool IsNumber(this object? value)
    {
        if (value is null) return false;
        return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
    }

    public static bool IsValidSByte(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not sbyte) return false;
        var d = (sbyte)obj;
        return (d <= sbyte.MaxValue && d >= sbyte.MinValue);
    }

    public static bool IsValidByte(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not byte) return false;
        var d = (byte)obj;
        return (d <= byte.MaxValue && d >= byte.MinValue);
    }

    public static bool IsValidShort(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not short) return false;
        var d = (short)obj;
        return (d <= short.MaxValue && d >= short.MinValue);
    }

    public static bool IsValidUshort(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not ushort) return false;
        var d = (ushort)obj;
        return (d <= ushort.MaxValue && d >= ushort.MinValue);
    }

    public static bool IsValidInt(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not int) return false;
        var d = (int)obj;
        return (d <= int.MaxValue && d >= int.MinValue);
    }

    public static bool IsValidUint(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not uint) return false;
        var d = (uint)obj;
        return (d <= uint.MaxValue && d >= uint.MinValue);
    }

    public static bool IsValidLong(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not long) return false;
        var d = (long)obj;
        return (d <= long.MaxValue && d >= long.MinValue);
    }

    public static bool IsValidUlong(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not ulong) return false;
        var d = (ulong)obj;
        return (d <= ulong.MaxValue && d >= ulong.MinValue);
    }

    public static bool IsValidFloat(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not float) return false;
        var d = (float)obj;
        return (d <= float.MaxValue && d >= float.MinValue);
    }

    public static bool IsValidDouble(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not double) return false;
        var d = (double)obj;
        return (d <= double.MaxValue && d >= double.MinValue);
    }

    public static bool IsValidDecimal(this object? obj)
    {
        if (obj is null) return false;
        if (obj is not decimal) return false;
        var d = (decimal)obj;
        return (d <= decimal.MaxValue && d >= decimal.MinValue);
    }

    public static bool ValidateNumberic(this object? obj)
    {
        if (obj is null) return false;
        if (obj is sbyte) 
        { 
            return IsValidSByte(obj);
        }
        if (obj is byte) 
        {
            return IsValidByte(obj);
        }
        if (obj is short) 
        { 
            return IsValidShort(obj);
        }
        if (obj is ushort) 
        {
            return IsValidUshort(obj);
        }
        if (obj is int) 
        { 
            return IsValidInt(obj);
        }
        if (obj is uint) 
        { 
            return IsValidUint(obj);
        }
        if (obj is long) 
        { 
            return IsValidLong(obj);
        }
        if (obj is ulong) 
        { 
            return IsValidUlong(obj);
        }
        if (obj is float) 
        { 
            return IsValidFloat(obj);
        }
        if (obj is double) 
        { 
            return IsValidDouble(obj);
        }
        if (obj is decimal) 
        { 
            return IsValidDecimal(obj);
        }
        return false;
    }
}
