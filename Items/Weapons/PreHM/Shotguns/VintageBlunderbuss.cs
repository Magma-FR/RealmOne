using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Bullet;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class VintageBlunderbuss : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vintage Blunderbuss"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'An antique, sturdy and heavy blunderbuss'"
            + "\n'There's a sheet of parchment inside the barrel of the gun'"
            + "\n'It says: Hold the blunderbuss by the bottom and make sure your hand is not near the barrel as when shot it will recoil your entire body'"
            + "\n'The blunderbuss has been made with reckless care but still shoots out great power");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 32;
            Item.useTime = 61;
            Item.useAnimation = 61;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<VintageBulletProjectile>();
            Item.shootSpeed = 98f;
            Item.value = Item.buyPrice(gold: 2, silver: 75);
            Item.crit = 4;
            Item.reuseDelay = 75;
            Item.noMelee = true;

        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 5 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 18f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<VintageBulletProjectile>(), damage, knockback, player.whoAmI);
            }

            for (int d = 0; d < 40; d++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 150, default, 3f);
                Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 150, default, 3f);

            }

           

            SoundEngine.PlaySound(rorAudio.blunderbussShot);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ShadowScale, 15)
            .AddIngredient(ItemID.IllegalGunParts, 1)
            .AddRecipeGroup("IronBar", 12)
            .AddRecipeGroup("Wood", 8)
             .AddIngredient(Mod, "BrassIngot", 6)
            .AddTile(TileID.Anvils)
            .Register();

         

        }
        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-10, -5);
            return offset;
        }

    }
}