using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RealmOne.Common.Core.ParticleContent.Particles
{
    public abstract class Particle
    {
        public Texture2D Texture;
        public string TextureName;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Angle;
        public float AngularVelocity;
        public Color Color;
        public float Size;
        public int TimeLeft;
        public Rectangle? Frame;
        public int? Alpha;

        public virtual void CustomDraw(SpriteBatch sb)
        { }

        public virtual void Update()
        { }

        public virtual bool ShouldCustomDraw => false;
        public virtual bool Kill => false;
    }
}