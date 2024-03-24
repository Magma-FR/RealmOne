using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class BrassMightCD : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Brass Encasement Cooldown");
            Description.SetDefault("'There is no more rust..'");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}