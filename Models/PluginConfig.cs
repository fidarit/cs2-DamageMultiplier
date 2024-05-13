using CounterStrikeSharp.API.Core;
using System.Collections.Generic;

namespace DamageMultiplierCS2.Models
{
    public class PluginConfig : BasePluginConfig
    {
        public Dictionary<string, PermissionData> Permissions { get; set; } = new()
        {
            { "*", new() }
        };
    }
}
