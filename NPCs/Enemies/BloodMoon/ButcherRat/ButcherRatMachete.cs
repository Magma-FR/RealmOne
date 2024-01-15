using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

public class ButcherRatMachete : ModProjectile
{
    public ref float Timer => ref Projectile.ai[0];

    public bool StickingToTile
    {
        get => Projectile.ai[1] == 1f;
        set => Projectile.ai[1] = value ? 1f : 0f;
    }

    public override void SetDefaults()
    {
        Projectile.ignoreWater = true;
        Projectile.hostile = true;

        Projectile.width = 20;
        Projectile.height = 20;

        Projectile.aiStyle = -1;
        AIType = -1;

        Projectile.penetrate = 1;
        Projectile.timeLeft = 180;
    }

    public override void AI()
    {
        const float MinimumTime = 10f;

        if (Projectile.timeLeft < 255 / 25)
        {
            Projectile.alpha += 25;
        }

        UpdateTileStick();

        if (StickingToTile)
        {
            return;
        }

        Projectile.spriteDirection = Projectile.direction;
        Projectile.rotation += Projectile.velocity.X * 0.05f;

        Dust.NewDust(Projectile.Center, 0, 0, DustID.Blood);

        if (Timer++ < MinimumTime)
        {
            return;
        }

        Projectile.velocity.Y += 0.2f;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (StickingToTile)
        {
            return false;
        }

        SoundEngine.PlaySound(in SoundID.Dig, Projectile.Center);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

        StickingToTile = true;

        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI);

        return false;
    }

    private void UpdateTileStick()
    {
        if (!StickingToTile)
        {
            return;
        }

        Projectile.velocity *= 0.5f;
    }
}