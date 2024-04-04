using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace GodotViews.VirtualGrid;

internal static class NullableData
{
    public static NullableData<T> Null<T>() => new(false, default);
    public static NullableData<T> Create<T>(T data) => new(true, data); 
}

internal readonly struct NullableData<T>(bool hasValue, T? value)
{
    internal static void Clear(ref readonly Span<NullableData<T>> array)
    {
        foreach (ref var element in array)
        {
            element = NullableData.Null<T>();
        }
    }
    
    public readonly bool IsNull = !hasValue;

    public bool TryUnwrap([NotNullWhen(true)] out T? data)
    {
        if (IsNull)
        {
            data = default;
            return false;
        }

        data = value!;
        return true;
    }
    
    public T Unwrap()
    {
        ArgumentOutOfRangeException.ThrowIfEqual(IsNull, true);
        return value!;
    }

    public override string ToString()
    {
        if (IsNull) return $"Null({typeof(T).Name})";
        return value!.ToString()!;
    }
}