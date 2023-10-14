using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria;
namespace RealmOne.Common.Core
{
    [Label("Music Config")]
    class ConfigMusic : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("$Mods.RealmOne.BloodMoonAlt")]
        [Tooltip("Adds a unique track for Blood Moons")]
        [DefaultValue(true)]
        public bool BloodMoonMusic { get; set; }
    }
}