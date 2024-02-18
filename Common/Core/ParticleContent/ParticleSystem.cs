using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core.ParticleContent.Particles;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Common.Core.ParticleContent
{
    public static class ParticleSystem
    {
        public static List<Particle> particles = new List<Particle>();
        private static readonly int maxParticles = 500;

        public static void GenerateParticle(params Particle[] p)
        {
            foreach (Particle particle in p)
            {
                if (particle != null)
                    particles.Add(particle);
            }
        }

        public static void UpdateParticles()
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                Particle Particle = particles[particle];
                Particle.Update();
                Particle.Position += Particle.Velocity;
                Particle.TimeLeft--;
                Particle.Angle += Particle.AngularVelocity;
                if (particles[particle].TimeLeft <= 0 || particles.Count >= maxParticles || Particle.Kill || Particle.Size <= 0.005f)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public static void Draw()
        {
            foreach (Particle Particle in particles)
            {
                if (Particle == null || Particle.TextureName == null)
                    continue;

                string texturePath = (Particle.GetType().Namespace + "." + Particle.TextureName).Replace('.', '/');
                Particle.Texture = ModContent.Request<Texture2D>(texturePath, AssetRequestMode.ImmediateLoad).Value;

                if (Particle.Alpha == null)
                    Particle.Alpha = 0;
                int alpha = (int)MathHelper.Clamp((float)Particle.Alpha, 0f, 255f);
                if (Particle.ShouldCustomDraw == false)
                {
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default,
                        RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                    Main.EntitySpriteDraw(Particle.Texture, Particle.Position - Main.screenPosition, Particle.Texture.Bounds, Particle.Color * (1 - alpha / 255),
                        Particle.Angle, Particle.Texture.Size() / 2, Particle.Size, SpriteEffects.None, default);
                    Main.spriteBatch.End();
                }
                else
                {
                    Particle.CustomDraw(Main.spriteBatch);
                }
            }
        }

        public static void AnimateFromVerticalSpritesheet(Particle particle, int numberOfFrames, int animSpeed = 5, int? animTimer = null)
        {
            if (particle != null)
            {
                if (animTimer == null)
                    animTimer = particle.TimeLeft;
                int whichFrame = (int)(animTimer / animSpeed % numberOfFrames);
                particle.Frame = Utils.Frame(particle.Texture, 1, numberOfFrames, 0, whichFrame, 0, 0);
            }
        }

        public static void Unload()
        {
            particles = null;
        }
    }
}