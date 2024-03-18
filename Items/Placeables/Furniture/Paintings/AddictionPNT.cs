using RealmOne.Tiles.Furniture.Paintings;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables.Furniture.Paintings
{
    public class AddictionPNT : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("'Addiction'");
            Tooltip.SetDefault("'J.Salaki'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 70, copper: 50);
            Item.rare = ItemRarityID.Green;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<AddictionPNTtile>();
        }
    }
}