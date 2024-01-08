using System;
using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

[AutoloadBossHead]
public class ButcherRat : ModNPC
{
    /*
     * NPC.ai[] is an array of floats with 4 elements. Each element can be used to represent a different value, for example, timers.
     * The differential of using NPC.ai[] instead of having your own fields is that all elements within it are automatically sent/received to/from the server.
     */

    public const float Idle = 0f;
    public const float Frenzy = 1f;
    
    // The way I'm using properties here eventually will make sense. I wont explain it here because it's a C# feature, not tML's.
    
    public float State {
        get => NPC.ai[0];
        set {
            NPC.ai[0] = value;
            
            CanSlam = true;
            NPC.netUpdate = true;
        }
    }
    
    public bool CanSlam {
        get => NPC.ai[1] == 0f;
        set => NPC.ai[1] = value ? 0f : 1f;
    }

    public ref float AttackTimer => ref NPC.ai[2];
    public ref float CollisionTimer => ref NPC.ai[3];

    public float CollisionSpeed = 4f;

    private Player Target => Main.player[NPC.target];
    
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Butcher Rat");

        Main.npcFrameCount[NPC.type] = 1;

        NPCID.Sets.ImmuneToAllBuffs[Type] = true;

        NPCID.Sets.TrailCacheLength[NPC.type] = 10;
        NPCID.Sets.TrailingMode[NPC.type] = 2;

        var value = new NPCID.Sets.NPCBestiaryDrawModifiers {
            Velocity = 1f
        };

        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }

    public override void SetDefaults() {
        NPC.netAlways = true;
        NPC.netUpdate = true;
        NPC.noGravity = false;
        NPC.boss = true;

        NPC.width = 60;
        NPC.height = 100;

        NPC.damage = 35;
        NPC.defense = 2;
        NPC.lifeMax = 1000;
        NPC.knockBackResist = 0f;

        NPC.value = Item.buyPrice(0, 2, 50, 50);

        NPC.aiStyle = -1;
        AIType = -1;

        NPC.HitSound = SoundID.NPCHit19;
        NPC.DeathSound = SoundID.NPCDeath2;

        Music = MusicID.Boss2;
    }

    public override void AI() {
        NPC.TargetClosest();

        var validTarget = Target.active && !Target.dead && !Target.ghost;

        if (!validTarget) {
            UpdateDespawn();
            return;
        }

        switch (State) {
            case Idle:
                UpdateMovement();
                UpdateThrowing();
                break;
            case Frenzy:
                break;
        }        

        UpdateSlamming();
        UpdateCollision();
    }

    private void UpdateThrowing() {
        const float MinimumDistance = 12f * 16f;
        const float Interval = 60f;
        
        var distance = MathF.Abs(Target.Center.X - NPC.Center.X);
        var nearby = distance < MinimumDistance;

        if (nearby || AttackTimer++ % Interval != 0f) {
            return;
        }
        
        var direction = MathF.Sign(Target.Center.X - NPC.Center.X);
        var speed = new Vector2(direction * 4f, -4f) + Target.velocity;
        
        var projectile = Projectile.NewProjectileDirect(new EntitySource_Parent(NPC),
            NPC.Center,
            speed,
            ModContent.ProjectileType<ButcherRatMachete>(),
            30,
            2f);

        projectile.direction = NPC.direction;
    }

    private void UpdateMovement() {
        const float MinimumDistance = 16f;

        var distance = MathF.Abs(Target.Center.X - NPC.Center.X);
        var nearby = distance < MinimumDistance;
        
        var speed = NPC.direction * 3f;

        if (nearby) {
            speed = 0f;
        }
        
        NPC.velocity.X = MathHelper.SmoothStep(NPC.velocity.X, speed, 0.1f);
    }
    
    private void UpdateSlamming() {
        if (!CanSlam) {
            return;
        }

        /*
         * Checks if the player is within at least 8 tiles from the NPC.
         * It is wise to use DistanceSQ instead of Distance. Distance uses square roots for its calculations, which costs performance.
         */
        
        const float MinimumDistance = 8f * 16f;

        var distance = Target.DistanceSQ(NPC.Center);
        var nearby = distance < MinimumDistance * MinimumDistance;

        if (!nearby) {
            return;
        }
        
        var amount = Main.rand.Next(6, 9);

        for (var i = 0; i < amount; i++) {
            var npc = NPC.NewNPCDirect(new EntitySource_Parent(NPC),
                (int)NPC.Center.X,
                (int)NPC.Center.Y,
                ModContent.NPCType<BloodRat>());
                    
            // Whenever using something like Main.rand to generate random values, it is recommended to set netUpdate to true to sync the value across the server.
            npc.velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), -2f);
            npc.netUpdate = true;
        }

        CanSlam = false;

        // Synced because the amount of NPCs spawned was randomly generated.
        NPC.netUpdate = true;
    }
    
    private void UpdateCollision() {
        const float Interval = 60f;
        
        // Makes the NPC step up on tiles that are on a different level than the current ground.
        Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.stepSpeed, ref NPC.gfxOffY);
        
        // Checks if there is a hole towards the NPC's direction.
        int tileWidth = (int)Math.Round(NPC.width / 16f);
			
        int tileX = (int)(NPC.Center.X / 16f) - tileWidth;
        int tileY = (int)((NPC.position.Y + NPC.height) / 16f);
			
        if (NPC.velocity.X > 0f) {
            tileX += tileWidth;
        }
        
        var holeBelow = true;

        for (var j = tileY; j < tileY + 2; j++) {
            for (var i = tileX; i < tileX + tileWidth; i++) {
                if (Framing.GetTileSafely(i, j).HasTile) {
                    holeBelow = false;
                }
            }
        }

        // Checks if the NPC has been stuck in the same position.
        var stuck = NPC.collideX && NPC.position.X == NPC.oldPosition.X;
        var belowTarget = Target.Center.Y < NPC.Center.Y;
        
        // Checks for the hole or if the NPC is stuck, then jump to get over it.
        if (CollisionTimer++ > Interval && (holeBelow || stuck || belowTarget)) {
            CollisionTimer = 0f;

            if (stuck) {
                
            }

            NPC.velocity.Y = -CollisionSpeed;
        }
        
        Main.NewText(stuck);
    }

    private void UpdateDespawn() {
        
    }

    public override void FindFrame(int frameHeight) {
        NPC.spriteDirection = NPC.direction;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit) {
        target.AddBuff(BuffID.Bleeding, 20 * 60);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo) {
        target.AddBuff(BuffID.Bleeding, 20 * 60);
    }

    public override void OnKill() {
        // TODO: Change the text from literal strings to localized strings.
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"[i:{ItemID.BloodyMachete}]The dreaded frenzying rodent has been slaughtered, the curse has awoken the neverending plague of rats[i:{ItemID.ButchersChainsaw}]"), new Color(249, 45, 99));

        NPC.SetEventFlagCleared(ref DownedBossSystem.downedRat, -1);

        var rat = NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<BloodRat>(), ai3: 1);

        rat.scale = 3f;
        rat.life = rat.lifeMax;
    }
    
     public override void HitEffect(NPC.HitInfo hit) {
        for (var i = 0; i < 25; i++) {
            Dust.NewDust(NPC.position, 
                NPC.width, 
                NPC.height, 
                DustID.Blood, 
                3f * hit.HitDirection, 
                -2.5f, 
                0, 
                Color.White, 
                0.9f);
        }

        if (NPC.life > 0) {
            return;
        }

        for (var i = 1; i <= 3; i++) {
            for (var j = 0; j < 2; j++) {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore" + i).Type);
            }
        }
    }   
    
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) {
        scale = 1.5f;

        return null;
    }
}
