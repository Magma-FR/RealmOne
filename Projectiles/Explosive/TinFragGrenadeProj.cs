using Microsoft.Xna.Framework;
using RealmOne.Common.DamageClasses;
using RealmOne.Common.Systems;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Explosive
{
    public class TinFragGrenadeProj : ModProjectile
    {
        public bool Exploded { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = !value ? 0 : 1; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tin Frag Grenade");

            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.hostile = false;
            Projectile.timeLeft = 260;
            Projectile.penetrate = 2;
            Projectile.DamageType = ModContent.GetInstance<DemolitionClass>();

            Projectile.ownerHitCheck = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.04f * Projectile.velocity.X;
            Projectile.velocity.Y += 0.1f;

            if (++Projectile.frameCounter >= 25f)//the amount of ticks the game spends on each frame
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            if (Projectile.timeLeft < 4 && !Exploded)
            {
                const int ExplosionSize = 250;

                Projectile.position -= new Vector2(ExplosionSize / 2f) - Projectile.Size / 2f;
                Projectile.width = Projectile.height = ExplosionSize;
                Projectile.hostile = true;
                Projectile.friendly = true;
                Projectile.hide = true;
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
                Exploded = true;
            }

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy(Projectile.rotation, default) * 1.1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X;

            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y * 0.22f;

            Projectile.velocity.X *= 0.9f;

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 140);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly;
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            SoundEngine.PlaySound(rorAudio.SFX_GrenadeRoll, Projectile.position);

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FlameBurst, 0f, 0f, 150, default, 1.2f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                Main.dust[dustIndex].velocity *= 3f;
            }

            int RumGore1 = Mod.Find<ModGore>("CopperGore1").Type;
            int RumGore2 = Mod.Find<ModGore>("CopperGore2").Type;
            int RumGore3 = Mod.Find<ModGore>("CopperGore3").Type;

            Terraria.DataStructures.IEntitySource entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 3; i++)
            {
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore1);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore2);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore3);
            }
        }
    }
}