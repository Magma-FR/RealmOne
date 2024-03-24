using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class BrassMissile : ModProjectile
    {
        int cd;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Missile");
        }

        public override void OnSpawn(IEntitySource source)
        {
            cd = 240;
        }


        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;

            Projectile.damage = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1200;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Lighting.AddLight(Projectile.Center, 1f, 0.5f, 0f);
            var dd = Dust.NewDustPerfect(Projectile.Center, DustID.Smoke, Scale: 1f);
            dd.noGravity = true;
            //Main.dust[dd].velocity *= 0.3f;
            var ddd = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Scale: 1f);
            ddd.noGravity = true;
            //Main.dust[ddd].velocity *= 0.3f;

            if (cd > 0)
            {
                Projectile.velocity = (Projectile.Center - Main.MouseWorld).SafeNormalize(Vector2.Zero) * -2f;
                cd--;
            }

            if (cd == 0)
            {
                Projectile.aiStyle = 1;
            }


            if (Vector2.Distance(Main.MouseWorld, Projectile.Center) < 10 && cd > 0)
            {
                cd = 0;
            }


        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<BrassExplosion>(), Projectile.damage, 0f, Main.myPlayer);
        }
    }
}