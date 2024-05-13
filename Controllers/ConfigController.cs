namespace DamageMultiplierCS2.Controllers
{
    internal class ConfigController : BaseController
    {
        public ConfigController(Plugin plugin) : base(plugin)
        {
            //Plugin.RegisterFakeConVars(this);
            Plugin.OnConfigInit += OnConfigUpdate;
        }

        private void OnConfigUpdate()
        {

        }
    }
}
