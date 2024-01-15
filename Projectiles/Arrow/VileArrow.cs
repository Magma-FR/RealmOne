using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Arrow
{
    public class VileArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vile Arrow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 10;

            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.timeLeft = 450;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }

        public override void OnKill(int timeleft)
        {
            for (var i = 0; i < 12; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptionThorns, 0f, 0f, 0, default, 1f);
            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }

        public override void AI()
        {
            Vector2 center = Projectile.Center;
            for (int j = 0; j < 30; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.CursedTorch, 0f, 0f, 100, default, 1f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;

                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
            }

            Lighting.AddLight(Projectile.position, 0.1f, 0.3f, 0.2f);
            Lighting.Brightness(1, 1);
        }
    }
}