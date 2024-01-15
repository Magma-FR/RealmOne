using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.Ores
{
    public class BrassNuggets : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Nuggets");
            Tooltip.SetDefault("A durable and inexpensive chunk of common brass");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 2);
        }

        public override void AddRecipes()
        {
            CreateRecipe(2)
            .AddIngredient(ItemID.CopperOre, 2)
            .AddIngredient(ItemID.IronOre, 2)

            .AddTile(TileID.Furnaces)
            .Register();

            CreateRecipe(2)
          .AddIngredient(ItemID.TinOre, 2)
          .AddIngredient(ItemID.LeadOre, 2)

          .AddTile(TileID.Furnaces)
          .Register();

            CreateRecipe(2)
        .AddIngredient(ItemID.CopperOre, 2)
        .AddIngredient(ItemID.LeadOre, 2)

        .AddTile(TileID.Furnaces)
        .Register();

            CreateRecipe(2)
        .AddIngredient(ItemID.TinOre, 2)
        .AddIngredient(ItemID.IronOre, 2)

        .AddTile(TileID.Furnaces)
        .Register();
        }
    }
}