using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Other
{
    public class MiniSand : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Sandnado");
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 100;
            Projectile.light = 1;
            Projectile.penetrate = -2;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 0.8f;
            Vector2 center = Projectile.Center;

            if (++Projectile.frameCounter >= 5f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado, 0f, 0f, 0, default, 1.5f);
            SoundEngine.PlaySound(SoundID.DD2_PhantomPhoenixShot);
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            //Makes a spawn place

            //Dusts
            for (int i = 0; i < 130; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Sandnado, speed * 8, Scale: 1f);
                ;
                d.noGravity = true;
            }
            //Spawns 3 Desert Hands
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, 0.4f, 0.2f, 0.1f);

            int fadeTime = 8;
            if (Projectile.timeLeft > fadeTime)
            {
                if (Projectile.alpha > 0)
                    Projectile.alpha -= 255 / fadeTime;
            }
            else
            {
                Projectile.alpha += 255 / fadeTime;
            }
        }
    }
}