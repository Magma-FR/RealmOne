using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class FlamerProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Spray");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.hostile = false;

            Projectile.width = 40;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.alpha = 0;
            Projectile.penetrate = -2;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = false;
            Projectile.damage = 24;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 10;
            Projectile.scale = 2f;
            Projectile.light = 2f;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 14f)//the amount of ticks the game spends on each frame
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.2f);
            Lighting.Brightness(1, 1);
            // Set the dust type to ExampleSolution
            //    int dustType = ModContent.DustType<Dusts.FlamerDust>();
            //  Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<Heller>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Lava, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, Scale: 0.2f);

            //      var dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<FlamerDust>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 101);

            //        dust.noGravity = true;
            //        dust.scale *= 1.75f;
            //        dust.velocity.X *= 2f;
            //         dust.velocity.Y *= 2f;
            //      dust.scale *= 0.5f;
            int newDust = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.FlameBurst, Main.rand.Next(-3, 4), Main.rand.Next(-3, 4), 100, default, 3f);
            Dust dust = Main.dust[newDust];
            dust.position.X -= 2f;
            dust.position.Y += 2f;
            dust.scale += Main.rand.Next(50) * 0.01f;
            dust.noGravity = true;

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 3f);
            Main.dust[dustIndex].scale = 0.2f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 160);
        }
    }
}