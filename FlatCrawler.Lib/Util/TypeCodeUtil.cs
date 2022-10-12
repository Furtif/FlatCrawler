using System;

namespace FlatCrawler.Lib;

public static class TypeCodeUtil
{
    public const TypeCode Unrecognized = TypeCode.Empty;

    public static (bool AsArray, TypeCode Type) GetTypeCodeTuple(ReadOnlySpan<char> text)
    {
        text = text.Trim();
        bool asArray = false;
        if (text.EndsWith("[]", StringComparison.Ordinal))
        {
            asArray = true;
            text = text[..^2];
        }

        if (text.Equals("table", StringComparison.OrdinalIgnoreCase))
            return (true, TypeCode.Object);
        return (asArray, GetTypeCode(text));
    }

    public static TypeCode GetTypeCode(ReadOnlySpan<char> type) => type switch
    {
        "bool" => TypeCode.Boolean,

        "sbyte" or "s8" => TypeCode.SByte,
        "short" or "s16" => TypeCode.Int16,
        "int" or "s32" => TypeCode.Int32,
        "long" or "s64" => TypeCode.Int64,

        "byte" or "u8" or "i8" => TypeCode.Byte,
        "ushort" or "u16" or "i16" => TypeCode.UInt16,
        "uint" or "u32" or "i32" => TypeCode.UInt32,
        "ulong" or "u64" or "i64" => TypeCode.UInt64,

        "float" => TypeCode.Single,
        "double" => TypeCode.Double,

        "string" or "str" => TypeCode.String,
        "object" or "obj" or "table" => TypeCode.Object,
        _ => Unrecognized,
    };

    public static bool IsValidNodeType(this TypeCode type) => type is not Unrecognized;
}
