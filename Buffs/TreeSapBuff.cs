using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class TreeSapBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Tree Sap Buff");
            Description.SetDefault("Calming effect and 5% increased movement speed while in the forest");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ZoneForest)
            {
                player.moveSpeed += 0.10f;
                player.maxRunSpeed += 0.05f;
                player.calmed = true;
            }
            else
            {
                player.moveSpeed += 0.05f;
                player.maxRunSpeed += 0.05f;
                player.calmed = true;
            }
        }
    }
}