using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class HazardBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("EXPLOSIVE HAZARD!");
            Description.SetDefault("'You are vulnerable to damage from explosives!!!'");
        }
    }
}