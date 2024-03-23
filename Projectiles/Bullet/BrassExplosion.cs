using Microsoft.Xna.Framework;
using RealmOne.Buffs.Debuffs;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class BrassExplosion : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Explosion");
            Main.projFrames[Projectile.type] = 14;
        }


        public override void SetDefaults()
        {
            Projectile.width = 204;
            Projectile.height = 204;

            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 70;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }


        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 0.5f, 0f);

            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            if (Projectile.frame == 6 && Projectile.frameCounter == 7)
            {
                SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
                Projectile.friendly = true;
                for (int i = 0; i < 90; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(2.5f, 2.5f);
                    var dus = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, speed * 5f, Scale: 2.5f);
                    ;
                    dus.noGravity = true;
                }
            }

            if (Projectile.frame == 6 && Projectile.frameCounter <= 6)
            {
                Projectile.friendly = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            int rand = Main.rand.Next(1, 3);
            if (target.boss == false && target.type != NPCID.TargetDummy)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (rand == 1)
                    {
                        target.velocity.X = 5f;
                    }
                    if (rand == 2)
                    {
                        target.velocity.X = -5f;
                    }
                    target.position.Y -= 8;
                }
            }
            target.AddBuff(ModContent.BuffType<Copperized>(), 240);
        }
    }
}