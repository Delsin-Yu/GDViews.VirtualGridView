using System.Runtime.InteropServices;

namespace GodotViews.VirtualGrid;

/// <summary>
/// A struct that used as a <see cref="System.Void"/> type in extra arguments.
/// </summary>
[StructLayout(LayoutKind.Explicit, Size = 0)]
public struct NoExtraArgument
{
    /// <summary>
    /// The default value for <see cref="NoExtraArgument"/>.
    /// </summary>
    public static readonly NoExtraArgument Default = new();
}