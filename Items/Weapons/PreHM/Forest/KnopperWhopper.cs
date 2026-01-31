/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class KnopperWhopper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Knopper Whopper"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Swing a reproductive and bio-reactive Knopper Gall that poisons enemies"
                + "\nDue to Knopper Galls being chemically distorted, the poison depends on how much health the enemy has"
                + "\n'Plenty of unwanted insects thrive off this acorn'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.ResearchUnlockCount = 1;

            // This line will make the damage shown in the tooltip twice the actual Item.damage. This multiplier is used to adjust for the dynamic damage capabilities of the projectile.
            // When thrown directly at enemies, the flail projectile will deal double Item.damage, matching the tooltip, but deals normal damage in other modes.
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2.5f;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shootSpeed = 30f;
            Item.shoot = ModContent.ProjectileType<KnopperBall>();
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(gold: 3, silver: 3);
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddIngredient(ItemID.Acorn, 10);

            recipe.AddIngredient(ItemID.Vine, 2);
            recipe.AddIngredient(ItemID.JungleSpores, 6);

            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}*/