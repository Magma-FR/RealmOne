using RealmOne.Items.Placeables.FarmStuff;
using RealmOne.Tiles.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables.Furniture
{
    public class FarmChest : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FarmChestTile>());
            Item.placeStyle = 1; // Use this to place the chest in its locked style
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.buyPrice(0, 0, 8, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TatteredWood>());
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}