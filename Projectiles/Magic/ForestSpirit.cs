using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class ForestSpirit : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Spirit");
        }


        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.alpha = 200;
        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var dus = Dust.NewDustPerfect(Projectile.Center, DustID.ChlorophyteWeapon, speed * 2f, Scale: 1f);
                ;
                dus.noGravity = true;
            }
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0f, 1f, 0f);

            var d = Dust.NewDustPerfect(Projectile.Center, DustID.ChlorophyteWeapon, new Vector2(0, 0), Scale: 0.8f);
            d.noGravity = true;

            Rectangle box = new Rectangle((int)Projectile.position.X - 200, (int)Projectile.position.Y - 200, 400, 400);
            Rectangle hitbox = new Rectangle((int)Projectile.position.X - 16, (int)Projectile.position.Y - 16, 32, 32);
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Projectile.Colliding(hitbox, Main.player[i].getRect()))
                {
                    Projectile.Kill();
                }
                if (Projectile.Colliding(box, Main.player[i].getRect()))
                {
                    Projectile.velocity = (Main.player[i].Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 4.5f;
                }
            }

        }
    }
}