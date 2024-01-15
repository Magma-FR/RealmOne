using RealmOne.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables
{
    public class NewtonCradleItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Newton's Cradle");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.value = Item.buyPrice(gold: 1);
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;

            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<NewtonCradleTile>();
        }
    }
}