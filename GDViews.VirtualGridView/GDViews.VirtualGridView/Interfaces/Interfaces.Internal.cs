using System;
using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

internal readonly struct NullableData<T>(bool hasValue, T? value)
{
    public static void Clear(ref readonly Span<NullableData<T>> array)
    {
        foreach (ref var element in array)
        {
            element = Null;
        }
    }
    
    public static NullableData<T> Null { get; } = new(false, default);
    
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

internal interface IDataInspector<T>
{
    int ViewRowCount { get; }

    ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset);
}