using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class LightbulbBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb");
        }

        //  private Vector2 flashoffset = Vector2.Zero;

        // private Player Owner => Main.player[Projectile.owner];

        //  private bool FullyUsed = false;


        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 200;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.aiStyle = 0;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.Left + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.8f);

            Player player = Main.player[Projectile.owner];
            Projectile.rotation = Projectile.velocity.ToRotation();

            Lighting.AddLight(Projectile.Center, r: 2f, g: 2.2f, 1f);

        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(rorAudio.BulbShatter, Projectile.position);

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore1").Type, 1.5f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore2").Type, 1.5f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore3").Type, 1.5f);
        }
    }
}