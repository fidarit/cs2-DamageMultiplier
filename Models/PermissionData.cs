using System.Collections.Generic;

namespace DamageMultiplierCS2.Models
{
    public class PermissionData
    {
        public Dictionary<string, float> Weapons { get; set; } = new()
        {
            {"*", 1f},
            {"hegrenade", 2f}
        };
    }
}
