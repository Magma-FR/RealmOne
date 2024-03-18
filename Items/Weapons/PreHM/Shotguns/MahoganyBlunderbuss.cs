/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Bullet;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class MahoganyBlunderbuss : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mahogany Blunderbuss"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'You're most likely going to be poisoned when using this'"
            + "\nShoots a poison petal");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60;
            Item.useAnimation = 60;
            //BOTH 60
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 55f;

            Item.shoot = ModContent.ProjectileType<FlowerBulletProjectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(1); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 28f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<FlowerBulletProjectile>(), damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RichMahogany, 25);
            recipe.AddIngredient(ItemID.JungleSpores, 15);
            recipe.AddIngredient(Mod, "LeadGunBarrel", 1);
            recipe.AddIngredient(Mod, "BrassIngot", 4);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.RichMahogany, 25);
            recipe1.AddIngredient(ItemID.JungleSpores, 15);
            recipe1.AddIngredient(Mod, "IronGunBarrel", 1);
            recipe1.AddIngredient(Mod, "BrassIngot", 4);

            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}*/