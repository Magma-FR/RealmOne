using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Weapons.PreHM.BossDrops.RatDrops;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

[AutoloadBossHead]
public sealed class ButcherRat : ModNPC
{
<<<<<<< HEAD
    public static readonly SoundStyle FrenzySound = new($"{nameof(RealmOne)}/Assets/Soundss/ButcherRatFrenzy");
   
    public static readonly SoundStyle SlamSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/ButcherRatSlam") with {
        Volume = 0.8f, 
=======
    private static readonly SoundStyle FrenzySound = new($"{nameof(RealmOne)}/Assets/Soundss/ButcherRatFrenzy");

    private static readonly SoundStyle SlamSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/ButcherRatSlam") with
    {
        Volume = 0.8f,
>>>>>>> f822304aabdeeae12a350cc80118cbc02b807403
        PitchVariance = 0.2f
    };

    /// <summary>
    ///     Represents the 'Idle' state of the boss.
    /// </summary>
    public const float Idle = 0f;

    /// <summary>
    ///     Represents the 'Slamming' state of the boss.
    /// </summary>
    public const float Slamming = 1f;

    /// <summary>
    ///     Represents the 'Frenzy' state of the boss.
    /// </summary>
    public const float Frenzy = 2f;

    /// <summary>
    ///     Represents which state the boss currently is in.
    /// </summary>
    public ref float State => ref NPC.ai[0];

    /// <summary>
    ///     Represents a timer which is used for general behavior of the boss.
    /// </summary>
    public ref float Timer => ref NPC.ai[1];

    /// <summary>
    ///     Represents a timer which represents how long the boss has been on the ground.
    /// </summary>
    public ref float Ground => ref NPC.ai[2];

    /// <summary>
    ///     Represents whether the boss can perform a slam or not.
    /// </summary>
    public bool CanSlam { get; private set; } = true;

    /// <summary>
    ///     Represents the initial center from where the 'Frenzy' state started.
    /// </summary>
    public Vector2 FrenzyStart { get; private set; }

    /// <summary>
    ///     Represents the opacity used for 'Frenzy' visual effects.
    /// </summary>
    /// <remarks>This property is not synced, for its use is purely visual.</remarks>
    public float FrenzyOpacity { get; private set; }

    /// <summary>
    ///     Represents the scale used for 'Frenzy' angry visual effect.
    /// </summary>
    /// <remarks>This property is not synced, for its use is purely visual.</remarks>
    public float AngryScale { get; private set; }

    private Player Target => Main.player[NPC.target];

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Butcher Rat");

        Main.npcFrameCount[NPC.type] = 7;

        NPCID.Sets.ImmuneToAllBuffs[Type] = true;

        NPCID.Sets.TrailCacheLength[NPC.type] = 10;
        NPCID.Sets.TrailingMode[NPC.type] = 2;

        var value = new NPCID.Sets.NPCBestiaryDrawModifiers
        {
            Velocity = 1f
        };

        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetDefaults()
    {
        NPC.noGravity = false;
        NPC.boss = true;

        NPC.width = 80;
        NPC.height = 150;

        NPC.damage = 35;
        NPC.defense = 2;
        NPC.lifeMax = 1100;
        NPC.knockBackResist = 0f;

        NPC.value = Item.buyPrice(gold: 5);

        NPC.aiStyle = -1;
        AIType = -1;

        NPC.HitSound = SoundID.NPCHit19;
        NPC.DeathSound = SoundID.NPCDeath2;

        Music = MusicID.Boss2;
        SceneEffectPriority = SceneEffectPriority.BossHigh;
    }

    public override void FindFrame(int frameHeight)
    {
        NPC.spriteDirection = NPC.direction;

        // Visually, states are more than just a flag.
        // This is only verified this way to ensure it does not look weird in-game.
        var chasing = NPC.velocity.X != 0f;
        var throwing = NPC.velocity.X < 3f && NPC.frame.Y != 0;

        var frameRate = State == Frenzy ? 2.5f : 5f;

        NPC.frameCounter++;

        if (NPC.frameCounter < frameRate || !(chasing || throwing))
        {
            return;
        }

        NPC.frame.Y += frameHeight;
        NPC.frameCounter = 0f;

        if (NPC.frame.Y < Main.npcFrameCount[Type] * frameHeight)
        {
            return;
        }

        NPC.frame.Y = 0;
    }

    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(CanSlam);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        CanSlam = reader.ReadBoolean();
    }

    public override void AI()
    {
        NPC.TargetClosest();

        if (!Target.active || Target.dead || Target.ghost)
        {
            UpdateDespawn();
            return;
        }

        switch (State)
        {
            case Idle:
                UpdateIdle();
                UpdateThrow();
                UpdateState();
                break;

            case Slamming:
                UpdateSlam();
                break;

            case Frenzy:
                UpdateFrenzy();
                break;
        }

        UpdateCollision();
    }

    private void UpdateDespawn()
    {
        NPC.velocity.X *= 0.95f;

        if (NPC.velocity.X != 0f)
        {
            return;
        }

        NPC.alpha += 5;

        if (NPC.alpha < 255)
        {
            return;
        }

        NPC.active = false;
    }

    private void UpdateState()
    {
        const float MinimumDistance = 8f * 16f;

        if (NPC.velocity.Y == 0f && Main.rand.NextBool(200))
        {
            FrenzyStart = NPC.Center;

            State = Frenzy;

            Timer = 0f;

            CanSlam = false;

            NPC.velocity.Y = -2f;

            NPC.netUpdate = true;

            var amount = 10;

            for (var i = 0; i < 20; i++)
            {
                Dust.NewDust(
                    NPC.position,
                    NPC.width,
                    NPC.height,
                    ModContent.DustType<ButcherDust>()
                );
            }

            SoundEngine.PlaySound(in FrenzySound, NPC.Center);
        }

        if (!CanSlam)
        {
            return;
        }

        var targetDisance = Target.DistanceSQ(NPC.Center);
        var targetNearby = targetDisance < MinimumDistance * MinimumDistance;

        if (!targetNearby)
        {
            return;
        }

        State = Slamming;

        Timer = 0f;

        CanSlam = true;

        NPC.netUpdate = true;

        SoundEngine.PlaySound(in SlamSound, NPC.Center);
    }

    private void UpdateIdle()
    {
        const float MinimumDistance = 10f * 16f;
        const float MaximumDistance = 20f * 16f;

        const float MaximumSpeed = 3f;

        var targetDistance = MathF.Abs(Target.Center.X - NPC.Center.X);
        var targetWithinRange = targetDistance > MinimumDistance && targetDistance < MaximumDistance;

        if (targetWithinRange)
        {
            NPC.velocity.X *= 0.5f;
            return;
        }

        NPC.velocity.X = MathHelper.SmoothStep(NPC.velocity.X, MaximumSpeed * NPC.direction, 0.1f);
    }

    private void UpdateThrow()
    {
        const float Interval = 60f;

        var targetDistance = MathF.Abs(Target.Center.X - NPC.Center.X);
        var canHitTarget = Terraria.Collision.CanHit(NPC, Target);

        if (!canHitTarget)
        {
            return;
        }

        Timer++;

        if (Timer % Interval != 0f)
        {
            return;
        }

        var direction = MathF.Sign(Target.Center.X - NPC.Center.X);
        var prediction = new Vector2(Target.velocity.X, Target.velocity.Y < 0f ? Target.velocity.Y : 0f);

        if (direction == -1)
        {
            prediction.X = MathHelper.Clamp(prediction.X, -Target.velocity.X, 0f);
        }
        else
        {
            prediction.X = MathHelper.Clamp(prediction.X, 0f, Target.velocity.X);
        }

        var velocity = new Vector2(direction * (6f + prediction.X), -4f - prediction.Y);

        var projectile = Projectile.NewProjectileDirect(
            new EntitySource_Parent(NPC),
            NPC.Center,
            velocity,
            ModContent.ProjectileType<ButcherRatMachete>(),
            30,
            2f,
            -1,
            0f,
            0f,
            NPC.whoAmI
        );

        projectile.direction = NPC.direction;
    }

    private void UpdateSlam()
    {
        var amount = Main.rand.Next(3, 7);

        for (var i = 0; i < amount; i++)
        {
            var rat = NPC.NewNPCDirect(
                new EntitySource_Parent(NPC),
                (int)NPC.Center.X,
                (int)NPC.Center.Y,
                ModContent.NPCType<BloodRat>()
            );

            rat.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), -2f);
            rat.netUpdate = true;
        }

        State = Idle;

        CanSlam = false;

        NPC.netUpdate = true;
    }

    private void UpdateFrenzy()
    {
        const float Distance = 16f;

        const float Charge = 60f;
        const float Duration = Charge + 180f;

        const float MaximumSpeed = 6f;

        Timer++;

        if (Timer < Charge)
        {
            NPC.Center = new Vector2(
                FrenzyStart.X + Main.rand.NextFloat(-2f, 4f),
                NPC.Center.Y
            );

            return;
        }

        var targetDistance = MathF.Abs(Target.Center.X - NPC.Center.X);
        var targetWithinRange = targetDistance < Distance;

        if (targetWithinRange)
        {
            NPC.velocity.X *= 0.9f;
            return;
        }

        NPC.velocity.X = MathHelper.SmoothStep(NPC.velocity.X, MaximumSpeed * NPC.direction, 0.1f);

        if (Timer < Duration)
        {
            return;
        }

        State = Idle;

        Timer = 0f;

        CanSlam = true;

        NPC.netUpdate = true;
    }

    private void UpdateCollision()
    {
        const float Interval = 60f;

        if (NPC.wet)
        {
            if (NPC.collideY)
            {
                NPC.velocity.Y = -2f;
            }

            if (NPC.velocity.Y > 2f)
            {
                NPC.velocity.Y *= 0.9f;
            }
            else if (NPC.directionY < 0)
            {
                NPC.velocity.Y -= 0.8f;
            }

            NPC.velocity.Y -= 0.5f;

            if (NPC.velocity.Y < -4f)
            {
                NPC.velocity.Y = -4f;
            }
            return;
        }

        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);

        var tileWidth = (int)Math.Round(NPC.width / 16f);

        var tileX = (int)(NPC.Center.X / 16f) - tileWidth;
        var tileY = (int)((NPC.position.Y + NPC.height) / 16f);

        if (NPC.velocity.X > 0f)
        {
            tileX += tileWidth;
        }

        var holeBelow = true;

        for (var j = tileY; j < tileY + 2; j++)
        {
            for (var i = tileX; i < tileX + tileWidth; i++)
            {
                holeBelow &= !Framing.GetTileSafely(i, j).HasTile;
            }
        }

        var stuck = NPC.collideX && NPC.position.X == NPC.oldPosition.X;
        var belowTarget = Target.Center.Y < NPC.Center.Y;

        if (NPC.velocity.Y == 0f)
        {
            Ground++;
        }

        if (Ground < Interval || !(holeBelow || stuck || belowTarget))
        {
            return;
        }

        Ground = 0f;

        NPC.velocity.Y = -Main.rand.NextFloat(8f, 12f);
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        var texture = ModContent.Request<Texture2D>(Texture).Value;

        var position = NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY);
        var effects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        Main.EntitySpriteDraw(
            texture,
            position,
            NPC.frame,
            NPC.GetAlpha(drawColor),
            NPC.rotation,
            NPC.frame.Size() / 2f,
            NPC.scale,
            effects
        );

        var frenzy = State == Frenzy;

        FrenzyOpacity = MathHelper.Lerp(FrenzyOpacity, frenzy ? 1f : 0f, 0.1f);

        AngryScale = MathHelper.Lerp(
            AngryScale,
            frenzy ? NPC.scale + MathF.Sin(Main.GameUpdateCount * 0.1f) * 0.25f : 0f,
            0.1f
        );

        var angry = ModContent.Request<Texture2D>(Texture + "_Angry").Value;

        Main.EntitySpriteDraw(
            angry,
            position - new Vector2(20f * -NPC.spriteDirection, 52f),
            null,
            NPC.GetAlpha(drawColor) * FrenzyOpacity,
            NPC.rotation,
            angry.Size() / 2f,
            AngryScale,
            SpriteEffects.None
        );

        var outline = ModContent.Request<Texture2D>(Texture + "_Glow").Value;

        Main.EntitySpriteDraw(
            outline,
            position,
            NPC.frame,
            NPC.GetAlpha(drawColor) * FrenzyOpacity,
            NPC.rotation,
            NPC.frame.Size() / 2f,
            NPC.scale,
            effects
        );

        return false;
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) => !NPC.AnyNPCs(Type) && Main.bloodMoon && spawnInfo.Player.Center.Y / 16f < Main.worldSurface ? 0.021f : 0f;

    public override void HitEffect(NPC.HitInfo hit)
    {
        for (var i = 0; i < 20; i++)
        {
            Dust.NewDust(
                NPC.position,
                NPC.width,
                NPC.height,
                ModContent.DustType<ButcherDust>()
            );
        }

        if (NPC.life > 0)
        {
            return;
        }

        for (var i = 1; i <= 3; i++)
        {
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore" + i).Type);
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit)
    {
        target.AddBuff(BuffID.Bleeding, 20 * 60);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
        target.AddBuff(BuffID.Bleeding, 20 * 60);
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ItemID.RatCage, 5));
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoreshankShotgun>()));
    }

    public override void OnKill()
    {
        ChatHelper.BroadcastChatMessage(
            NetworkText.FromKey($"Mods.{nameof(RealmOne)}.Messages.ButcherRat"),
            new Color(249, 45, 99)
        );

        NPC.SetEventFlagCleared(ref DownedBossSystem.downedRat, -1);

        NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<BloodRat>());
    }

    public override void BossLoot(ref string name, ref int potionType)
    {
        potionType = ItemID.None;

        NPCLoader.blockLoot.Add(ItemID.Heart);
    }

    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        scale = 1.5f;

        return null;
    }

    public override bool CanHitPlayer(Player target, ref int cooldownSlot)
    {
        cooldownSlot = ImmunityCooldownID.Bosses;

        return true;
    }
}