using CounterStrikeSharp.API.Core;

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

            if (!attacker.IsValid)
                return HookResult.Continue;

            if (victim == null || !victim.IsValid)
                return HookResult.Continue;

            if (string.IsNullOrWhiteSpace(weapon))
                return HookResult.Continue;

            var damage = eventInfo.DmgHealth * 20;
            if (damage <= 0)
                return HookResult.Continue;

            if (damage > victim.Health)
                damage = victim.Health;

            victim.Health -= damage;

            return HookResult.Continue;
        }
    }
}
