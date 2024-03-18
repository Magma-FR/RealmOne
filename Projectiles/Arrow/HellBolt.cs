using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Arrow
{
    public class HellBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HellBolt");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 10;

            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 320;
            AIType = ProjectileID.DD2BallistraProj;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }

        public override void OnKill(int timeleft)
        {
            for (int i = 0; i < 50; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Lava, 1f, 1f, 0, default, 1.5f);
            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(rorAudio.SFX_CrossbowImpact);
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.7f, Projectile.velocity.Y * 0.7f, Scale: 1f);   //spawns dust behind it, this is a spectral light blue dust
            Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.2f);
            Lighting.Brightness(1, 1);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            SoundEngine.PlaySound(rorAudio.SFX_CrossbowHit);
            target.AddBuff(BuffID.OnFire3, 180);
        }
    }
}