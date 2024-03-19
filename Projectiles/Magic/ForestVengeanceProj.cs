using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using System.Net;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class ForestVengeanceProj : ModProjectile
    {
        bool pulse;
        int cd = 50;

        int maxPulse = 225;
        int minPulse = 25;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forest Spirit");
        }


        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;


            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.alpha = 20;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0f, 1f, 0f);

            var d = Dust.NewDustPerfect(Projectile.Center, DustID.ChlorophyteWeapon, new Vector2(0, 0), Scale: 1.2f);
            d.noGravity = true;

            if (cd > 0)
            {
                cd--;
            }

            if (cd == 0)
            {
                cd = 50;
                for (int i = 0; i < 60; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.8f);
                    var dus = Dust.NewDustPerfect(Projectile.Center, DustID.ChlorophyteWeapon, speed * 5f, Scale: 1.2f);
                    ;
                    dus.noGravity = true;
                }
            }

            if (Projectile.alpha >= minPulse && Projectile.alpha < maxPulse && pulse == false)
            {
                for (int i = 0; i < 20; i++)
                {
                    Projectile.alpha++;
                }

            }
            if (Projectile.alpha == maxPulse && pulse == false)
            {
                pulse = true;
            }
            if (Projectile.alpha > minPulse && pulse == true)
            {
                for (int i = 0; i < 20; i++)
                {
                    Projectile.alpha--;
                }

            }
            if (Projectile.alpha == minPulse)
            {
                pulse = false;
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.collideY == true && target.boss == false) // ground
            {
                CombatText.NewText(target.getRect(), Color.DarkRed, "!");
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(target.Center.X, target.Center.Y + 140), new Vector2(0, 0), ModContent.ProjectileType<TreeBush>(), 1, 0f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(target.Center.X + 50, target.Center.Y + 140), new Vector2(0, 0), ModContent.ProjectileType<TreeBush2>(), 1, 0f, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(target.Center.X - 50, target.Center.Y + 140), new Vector2(0, 0), ModContent.ProjectileType<TreeBush3>(), 1, 0f, Main.myPlayer);
            }
            else //aerial
            {
                int random = Main.rand.Next(3, 4);
                for (int i = 0; i < random; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, speed * Main.rand.Next(1, 2), ModContent.ProjectileType<ForestVengeanceProjec>(), Projectile.damage / 2, 2f, Main.myPlayer);
                }
            }
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            for (int i = 0; i < 90; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.GreenTorch, speed * Main.rand.Next(1, 8), Scale: 2.2f);
                ;
                d.noGravity = true;
            }
        }
    }
}