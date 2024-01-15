using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Hell
{
    public class Phlogiston : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phlogiston"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Get a fresh breeze of FYAARRR"
            + $"\nUses Gel [i:{ItemID.Gel}]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.UseSound = SoundID.Item116;

            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Gel;
            Item.noMelee = true;
            Item.shootSpeed = 14f;
            Item.shoot = ProjectileID.ExplosiveBullet;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));

            if (Main.rand.NextBool(4))
                type = ProjectileID.DD2FlameBurstTowerT3Shot;

            if (Main.rand.NextBool(2))
                type = ProjectileID.Flames;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            for (int i = 0; i < 6; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Lava, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddRecipeGroup("IronBar", 10);

            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.Torch, 30);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.38f;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-2, 0);
            return offset;
        }
    }
}