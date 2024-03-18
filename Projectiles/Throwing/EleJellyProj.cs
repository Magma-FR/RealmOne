using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Debuffs;
using RealmOne.Common.Core;
using RealmOne.Items.Weapons.PreHM.Throwing;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Throwing
{
    public class EleJellyProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ele Jelly");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.aiStyle = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.light = 1f;

            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 2;
            Projectile.CloneDefaults(ProjectileID.Shuriken);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, r: 0.2f, g: 0.8f, b: 1.5f);

            Lighting.Brightness(1, 1);

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water_GlowingMushroom, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public PrimitiveTrail trail = new();
        public List<Vector2> oldPositions = new List<Vector2>();

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            lightColor = Color.White;

            Color color = Color.Cyan;

            Vector2 pos = (Projectile.Center).RotatedBy(Projectile.rotation, Projectile.Center);

            oldPositions.Add(pos);
            while (oldPositions.Count > 30)
                oldPositions.RemoveAt(0);

            trail.Draw(color, pos, oldPositions, 1.4f);
            trail.width = 2;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }

        public override void OnKill(int timeleft)

        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<JellySpark>(), Projectile.damage, Projectile.knockBack, Projectile.owner);

            for (int i = 0; i < 17; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple, 0f, 0f, 50, default, 2f);

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item95, Projectile.position);

            if (Main.rand.Next(0, 5) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                    ModContent.ItemType<EleJelly>(), 1, false, 0, false, false);

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 1f);

            Main.dust[dustIndex].noGravity = false;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex].noLight = false;

            int dustIndex1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_GlowingMushroom, 0f, 0f, 255, default, 3f);

            Main.dust[dustIndex1].noGravity = true;
            Main.dust[dustIndex1].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex1].noLight = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            target.AddBuff(ModContent.BuffType<AltElectrified>(), 600);

            Projectile.Kill();
        }
    }
}