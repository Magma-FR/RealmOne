using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    public class TatteredHammer : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TinHammer);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 8)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}