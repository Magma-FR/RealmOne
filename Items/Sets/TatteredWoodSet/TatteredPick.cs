using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    public class TatteredPick : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TinPickaxe);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}