using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class DesertHands : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Hands");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 220;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Vector2 center = Projectile.Center;
            for (int j = 0; j < 5; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.Sandnado, 0f, 0f, 100, default, 1f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;

                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
            }

            Lighting.AddLight(Projectile.position, 0.3f, 0.2f, 0.1f);
            Lighting.Brightness(1, 1);

            if (++Projectile.frameCounter >= 20f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public override void OnKill(int timeleft)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("BoneSpineGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("BoneSpineGore2").Type, 1f);
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Sandnado, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            SoundEngine.PlaySound(SoundID.DD2_SkeletonHurt);
        }
    }
}