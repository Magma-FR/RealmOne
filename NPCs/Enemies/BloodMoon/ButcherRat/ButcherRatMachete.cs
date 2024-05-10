using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

public class ButcherRatMachete : ModProjectile
{
    /// <summary>
    ///     Represents whether the projectile is sticking to a tile or not.
    /// </summary>
    public bool Sticking {
        get => Projectile.ai[0] == 1f;
        set => Projectile.ai[0] = value ? 1f : 0f;
    }
    
    /// <summary>
    ///     Represents a timer which is used for general behavior of the projectile.
    /// </summary>
    public ref float Timer => ref Projectile.ai[1];

    /// <summary>
    ///     Represents the index of the Projectile's parent.
    ///     Normally, this will be used to identify 'Butcher Rat' spawns.
    /// </summary>
    public ref float Index => ref Projectile.ai[2];
    
    private NPC Parent => Main.npc[(int)Index];

    public override void SetDefaults() {
        Projectile.ignoreWater = true;
        Projectile.hostile = true;

        Projectile.width = 20;
        Projectile.height = 20;

        Projectile.aiStyle = -1;
        AIType = -1;

        Projectile.penetrate = 1;
        Projectile.timeLeft = 180;
    }

    public override void AI() {
        const float MinimumTime = 10f;

        if (Projectile.timeLeft < 255 / 25) {
            Projectile.alpha += 25;
        }

        UpdateTileStick();

        if (Sticking) {
            return;
        }

        Projectile.spriteDirection = Projectile.direction;
        
        Projectile.rotation += Projectile.velocity.X * 0.05f;

        if (Main.rand.NextBool(5)) {
            Dust.NewDust(
                Projectile.Center, 
                Projectile.width, 
                Projectile.height, 
                ModContent.DustType<ButcherDust>()
            );
        }
        
        Timer++;
        
        if (Timer < MinimumTime) {
            return;
        }

        Projectile.velocity.Y += 0.2f;
    }

    public override bool PreDraw(ref Color lightColor) {
        var texture = ModContent.Request<Texture2D>(Texture).Value;
        
        var drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        Main.EntitySpriteDraw(
            texture, 
            drawPosition, 
            null,
            Projectile.GetAlpha(lightColor),
            Projectile.rotation, 
            texture.Size() / 2f, 
            Projectile.scale,
            SpriteEffects.None
        );
        
        if (Parent == null || !Parent.active || Parent.ModNPC is not ButcherRat butcher) {
            return false;
        }
        
        var glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

        Main.EntitySpriteDraw(
            glow, 
            drawPosition, 
            null,
            Projectile.GetAlpha(Color.White) * butcher.FrenzyOpacity,
            Projectile.rotation, 
            glow.Size() / 2f, 
            Projectile.scale,
            SpriteEffects.None
        );
        
        return false;
    }
    
    public override bool OnTileCollide(Vector2 oldVelocity) {
        if (Sticking) {
            return false;
        }
        
        var amount = 5;
        
        for (var i = 0; i < amount; i++) {
            var rotation = i * MathHelper.TwoPi / amount;
            var velocity = rotation.ToRotationVector2();
                
            Dust.NewDustDirect(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                ModContent.DustType<ButcherDust>(),
                velocity.X,
                velocity.Y
            );
        }

        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
       
        SoundEngine.PlaySound(in SoundID.Dig, Projectile.Center);
        
        Sticking = true;

        return false;
    }
    
    private void UpdateTileStick() {
        if (!Sticking) {
            return;
        }

        Projectile.velocity *= 0.5f;
    }
}
