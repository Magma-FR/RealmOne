/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.EnemyDrops
{
    public class HellishMembrane : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellish Membrane");
            Tooltip.SetDefault("The souls of underworld monstrosities");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 28, copper: 10);
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Orange.ToVector3() * 0.65f * Main.essScale);
        }
    }
}*/