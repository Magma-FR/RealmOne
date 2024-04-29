using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Desert
{
    public class CactusCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 55;
            Item.useAnimation = 55;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CactusCannonProj>();
            Item.shootSpeed = 12f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(gold: 3, silver: 75);
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
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.OasisCactus, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
                d.noLight = false;
            }

            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Cactus, 15)
            .AddIngredient(ItemID.IllegalGunParts, 1)
            .AddRecipeGroup("IronBar", 6)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(3, 12);
            return offset;
        }

        public class CactusCannonProj : ModProjectile
        {
            public override void SetStaticDefaults()
            {
                ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
                ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            }

            public override void SetDefaults()
            {
                Projectile.width = 32;
                Projectile.height = 32;
                Projectile.timeLeft = 340;
                Projectile.penetrate = 5;
                Projectile.scale = 1f;
                Projectile.tileCollide = true;
                Projectile.CloneDefaults(ProjectileID.Shuriken);
            }

            public override void AI()
            {
                Projectile.rotation += 0.4f * Projectile.direction;

                if (Projectile.timeLeft == 0)
                {
                    for (int i = 0; i < 80; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.t_Cactus, speed * 4, Scale: 1.3f);
                        ;
                        d.noGravity = true;
                        d.noLight = false;
                    }
                }
                Projectile.velocity.Y += 0.20f;
                Projectile.velocity.X *= 1f;
                Projectile.aiStyle = 0;
            }

            public override bool OnTileCollide(Vector2 oldVelocity)
            {
                Projectile.penetrate--; //Make sure it doesnt penetrate anymore
                if (Projectile.penetrate <= 0)
                    Projectile.Kill();
                else
                {
                    Projectile.velocity *= 0.3f;

                    if (Projectile.velocity.Y != oldVelocity.Y)
                    {
                        Projectile.velocity.Y = -oldVelocity.Y;
                    }
                    if (Projectile.velocity.X != oldVelocity.X)
                    {
                        Projectile.velocity.X = -oldVelocity.X;
                    }
                }
                return false;
            }

            public override Color? GetAlpha(Color lightColor)
            {
                return Color.White;
            }

            public override void OnKill(int timeLeft)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

                for (int i = 0; i < 80; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(Projectile.Center, DustID.OasisCactus, speed * 4, Scale: 1.3f);
                    ;
                    d.noGravity = true;
                    d.noLight = false;
                }
            }

            public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
            {
                Projectile.penetrate--; //Make sure it doesnt penetrate anymore
                if (Projectile.penetrate <= 0)
                    Projectile.Kill();
                else
                {
                    Projectile.velocity *= 0.7f;
                }
            }
        }
    }
}