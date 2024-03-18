using Microsoft.Xna.Framework;
using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class JungleTechShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Tech Shotgun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Charge up a bio-infused shotgun to shot out a spread of bullets'"
            + "\n'Snazzy!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<JungleTechHeld>();
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item149;
            Item.channel = true;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JungleSpores, 12);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemID.RichMahogany, 10);
            recipe.AddIngredient(Mod, "GizmoScrap", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(5, 0);
            return offset;
        }
    }
}