using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class LilGold : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Gold Bullet");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, 0.3f, 0.3f, 0.2f);
            Lighting.Brightness(1, 1);
        }

        public PrimitiveTrail trail = new();
        public List<Vector2> oldPositions = new List<Vector2>();

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            lightColor = Color.White;

            Color color = Color.Gold;

            Vector2 pos = (Projectile.Center).RotatedBy(Projectile.rotation, Projectile.Center);

            oldPositions.Add(pos);
            while (oldPositions.Count > 30)
                oldPositions.RemoveAt(0);

            trail.Draw(color, pos, oldPositions, 1.4f);
            trail.width = 2;

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.5f);
                Color ProjColor = new Color(244, 204, 39) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;

                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }

        public override void OnKill(int timeleft)
        {
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Sandnado, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }
        }
    }
}