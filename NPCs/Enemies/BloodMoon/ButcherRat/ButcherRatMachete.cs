using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

public class ButcherRatMachete : ModProjectile
{
    public ref float Timer => ref Projectile.ai[0];
    
    public override void SetDefaults() {
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        
        Projectile.width = 24;
        Projectile.height = 24;

        Projectile.aiStyle = -1;
        AIType = -1;
        
        Projectile.penetrate = 1;
    }

    public override void AI() {
        const float MinimumTime = 10f;

        Projectile.spriteDirection = Projectile.direction;
        Projectile.rotation += Projectile.velocity.X * 0.05f;
        
        Dust.NewDust(Projectile.Center, 0, 0, DustID.Blood);

        if (Timer++ < MinimumTime) {
            return;
        }

        Projectile.velocity.Y += 0.2f;
    }
}
