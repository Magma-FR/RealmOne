using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class IchorGlobSmall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Glob Small");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.light = 0.2f;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
            Projectile.penetrate = 2;
            Projectile.alpha = 0;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.8f);
            Lighting.AddLight(Projectile.position, 0.1f, 0.1f, 0.1f);
            Lighting.Brightness(1, 1);
            Projectile.rotation += 0.3f;
            Projectile.velocity *= 0.97f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 400);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.NavajoWhite;
        }

        public override void OnKill(int timeleft)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore1").Type, 1f);

            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Ichor, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            SoundEngine.PlaySound(SoundID.NPCDeath11);
        }
    }
}