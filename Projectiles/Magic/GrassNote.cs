using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class GrassNote : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seawater Note");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 24;

            Projectile.aiStyle = ProjAIStyleID.MusicNote;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.light = 0.6f;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 500;
            Projectile.penetrate = 3;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.2f;
        }

        public override void AI()
        {
            Projectile.aiStyle = ProjAIStyleID.MusicNote;

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water_Jungle, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.6f);
            Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.2f);
            Lighting.Brightness(1, 1);
        }

        public override void OnKill(int timeleft)
        {
            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            for (int i = 0; i < 5; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.JungleGrass, 0f, 0f, 0, default, 0.7f);
            }
        }
    }
}