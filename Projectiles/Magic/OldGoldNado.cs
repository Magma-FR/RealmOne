using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{

    public class OldGoldNado : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Gold Nado");
            Main.projFrames[Projectile.type] = 7;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {

            Projectile.width = 60;
            Projectile.height = 60;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 100;
            Projectile.light = 1;
            Projectile.penetrate = 2;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;

        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity *= 1f;
            Vector2 center = Projectile.Center;

            if (++Projectile.frameCounter >= 5f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sandnado, 0f, 0f, 0, default, 1.5f);
            SoundEngine.PlaySound(SoundID.DD2_PhantomPhoenixShot);

        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];

            for (int k = 0; k < 2; k++)
            {
                var position = new Vector2(Projectile.Center.X + Main.rand.Next(-40, 20), Projectile.Center.Y + Main.rand.Next(-30, 20));
                //Spawns randomly near the Sandnado
                //Copied from Glitterbook Item Shoot

                //Position the Projectile ##Problem: Projectile doensnt appear on the mouse position
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - position) * 6f;

                //Makes a spawn place
                Vector2 spawnPlace = Vector2.Normalize(position) * 20f;

                if (Collision.CanHit(position, 0, 0, position + spawnPlace, 0, 0))
                {
                    position += spawnPlace;
                }
                //Dusts
                for (int i = 0; i < 130; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(Projectile.Center, DustID.Sandnado, speed * 8, Scale: 1f);
                    ;
                    d.noGravity = true;
                }
                //Spawns 3 Desert Hands
                for (int f = 0; f < 3; f++)
                {

                    int pr = Projectile.NewProjectile(Projectile.GetSource_Death(), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<DesertHands>(), Projectile.damage, Projectile.knockBack, 0, 0.0f, 0.0f);

                }
            }
        }

        public override void AI()
        {

            Lighting.AddLight(Projectile.position, 0.4f, 0.2f, 0.1f);

            int fadeTime = 8;
            if (Projectile.timeLeft > fadeTime)
            {
                if (Projectile.alpha > 0)
                    Projectile.alpha -= 255 / fadeTime;
            }
            else
            {
                Projectile.alpha += 255 / fadeTime;
            }
        }
    }
}