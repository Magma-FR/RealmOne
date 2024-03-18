using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Bosses
{
    public class GlobGunProjectileHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Explodey Mud Ball");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;

            Projectile.timeLeft = 60;

            DrawOffsetX = 5;
            DrawOriginOffsetY = 5;
            Projectile.ownerHitCheck = false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Die immediately if ai[1] isn't 0 (We set this to 1 for the 5 extra explosives we spawn in Kill)
            if (Projectile.ai[1] != 0)
            {
                return true;
            }

            return false;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BubbleBurst_Pink, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f);//spawns dust behind it, this is a spectral light blue dust

            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = true;
                Projectile.alpha = 255;
                Projectile.position.X = Projectile.position.X + Projectile.width / 2;
                Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
                Projectile.width = 350;
                Projectile.height = 350;
                Projectile.position.X = Projectile.position.X - Projectile.width / 2;
                Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
                Projectile.damage = 20;
                Projectile.knockBack = 6f;
                Projectile.penetrate = 2;
                Projectile.ownerHitCheck = false;
                Projectile.friendly = false;
                Projectile.hostile = true;
            }
            else
            {
                // Smoke and fuse dust spawn.
                if (Main.rand.NextBool(2))
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Worm, 0f, 0f, 100, default, 1f);
                    Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
                    dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Mud, 0f, 0f, 100, default, 1f);
                    Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy(Projectile.rotation, default) * 1.1f;
                }
            }

            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 5f)
            {
                Projectile.ai[0] = 10f;
                if (Projectile.velocity.Y == 0f && Projectile.velocity.X != 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X * 0.9f;
                    {
                        Projectile.velocity.X = Projectile.velocity.X * 0.7f;
                    }

                    if (Projectile.velocity.X > -0.01 && Projectile.velocity.X < 0.1)
                    {
                        Projectile.velocity.X = 0f;
                        Projectile.netUpdate = true;
                    }
                }

                Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.1f;
            return;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.penetrate = 1;

            if (Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    var vel = new Vector2(Main.rand.NextFloat(0, 0), Main.rand.NextFloat(0, 0));
                }
            }

            SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Worm, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Mud, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Mud, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].velocity *= 3f;
            }

            // reset size to normal width and height.
            Projectile.position.X = Projectile.position.X + Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            Projectile.friendly = false;
            Projectile.hostile = true;

            {
                int explosionRadius = 250;

                int minTileX = (int)(Projectile.position.X / 16f - explosionRadius);
                int maxTileX = (int)(Projectile.position.X / 16f + explosionRadius);
                int minTileY = (int)(Projectile.position.Y / 16f - explosionRadius);
                int maxTileY = (int)(Projectile.position.Y / 16f + explosionRadius);
                if (minTileX < 0)
                {
                    minTileX = 0;
                }

                if (maxTileX > Main.maxTilesX)
                {
                    maxTileX = Main.maxTilesX;
                }

                if (minTileY < 0)
                {
                    minTileY = 0;
                }

                if (maxTileY > Main.maxTilesY)
                {
                    maxTileY = Main.maxTilesY;
                }
                for (int x = minTileX; x <= maxTileX; x++)
                {
                    for (int y = minTileY; y <= maxTileY; y++)
                    {
                        float diffX = Math.Abs(x - Projectile.position.X / 16f);
                        float diffY = Math.Abs(y - Projectile.position.Y / 16f);
                        double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                        if (distance < explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].WallType == 0)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}