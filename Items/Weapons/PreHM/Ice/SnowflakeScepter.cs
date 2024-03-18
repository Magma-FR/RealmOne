using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Magic;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Ice
{
    public class SnowflakeScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake Scepter");
            Tooltip.SetDefault("Conjure snowflakes from the sky and from the staff!"
                + "\nThese snowflakes can pierce up to 3 enemies!'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 8;
            Item.damage = 10;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 9f;
            Item.staff[Item.type] = true;

            Item.useAnimation = 36;
            Item.useTime = 36;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 11);

            Item.shoot = ModContent.ProjectileType<Snowy>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int a = 0; a < Main.rand.Next(1, 5); a++)
            {
                float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi - MathHelper.PiOver2);

                Vector2 PositionArea = Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 20f;
                if (Collision.CanHit(position, 0, 0, position + PositionArea, 0, 0))
                    position += PositionArea;
            }

            Lighting.AddLight(player.position, r: 0.3f, g: 0.25f, b: 0.1f);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 4f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            float numberProjectiles = 2 + Main.rand.Next(1); // 3, 4, or 5 shotsx`
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(velocity) * 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<Snowy>(), damage, knockback, player.whoAmI);
            }

            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
                ceilingLimit = player.Center.Y - 200f;
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                    heading.Y *= -1f;

                if (heading.Y < 20f)
                    heading.Y = 20f;

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(1, 3) * 0.02f;
                Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<Snowy>(), damage * 1, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.MaxMana);
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(2, 0);
            return offset;
        }
    }
}