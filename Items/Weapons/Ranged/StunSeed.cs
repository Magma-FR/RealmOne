using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Bullet.StunSeed;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Ranged
{
    public class StunSeed : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.knockBack = 2.7f;
            Item.crit = 8;
            Item.rare = ItemRarityID.LightRed;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.shootSpeed = 13;
            Item.shoot = ModContent.ProjectileType<StunSeedA>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Main.rand.Next(100) < 25)
            {
                type = Main.rand.Next(new int[] { ModContent.ProjectileType<StunSeedBA>(), ModContent.ProjectileType<StunSeedBP>(), ModContent.ProjectileType<StunSeedBW>() });
                damage *= 2;
                knockback *= 2;
                velocity /= 2;
            }
            else
            {
                type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<StunSeedA>(), ModContent.ProjectileType<StunSeedP>(), ModContent.ProjectileType<StunSeedW>() });
            }

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }
    }
}