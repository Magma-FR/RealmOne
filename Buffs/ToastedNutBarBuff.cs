using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class ToastedNutBarBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Energised");
            Description.SetDefault("5% increased endurance and increase to all stats");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.05f;

            player.AddBuff(BuffID.WellFed, 15000);
        }
    }
}