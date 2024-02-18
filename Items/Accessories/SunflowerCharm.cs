using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]

    public class SunflowerCharm : ModItem
    {
        public override void SetStaticDefaults()
        {

            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public int lifeRegen = 3;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += lifeRegen;
            Lighting.AddLight(player.Center, Color.Yellow.ToVector3() / 1.8f);

        }
    }
}