using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Viridisca.Modules.Identity.Application.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        
        if (field == null)
            return value.ToString();
            
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        
        return attribute?.Description ?? value.ToString();
    }
} 