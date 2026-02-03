using System;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Items.Sets.ForestRevengeSet;
using RealmOne.Items.Weapons.Melee;
using RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;
using ReLogic.Utilities;
using StructureHelper;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Melee;

public class BloodthirstyVerminsawProj : ModProjectile
{
    private sealed class VerminsawLoopAudioManager : ModSystem
    {
        public static readonly SoundStyle Loop = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/BloodthirstyVerminsawLoop") with
        {
            Volume = 0.5f,
            IsLooped = true,
            MaxInstances = 1
        };

        private static SlotId Slot { get; set; } = SlotId.Invalid;

        private static float volume;

        private static float Volume
        {
            get => volume;
            set => volume = MathHelper.Clamp(value, 0f, 0.5f);
        }

        private static bool Active { get; set; }

        public static void Play(Vector2 position)
        {
            Slot = SoundEngine.PlaySound(in Loop, position);

            Volume = 0f;
            Active = true;

            if (!SoundEngine.TryGetActiveSound(Slot, out var sound))
            {
                return;
            }

            sound.Volume = Volume;
        }

        public static void Stop()
        {
            if (!SoundEngine.TryGetActiveSound(Slot, out var sound))
            {
                return;
            }

            Active = false;
        }

        public override void PreUpdateWorld()
        {
            if (!SoundEngine.TryGetActiveSound(Slot, out var sound))
            {
                return;
            }

            if (Active)
            {
                Volume += 0.005f;
            }
            else
            {
                Volume -= 0.05f;

                if (Volume <= 0f)
                {
                    sound.Stop();

                    Slot = SlotId.Invalid;
                }
            }

            sound.Volume = Volume;
        }
    }

    private Vector2 Scale { get; set; } = Vector2.UnitY;
    private Vector2 direction;

    private int hitAmount;

    public bool justHit = false;
    private float maxTimeLeft;

    private int _charge = 0;
    private int _endCharge = -1;

    private const int MinimumCharge = 0; //How long it takes for a minimum charge - 1/2 second by default

    private float Scaling => ((_charge - MinimumCharge) * 0.05f) + 1f; //Scale factor for projectile damage, spread and speed
    private float ScalingCapped => Scaling >= 4f ? 4f : Scaling; //Cap for scaling so there's not super OP charging lol

    private ref float Timer => ref Projectile.ai[0];

    private Player Owner => Main.player[Projectile.owner];

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 2;
    }

    private Player player = Main.LocalPlayer;

    public override void SetDefaults()
    {
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Melee;

        Projectile.width = 40;
        Projectile.height = 40;

        Projectile.aiStyle = -1;

        Projectile.penetrate = -1;
    }

    public override void OnKill(int timeLeft)
    {
        VerminsawLoopAudioManager.Stop();
    }

    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        var direction = Projectile.rotation.ToRotationVector2() * 32f;

        hitbox.X += (int)direction.X;
        hitbox.Y += (int)direction.Y;
    }

    public override void AI()
    {
        if (!Owner.active || Owner.dead || !Owner.channel)
        {
            return;
        }

        Timer++;

        if (Timer == 70f)
        {
            VerminsawLoopAudioManager.Play(Projectile.Center);
        }
        if (Timer >= 400)
        {
            Projectile.damage = 35;
            player.AddBuff(ModContent.BuffType<SawBuff>(), 10);
            player.velocity *= 1.0018f;
        }

        Projectile.Center = Owner.MountedCenter;
        Projectile.timeLeft = 2;

        Owner.heldProj = Projectile.whoAmI;
        Owner.itemTime = Projectile.timeLeft;
        Owner.itemAnimation = Projectile.timeLeft;

        var direction = Math.Sign(Main.MouseWorld.X - Projectile.Center.X);

        if (Owner.direction != direction)
        {
            Owner.ChangeDir(direction);

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
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

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 9; i++)
        {
            Vector2 directionTo = target.DirectionTo(Owner.Center);
            if (!target.friendly)
            {
                Dust.NewDustPerfect(target.Center + directionTo * 10 + new Vector2(0, 0), ModContent.DustType<ButcherDust>(), directionTo.RotatedBy(Main.rand.NextFloat(-0.6f, 0.6f) + 3.14f) * -Main.rand.NextFloat(0.5f, 5f), 0, new Color(255, 230, 60) * 0.8f, 1);
                Dust.NewDustPerfect(target.Center + directionTo * 10 + new Vector2(0, 0), DustID.TreasureSparkle, directionTo.RotatedBy(Main.rand.NextFloat(-0.6f, 0.6f) + 3.14f) * -Main.rand.NextFloat(0.5f, 5f), 0, Scale: 1.2f);
            }
            else
            {
                for (int j = 0; j < 6; j++)
                {
                    Dust.NewDustPerfect(target.Center + directionTo * 10, DustID.Blood, directionTo.RotatedBy(Main.rand.NextFloat(-0.2f, 0.2f) - 1.57f) * -Main.rand.NextFloat(0.5f, 5f), 0, default, 0.7f);
                }

                Dust.NewDustPerfect(target.Center + directionTo * 10, ModContent.DustType<ButcherDust>(), Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-0.2f, 0.2f)) * -Main.rand.NextFloat(3f, 5f), 0, default, 1.1f);
            }
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Color color = Projectile.GetAlpha(lightColor);
        if (Timer >= 400)
        {
            color = Color.Lerp(lightColor, Color.OrangeRed, Timer / 1000);
            if (Timer % 20 == 0)
            {
                color = Color.Lerp(lightColor, Color.OrangeRed, Timer / 1000);

                Gore.NewGore(Projectile.GetSource_FromThis(), Main.rand.NextVector2FromRectangle(Projectile.Hitbox), new(0, Main.rand.Next(-7, -3)), GoreID.Smoke1, Scale: 0.55f);
            }
        }
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
            color,
            Projectile.rotation,
            origin,
            Scale * Projectile.scale,
            effects
        );

        return false;
    }
}