using Microsoft.Xna.Framework;
using Terraria;

namespace RealmOne.Common.Core.ParticleContent.Particles
{
    public class SparkleParticle : Particle
    {
        public SparkleParticle(Color color, float scale, Vector2 position, Vector2 velocity, int alpha, int maxTime = 60)
        {
            Color = color;
            Size = scale;
            Position = position;
            Velocity = velocity;
            TimeLeft = maxTime;
            TextureName = "SparkleParticle";
            Alpha = alpha;
        }

        public override void Update()
        {
            Lighting.AddLight(Position, Color.R / 255, Color.G / 255, Color.B / 255);
            Velocity = Velocity.RotatedByRandom(MathHelper.ToRadians(2)) * 0.99f;
            Size *= 0.90f;
            AngularVelocity = Velocity.ToRotation();
        }
    }
}