using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Other
{
    public class InkGlob : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ink Glob");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.timeLeft = 140;
            Projectile.penetrate = 3;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
                target.AddBuff(BuffID.ShadowFlame, 160, true);
        }

        public override bool PreAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.5f;
            Lighting.AddLight(Projectile.position, r: 0.2f, g: 0.3f, b: 0.4f);

            Projectile.velocity.Y += 0.4f;
            Projectile.velocity.X *= 1f;

            Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -8, 8);

            Vector2 center = Projectile.Center;
            for (int j = 0; j < 30; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.Obsidian, 0f, 0f, 100, default, 0.6f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;

                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
            }

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.position);
            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Obsidian, speed * 5, Scale: 2f);
                d.noGravity = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}