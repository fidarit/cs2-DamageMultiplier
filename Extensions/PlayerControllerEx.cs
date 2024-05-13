using CounterStrikeSharp.API.Core;

namespace DamageMultiplierCS2.Extensions
{
    internal static class PlayerControllerEx
    {
        public static bool IsValid(this CCSPlayerController player)
        {
            return player is { IsValid: true, IsBot: false, PawnIsAlive: true };
        }
    }
}
