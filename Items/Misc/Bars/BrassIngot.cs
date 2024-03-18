using RealmOne.Tiles.Blocks;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.Bars
{
    public class BrassIngot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Ingot"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Crafts antique and durable stationary and weaponary"
                + "\n'Brass is most widely used in applications that are decorative and mechanical.'"
                + "\n'Due to its unique properties, which include corrosion resistance to rusting.'"
                + "\n'Common uses for brass include applications that require low friction.'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.width = 20;
            Item.consumable = true;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 15);
            Item.createTile = ModContent.TileType<BrassBarTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(Mod, "BrassNuggets", 4)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}