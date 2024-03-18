/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Bullet;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class EbonfrostBlunderbuss : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebonfrost Blunderbuss"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Chilled and Corrupted'"
            + "\nShoots a icy corruption bullet");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60;
            Item.useAnimation = 60;
            //60 FOR BOTH
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 57f;

            Item.shoot = ModContent.ProjectileType<IceBulletProjectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(1); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 28f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<IceBulletProjectile>(), damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 15);
            recipe.AddIngredient(Mod, "BrassIngot", 4);
            recipe.AddIngredient(Mod, "LeadGunBarrel", 1);
            recipe.AddIngredient(ItemID.Ebonwood, 10);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.DemoniteBar, 15);
            recipe2.AddIngredient(Mod, "BrassIngot", 4);
            recipe2.AddIngredient(Mod, "IronGunBarrel", 1);
            recipe2.AddIngredient(ItemID.Ebonwood, 10);

            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-12, 0);
            return offset;
        }
    }
}*/ 