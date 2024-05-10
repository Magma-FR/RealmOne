using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Bullet
{
    public class PulsaShot : ModProjectile
    {
        public override string Texture
          => Helper.Empty;

        private const int timeLeftMax = 18;
        private Vector2 origin;

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(12);

            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = timeLeftMax;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == timeLeftMax)
            {
                origin = Projectile.Center;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                return;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float Descaling = (float)
            Projectile.timeLeft / timeLeftMax;
            float ScaleY = 18;
            Color color = Color.DodgerBlue;

            for (int i = 0; i < 3; i++)
            {
                float shotLength = origin.Distance(Projectile.Center);

                Texture2D texture = (i > 1) ?
                Mod.Assets.Request<Texture2D>("Assets/Effects/Trail_1").Value : Mod.Assets.Request<Texture2D>("Assets/Effects/GlowTrail").Value;
                Vector2 scale = new Vector2(shotLength, MathHelper.Lerp(ScaleY, 5, 1f - Descaling)) / texture.Size();

                //descales by colour
                color = (color with { A = 0 }) * Descaling;
                Main.EntitySpriteDraw(texture, origin - Main.screenPosition, null, color, Projectile.velocity.ToRotation(), new Vector2(0, texture.Height / 2), scale, SpriteEffects.None, 0);

                Texture2D Trailing = Mod.Assets.Request<Texture2D>("Assets/Effects/GradientCirc").Value;
                Vector2 Endscale = origin + (Vector2.UnitX * shotLength).RotatedBy(Projectile.velocity.ToRotation());

                Main.EntitySpriteDraw(Trailing, Endscale - Main.screenPosition, null, color, 0, Trailing.Size() / 2, (0.13f - (i * 0.1f)) * Descaling, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}