using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using System.Net;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class ForestVengeanceProjec : ModProjectile
    {
        private bool pulse;
        private int cd = 100;

        private int maxPulse = 225;
        private int minPulse = 25;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Spirit");
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;

            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            var d = Dust.NewDustPerfect(Projectile.Center, DustID.ChlorophyteWeapon, new Vector2(0, 0), Scale: 1f);
            d.noGravity = true;

            if (cd > 0)
            {
                cd--;
            }

            if (cd == 0)
            {
                NPC closestNPC = FindClosestNPC(1000);
                if (closestNPC == null)
                    return;

                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 3f;
                Projectile.friendly = true;
            }
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override void OnKill(int timeleft)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.GreenTorch, speed * Main.rand.Next(1, 5), Scale: 2f);
                ;
                d.noGravity = true;
            }
        }
    }
}