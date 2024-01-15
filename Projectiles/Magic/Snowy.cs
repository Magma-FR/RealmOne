using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class Snowy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.11f;

            Vector2 center = Projectile.Center;
            for (int j = 0; j < 100; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.IceTorch, 0f, 0f, 100, default, 0.6f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;

                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
            }

            Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.4f);
            Lighting.Brightness(1, 1);
        }

        public override void OnKill(int timeleft)
        {
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.IceTorch, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            SoundEngine.PlaySound(SoundID.Item50);
        }
    }
}