using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Debuffs;
using RealmOne.Common.Core;
using RealmOne.Projectiles.Bullet.StunSeed;
using RealmOne.Projectiles.Throwing;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Ocean
{
    public class JellyfishInfestedShark : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 16;
            Item.knockBack = 1f;
            Item.crit = 1;
            Item.rare = ItemRarityID.Green;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<BlueJ>();
        }
        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item11 with { Volume = 0.7f, PitchVariance = 0.5f }, player.Center);
            SoundEngine.PlaySound(SoundID.NPCHit13 with { Volume = 0.5f, PitchVariance = 0.5f }, player.Center);

            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Main.rand.Next(100) < 25)
            {
                type = Main.rand.Next(new int[] { ModContent.ProjectileType<BlueJ>(), ModContent.ProjectileType<PinkJ>(), ModContent.ProjectileType<GreenJ>() });
                damage *= 2;
                knockback *= 2;
                velocity /= 2;
            }
            else
            {
                type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<BlueJ>(), ModContent.ProjectileType<PinkJ>(), ModContent.ProjectileType<GreenJ>() });

            }

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

    }
    public class BlueJ : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.damage = 26;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 2;
            Projectile.CloneDefaults(ProjectileID.Shuriken);
        }



        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;

            Lighting.AddLight(Projectile.position, r: 0.2f, g: 0.8f, b: 1.5f);

            Lighting.Brightness(1, 1);

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water_GlowingMushroom, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            target.AddBuff(ModContent.BuffType<AltElectrified>(), 600);

            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {

            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<JellySpark>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item95, Projectile.position);

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 100, default, 1f);

            Main.dust[dustIndex].noGravity = false;
            Main.dust[dustIndex].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex].noLight = false;

            int dustIndex1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_GlowingMushroom, 0f, 0f, 255, default, 3f);

            Main.dust[dustIndex1].noGravity = true;
            Main.dust[dustIndex1].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex1].noLight = false;
        }
    }
    public class PinkJ : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 340;
            Projectile.penetrate = 5;
            Projectile.scale = 1f;
            Projectile.tileCollide = true;
            Projectile.CloneDefaults(ProjectileID.Shuriken);

        }
        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water_Hallowed, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1f);

            if (Projectile.timeLeft == 0)
            {
                for (int i = 0; i < 80; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.PinkSlime, speed * 4, Scale: 1.3f);
                    ;
                    d.noGravity = true;
                    d.noLight = false;
                }
            }
            Projectile.velocity.Y += 0.2f;
            Projectile.velocity.X *= 1f;
            Projectile.aiStyle = 0;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--; //Make sure it doesnt penetrate anymore
            if (Projectile.penetrate <= 0)
                Projectile.Kill();
            else
            {
                Projectile.velocity *= 0.7f;

                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }

            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit25, Projectile.position);


            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.PinkSlime, speed * 4, Scale: 1.3f);
                ;
                d.noGravity = true;
                d.noLight = false;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.penetrate--; //Make sure it doesnt penetrate anymore
            if (Projectile.penetrate <= 0)
                Projectile.Kill();
            else
            {
                Projectile.velocity *= 0.7f;

            }
        }
    }

    public class GreenJ : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 1;
            Projectile.light = 1;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water_Jungle, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1f);
            Projectile.rotation += 0.09f;
            Projectile.velocity.X *= 0.96f;
            Projectile.velocity.Y *= 0.95f;


        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 500);

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit25, Projectile.position);

            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<GreenBoom>(), Projectile.damage, Projectile.knockBack, Projectile.owner);

            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, Scale: 0.6f, Alpha: 120);
            }
        }
    }
    public class GreenBoom : ModProjectile
    {
        public override string Texture => Helper.Empty;
        public override void SetDefaults()
        {
            Projectile.height = 400;
            Projectile.width = 400;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -2;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 200;
        }
        public override void AI()
        {
            Projectile.ai[0] += 0.05f;
            if (Projectile.ai[0] > 1)
            {
                Projectile.Kill();
            }
        }
        public override void PostAI()
        {
            if (Projectile.ai[1] == 1)
                Projectile.damage = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = Request<Texture2D>("RealmOne/Assets/Effects/g").Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); float alpha = MathHelper.Lerp(8, 0, Projectile.ai[0]);
            for (int i = 0; i < 3; i++)
                Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.ForestGreen * (3 - alpha), Projectile.rotation, tex.Size() / 2, Projectile.ai[0], SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hitinfo, int damage)
        {
            Projectile.ai[1] = 1;
            target.AddBuff(BuffID.Poisoned, 500);
        }
        public override bool ShouldUpdatePosition() 
        {
            return false;
        }
    }
}
