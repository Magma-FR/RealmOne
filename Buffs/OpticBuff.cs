using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class OpticBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            Main.buffNoTimeDisplay[Type] = true;

            DisplayName.SetDefault("Midnight Hunter");
            Description.SetDefault("Hunter and Nightowl effects, 5% increased crit");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.nightVision = true;
            player.AddBuff(BuffID.Hunter, 60);
        }
    }
}