using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Melee;

public class BloodthirstyVerminsaw : ModProjectile
{
    private sealed class VerminsawLoopAudioManager : ModSystem
    {
        public static readonly SoundStyle Loop = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/BloodthirstyVerminsawLoop") with {
            Volume = 0.5f,
            IsLooped = true,
            MaxInstances = 1
        };
        
        private static SlotId Slot { get; set; } = SlotId.Invalid;

        private static float volume;

        private static float Volume {
            get => volume;
            set => volume = MathHelper.Clamp(value, 0f, 0.5f);
        }
        
        private static bool Active { get; set; }

        public static void Play(Vector2 position) {
            Slot = SoundEngine.PlaySound(in Loop, position);
     
            Volume = 0f;
            Active = true;

            if (!SoundEngine.TryGetActiveSound(Slot, out var sound)) {
                return;
            }

            sound.Volume = Volume;
        }

        public static void Stop() {
            if (!SoundEngine.TryGetActiveSound(Slot, out var sound)) {
                return;
            }

            Active = false;
        }

        public override void PreUpdateWorld() {
            if (!SoundEngine.TryGetActiveSound(Slot, out var sound)) {
                return;
            }

            if (Active) {
                Volume += 0.005f;
            }
            else {
                Volume -= 0.05f;

                if (Volume <= 0f) {
                    sound.Stop();
                    
                    Slot = SlotId.Invalid;
                }
            }
            
            sound.Volume = Volume;
        }
    }
    
    private Vector2 Scale { get; set; } = Vector2.UnitY;
    
    private ref float Timer => ref Projectile.ai[0];
    
    private Player Owner => Main.player[Projectile.owner];
    
    public override void SetStaticDefaults() {
        Main.projFrames[Type] = 2;
    }

    public override void SetDefaults() {
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;

        Projectile.width = 40;
        Projectile.height = 40;

        Projectile.aiStyle = -1;
        
        Projectile.penetrate = -1;
    }
    
    public override void OnKill(int timeLeft) {
        VerminsawLoopAudioManager.Stop();
    }

    public override void ModifyDamageHitbox(ref Rectangle hitbox) {
        var direction = Projectile.rotation.ToRotationVector2() * 32f;

        hitbox.X += (int)direction.X;
        hitbox.Y += (int)direction.Y;
    }

    public override void AI() {
        if (!Owner.active || Owner.dead || !Owner.channel) {
            return;
        }

        Timer++;

        if (Timer == 70f) {
            VerminsawLoopAudioManager.Play(Projectile.Center);
        }

        Projectile.Center = Owner.MountedCenter;
        Projectile.timeLeft = 2;

        Owner.heldProj = Projectile.whoAmI;
        Owner.itemTime = Projectile.timeLeft;
        Owner.itemAnimation = Projectile.timeLeft;
        
        var direction = Math.Sign(Main.MouseWorld.X - Projectile.Center.X);

        if (Owner.direction != direction) {
            Owner.ChangeDir(direction);

            if (Main.netMode == NetmodeID.SinglePlayer) {
                return;
            }
            
            NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, Owner.whoAmI);
        }

        Projectile.direction = direction;
        Projectile.spriteDirection = direction;
        
        Projectile.rotation = Projectile.rotation.AngleLerp(Projectile.AngleTo(Main.MouseWorld), 0.2f);

        var offset = MathHelper.PiOver2;
        
        Owner.SetCompositeArmFront(
            true, 
            Player.CompositeArmStretchAmount.Full, 
            Projectile.rotation - offset
        );
        
        Owner.SetCompositeArmBack(
            true,
            Player.CompositeArmStretchAmount.Quarter, 
            Projectile.rotation - offset
        );
        
        Scale = Vector2.SmoothStep(Scale, Vector2.One, 0.2f);
    }

    public override bool PreDraw(ref Color lightColor) {
        var texture = ModContent.Request<Texture2D>(Texture).Value;
        var frame = texture.Frame(1, Main.projFrames[Type], 0, Projectile.frame);
        var origin = new Vector2(frame.Width / 4f, frame.Height / 2f);
        
        var position = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

        var effects = Projectile.spriteDirection == -1
            ? SpriteEffects.FlipVertically
            : SpriteEffects.None;

        Main.EntitySpriteDraw(
            texture,
            position,
            frame,
            Projectile.GetAlpha(lightColor),
            Projectile.rotation,
            origin,
            Scale * Projectile.scale,
            effects
        );
        
        return false;
    }
}
