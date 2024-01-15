using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class TraditionalCologneBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Traditional Cologne");
            Description.SetDefault("'You smell like exotic moonglow and cold skybreeze!'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 3;
            player.moveSpeed += 0.15f;
            player.accRunSpeed += 0.15f;
            player.calmed = true;
            player.discountAvailable = true;
        }
    }
}