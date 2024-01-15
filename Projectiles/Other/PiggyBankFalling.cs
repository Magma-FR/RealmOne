using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Other
{
    public class PiggyBankFalling : ModProjectile
    {
        private bool exploded = false;
        private int soundLoop = 0;
        private int angle = 60;

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 24;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 2400;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (soundLoop > 0)
            {
                soundLoop--;
            }

            if (soundLoop == 0)
            {
                SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
                soundLoop = 35;
            }
            Vector2 vec = Projectile.Center + Vector2.Normalize(Projectile.velocity) * 10f;
            Dust dusted = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch)];
            dusted.position = vec;
            dusted.velocity = Projectile.velocity.RotatedBy(1.5707963705062866) * 0.33f + Projectile.velocity / 4f;
            dusted.position += Projectile.velocity.RotatedBy(1.5707963705062866);
            dusted.fadeIn = 0.5f;
            dusted.noGravity = true;
            Dust ddusted = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch)];
            ddusted.position = vec;
            ddusted.velocity = Projectile.velocity.RotatedBy(-1.5707963705062866) * 0.33f + Projectile.velocity / 4f;
            ddusted.position += Projectile.velocity.RotatedBy(-1.5707963705062866);
            ddusted.fadeIn = 0.5f;
            ddusted.noGravity = true;
            for (int num210 = 0; num210 < 1; num210++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch);
                Main.dust[dust].velocity *= 0.5f;
                Main.dust[dust].scale *= 1.3f;
                Main.dust[dust].fadeIn = 1f;
                Main.dust[dust].noGravity = true;
            }
            Projectile.velocity.X = 0;
            Projectile.spriteDirection = Projectile.direction;
            Vector2 loc = new Vector2(Projectile.position.X, Projectile.position.Y - 1);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, speed * 12, Scale: 1.8f);
                dust1.noGravity = true;
            }
            if (exploded == false)
            {
                Vector2 rotation = Projectile.velocity.RotatedBy(1);
                Vector2 rotation2 = Projectile.velocity.RotatedBy(-1);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y + 8), new Vector2(0, 0), ModContent.ProjectileType<FireFriendly>(), Projectile.damage / 5, 8f, Main.myPlayer);
                for (int i = 0; i < 6; i++)
                {
                    Vector2 velocity = Projectile.velocity / 3;
                    Vector2 speed = velocity.RotatedBy(angle);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, speed, ModContent.ProjectileType<PorcelainFriendly>(), Projectile.damage / 5, 8f, Main.myPlayer);
                    angle += 15;
                }
                const int ExplosionSize = 270;

                Projectile.position -= new Vector2(ExplosionSize / 2f) - Projectile.Size / 2f;
                Projectile.width = Projectile.height = ExplosionSize;
                Projectile.friendly = true;
                Projectile.hide = true;
                Projectile.penetrate = -1;
                Projectile.timeLeft = 4;
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
                exploded = true;
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    if (Projectile.Colliding(Projectile.getRect(), Main.player[i].getRect()))
                    {
                        Main.player[i].Heal(Main.player[i].statLifeMax2 / 6);
                    }
                }
            }
        }
    }
}