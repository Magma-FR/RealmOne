using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class PiggyDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Explosive Piggy Bank Cooldown");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}