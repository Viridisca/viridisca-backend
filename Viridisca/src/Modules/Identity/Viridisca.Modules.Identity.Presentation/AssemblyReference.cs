using System.Reflection;

namespace Viridisca.Modules.Identity.Presentation;

/// <summary>
/// Reference to the assembly to make it available for reflection
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
} 