using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using System;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Other
{
    public class GoretoothTooth : ModProjectile
    {
        bool sentOut = false;
        bool oneDust = false;
        int hits = 0;
        int maxHit = Main.rand.Next(7, 11);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goretooth");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }


        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 36;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.alpha = 0;
        }

        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var dus = Dust.NewDustPerfect(Projectile.Center, DustID.RedTorch, speed * 4f, Scale: 1f);
                ;
                dus.noGravity = true;
            }
        }

        int currentDegree = 0;

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];

            if (Main.player[Projectile.owner].GetModPlayer<RealmModPlayer>().GoreToothBonus == true && Main.player[Projectile.owner].statLife <= Main.player[Projectile.owner].statLifeMax2 / 2 && sentOut == false)
            {
                Projectile.timeLeft = 300;
            }
            if (Main.player[Projectile.owner].GetModPlayer<RealmModPlayer>().GoreToothBonus == false || Main.player[Projectile.owner].GetModPlayer<RealmModPlayer>().GoreToothBonus == true && Main.player[Projectile.owner].statLife > Main.player[Projectile.owner].statLifeMax2 / 2)
            {
                sentOut = true;
            }

            if (sentOut == true)
            {
                if (oneDust == false)
                {
                    oneDust = true;
                    for (int i = 0; i < 60; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                        var dus = Dust.NewDustPerfect(Projectile.Center, DustID.RedTorch, speed * 4f, Scale: 1f);
                        ;
                        dus.noGravity = true;
                    }

                }
                Projectile.aiStyle = 1;
                Projectile.velocity = (p.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * -8f;
            }
            if (sentOut == false)
            {
                Projectile.Center = p.Center + new Vector2(40).RotatedBy(MathHelper.ToRadians(currentDegree));
                if (currentDegree >= 360)
                {
                    currentDegree = 0;
                }
                else
                {
                    currentDegree += 3;

                }
                Projectile.rotation = Projectile.AngleTo(p.Center) + MathHelper.PiOver2 - MathHelper.ToRadians(180f);
            }


            Lighting.AddLight(Projectile.Center, 0.1f, 0f, 0f);


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hits < maxHit)
            {
                hits++;
            }
            else if (hits >= maxHit)
            {
                hits = 0;
                sentOut = true;
            }
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                if (sentOut == true)
                {
                    var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                    var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                    Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                    float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.4f);
                    Color color = new Color(255, 70, 129) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
                }
                
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}