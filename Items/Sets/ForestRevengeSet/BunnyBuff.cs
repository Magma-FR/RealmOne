using RealmOne.RealmPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.ForestRevengeSet
{
    public class BunnyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<RealmModPlayer>().Bunny = true;
        }
    }
}