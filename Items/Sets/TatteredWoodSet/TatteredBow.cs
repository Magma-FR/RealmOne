using Microsoft.Xna.Framework;
using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    public class TatteredBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LeadBow);
            Item.useTime = 14;
            Item.useAnimation = 14;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-1, 0);
            return offset;
        }
    }
}