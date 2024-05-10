using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

public class BloodRat : ModNPC
{
    /// <summary>
    ///     Represents the index of the NPC's parent.
    ///     Normally, this will be used to identify 'Butcher Rat' spawns.
    /// </summary>
    public ref float Index => ref NPC.ai[0];

    private NPC Parent => Main.npc[(int)Index];
    
    public override void SetStaticDefaults() {
        Main.npcFrameCount[NPC.type] = 3;
        
        var value = new NPCID.Sets.NPCBestiaryDrawModifiers {
            Velocity = 0.7f
        };
        
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
        // TODO: Use localization instead of raw strings.
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
            new FlavorTextBestiaryInfoElement("The offspring of the horrendous scavenger of the bloody sky, these lil terrors jump on the player to rip your flesh out!")
        });
    }

    public override void SetDefaults() {
        NPC.noGravity = false;
        NPC.noTileCollide = false;
        NPC.friendly = false;

        NPC.width = 26;
        NPC.height = 28;

        NPC.damage = 7;
        NPC.lifeMax = 39;

        AIType = NPCID.LarvaeAntlion;
        NPC.aiStyle = NPCAIStyleID.Fighter;

        NPC.HitSound = SoundID.NPCHit18;
        NPC.DeathSound = SoundID.NPCDeath4;
    }

    public override void FindFrame(int frameHeight) {
        // TODO: Change this to not use modulus later.
        NPC.frameCounter += 0.15f;
        NPC.frameCounter %= Main.npcFrameCount[NPC.type];
        
        var frame = (int)NPC.frameCounter;
        
        NPC.frame.Y = frame * frameHeight;
    }

    public override void OnSpawn(IEntitySource source) {
        if (source is not EntitySource_Parent parent) {
            return;
        }

        Index = parent.Entity.whoAmI;
    }

    public override void AI() {
        var player = Main.player[NPC.target];
        
        NPC.TargetClosest();
        
        NPC.spriteDirection = NPC.direction;
    }

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
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

        if (Parent == null || !Parent.active || Parent.ModNPC is not ButcherRat butcher) {
            return false;
        }
        
        var angry = ModContent.Request<Texture2D>(Texture + "_Angry").Value;

        Main.EntitySpriteDraw(
            angry,
            position - new Vector2(6f * -NPC.spriteDirection, 12f),
            null,
            NPC.GetAlpha(drawColor) * butcher.FrenzyOpacity,
            NPC.rotation,
            angry.Size() / 2f,
            butcher.AngryScale * 0.5f,
            SpriteEffects.None
        );
        
        var outline = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
        
        Main.EntitySpriteDraw(
            outline,
            position,
            NPC.frame,
            NPC.GetAlpha(drawColor) * butcher.FrenzyOpacity,
            NPC.rotation,
            NPC.frame.Size() / 2f,
            NPC.scale,
            effects
        );
        
        return false;
    }

    public override void HitEffect(NPC.HitInfo hit) {
        for (var i = 0; i < 5; i++) {
            Dust.NewDust(
                NPC.position,
                NPC.width, 
                NPC.height, 
                ModContent.DustType<ButcherDust>(),
                2.5f * hit.HitDirection, 
                -2.5f
            );
        }

        if (NPC.life > 0 && Main.netMode != NetmodeID.Server) {
            return;
        }

        for (var i = 1; i <= 3; i++) {
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("lilratgore" + i).Type);
        }
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo) {
        return spawnInfo.SpawnTileY < Main.rockLayer && Main.bloodMoon 
            ? SpawnCondition.OverworldNightMonster.Chance * 0.12f 
            : 0f;
    }
}
