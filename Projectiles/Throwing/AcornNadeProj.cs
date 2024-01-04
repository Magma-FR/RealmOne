using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.RealmPlayer;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Throwing
{
    public class AcornNadeProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acorn Nade");

        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.extraUpdates = 1;
            Projectile.hostile = false;
            Projectile.timeLeft = 260;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ownerHitCheck = true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void AI()
        {
            Projectile.rotation += 0.04f * Projectile.velocity.X;
            Projectile.velocity.Y += 0.09f;

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 0.1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.t_LivingWood, 0f, 0f, 100, default, 0.6f);
            Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2 - 6)).RotatedBy(Projectile.rotation, default) * 1.1f;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                Projectile.velocity.X = oldVelocity.X * -0.45f;
            if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                Projectile.velocity.Y = oldVelocity.Y * -0.45f;

            return false;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 4, 0, ProjectileID.SeedlerNut, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, -4, 0, ProjectileID.SeedlerNut, Projectile.damage, Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileID.SeedlerNut, Projectile.damage, Projectile.knockBack, Projectile.owner);

        //    Player player = Main.player[Projectile.owner];
     //       player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(rorAudio.SFX_GrenadeRoll, Projectile.position);

            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.WoodFurniture, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.t_LivingWood, 0f, 0f, 150, default, 1.2f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
                Main.dust[dustIndex].velocity *= 3f;

            }

            int RumGore1 = Mod.Find<ModGore>("CopperGore1").Type;
            int RumGore2 = Mod.Find<ModGore>("CopperGore2").Type;
            int RumGore3 = Mod.Find<ModGore>("CopperGore3").Type;

            Terraria.DataStructures.IEntitySource entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 1; i++)
            {
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore1);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore2);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore3);

            }
        }
    }
}

