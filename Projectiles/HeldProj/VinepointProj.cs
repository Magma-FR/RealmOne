using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.HeldProj;

public class VinepointProj : ModProjectile
{
    protected virtual float HoldoutRangeMin => 10f;
    protected virtual float HoldoutRangeMax => 90f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Bone Spine");
        ProjectileID.Sets.TrailCacheLength[Type] = 4;
        ProjectileID.Sets.TrailingMode[Type] = 0;
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Spear);
    }


    public override bool PreAI()
    {
        for (int i = 0; i < 2; ++i)
        {
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Plantera_Green, Projectile.velocity.X * 0.7f, Projectile.velocity.Y * 0.7f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.4f;
            Main.dust[dust].scale = 1f;
        }

        Player player = Main.player[Projectile.owner];
        int duration = player.itemAnimationMax;

        player.heldProj = Projectile.whoAmI;

        if (Projectile.timeLeft > duration)
            Projectile.timeLeft = duration;

        Projectile.velocity = Vector2.Normalize(Projectile.velocity);
        float halfDuration = duration * 0.4f;
        float progress;

        if (Projectile.timeLeft < halfDuration)
            progress = Projectile.timeLeft / halfDuration;
        else
            progress = (duration - Projectile.timeLeft) / halfDuration;

        Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

        if (Projectile.spriteDirection == -1)

            Projectile.rotation += MathHelper.ToRadians(45f);
        else

            Projectile.rotation += MathHelper.ToRadians(135f);

        return false;
    }
}