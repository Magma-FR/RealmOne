using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    public class TatteredSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SilverBroadsword);
            Item.autoReuse = true;
            Item.scale = 1.3f;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 12)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}