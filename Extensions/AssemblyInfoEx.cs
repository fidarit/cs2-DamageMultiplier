using System.Reflection;

namespace DamageMultiplierCS2.Extensions
{
    internal static class AssemblyInfoEx
    {
        public static string GetVersion()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
        }

        public static string GetAuthor()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<AssemblyCopyrightAttribute>()
                .Copyright;
        }
    }
}
