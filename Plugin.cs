using CounterStrikeSharp.API.Core;
using DamageMultiplierCS2.Controllers;
using DamageMultiplierCS2.Extensions;
using DamageMultiplierCS2.Models;
using System;

namespace DamageMultiplierCS2
{
    public class Plugin : BasePlugin, IPluginConfig<PluginConfig>
    {
        #region Plugin info
        public override string ModuleName => "CS2_DamageMultiplier";
        public override string ModuleAuthor => AssemblyInfoEx.GetAuthor();
        public override string ModuleVersion => AssemblyInfoEx.GetVersion();
        #endregion

        public PluginConfig Config { get; set; } = new();
        
        internal ConfigController ConfigController { get; private set; }
        internal DamageController DamageController  { get; private set; }

        internal event Action OnConfigInit;

        public override void Load(bool hotReload)
        {
            if (hotReload)
                return;

            ConfigController = new(this);
            DamageController = new(this);
        }

        public void OnConfigParsed(PluginConfig config)
        {
            this.Config = config;
            OnConfigInit?.Invoke();
        }
    }
}
