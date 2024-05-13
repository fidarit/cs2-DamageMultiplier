using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using DamageMultiplierCS2.Models;
using System;
using System.Linq;

namespace DamageMultiplierCS2.Controllers
{
    internal class DamageController : BaseController
    {
        public DamageController(Plugin plugin) : base(plugin)
        {
            plugin.RegisterEventHandler<EventPlayerHurt>(EventPlayerHurtHandler, HookMode.Pre);
        }

        private HookResult EventPlayerHurtHandler(EventPlayerHurt eventInfo, GameEventInfo gameEventInfo)
        {
            var attacker = eventInfo.Attacker;
            var victim = eventInfo.Userid?.PlayerPawn?.Value;
            var weapon = eventInfo.Weapon;

            if (eventInfo.DmgHealth <= 0)
                return HookResult.Continue;

            if (attacker == null || !attacker.IsValid)
                return HookResult.Continue;

            if (victim == null || !victim.IsValid)
                return HookResult.Continue;

            if (string.IsNullOrWhiteSpace(weapon))
                return HookResult.Continue;

            var damageMultiplier = TryGetDamageMultiplier(attacker, weapon);
            if (damageMultiplier == null)
                return HookResult.Continue;

            var damage = (int)(eventInfo.DmgHealth * damageMultiplier) - eventInfo.DmgHealth;

            victim.Health = Math.Clamp(victim.Health - damage, 0, victim.MaxHealth);

            return HookResult.Continue;
        }

        private float? TryGetDamageMultiplier(CCSPlayerController playerController, string weapon)
        {
            var permission = TryGetPermissionConfig(playerController);
            if (permission == null)
                return null;

            if (permission.Weapons.TryGetValue(weapon, out var damageMultiplier))
                return damageMultiplier;
            
            if (permission.Weapons.TryGetValue("*", out damageMultiplier))
                return damageMultiplier;

            return null;
        }

        private PermissionData TryGetPermissionConfig(CCSPlayerController playerController)
        {
            Config.Permissions.TryGetValue("*", out var any);

            return Config.Permissions
                .Where(t => AdminManager.PlayerHasPermissions(playerController, t.Key))
                .Select(t => t.Value)
                .Append(any)
                .FirstOrDefault();
        }
    }
}
