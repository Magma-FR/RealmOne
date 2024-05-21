using RealmOne.Items.Placeables.FarmStuff;
using RealmOne.Projectiles.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    public class TatteredJavlin : ModItem
    {
        public override void SetDefaults()
        {
            Item.shoot = ModContent.ProjectileType<TatteredJavlinProj>();
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 14;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.shootSpeed = 9f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
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