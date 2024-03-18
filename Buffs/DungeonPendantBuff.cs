using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class DungeonPendantBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Dungeon Pendant Buff");
            Description.SetDefault("'The charm empowers when you step foot into the dungeon!'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ZoneDungeon)
            {
                player.statDefense += 10;
                player.GetDamage(DamageClass.Generic) += 0.10f;
                player.nearbyActiveNPCs = player.statDefense + 1;
            }
        }
    }
}