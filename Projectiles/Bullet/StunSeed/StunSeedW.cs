using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet.StunSeed
{
    public class StunSeedW : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.damage = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
        }

        private int bounce = 0;
        private int maxBounces = 3;

        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;

            Projectile.velocity.Y += 0.5f;
            Projectile.velocity.X *= 1f;
            Projectile.aiStyle = 0;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bounce++;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X / 2;
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y / 2;
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            }
            Projectile.aiStyle = 1;

            if (bounce >= maxBounces) return true;
            else return false;
        }

        public override void OnKill(int timeLeft)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("SeedGore10").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("SeedGore10").Type, 1f);
            for (int i = 0; i < 7; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.WoodFurniture, speed * 8, Scale: 1f);
                dust1.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}