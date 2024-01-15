using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Magic
{
    public class CorneaScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cornea Scepter");
            Tooltip.SetDefault("Casts reckless demon eyes out of the staff");
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.width = 32;
            Item.height = 38;
            Item.maxStack = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Blue;
            Item.mana = 5;
            Item.noMelee = true;
            Item.staff[Item.type] = true;
            Item.shoot = ModContent.ProjectileType<eyesmol>();
            Item.UseSound = SoundID.Item8;
            Item.shootSpeed = 20f;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            position += Vector2.Normalize(velocity) * 35f;

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.t_Flesh, speed * 5, Scale: 1.8f);
                ;
                d.noGravity = true;
            }

            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(13));
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            if (Main.rand.NextBool(8))
                type = ModContent.ProjectileType<eyebig>();
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (Item.shoot == ModContent.ProjectileType<eyebig>())

            {
                damage *= 2f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<FleshyCornea>(), 12)
            .AddIngredient(ItemID.CrimtaneBar, 10)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<FleshyCornea>(), 12)
            .AddIngredient(ItemID.DemoniteBar, 10)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6f, -2f);
        }
    }
}