using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Items.ItemCritter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
namespace RealmOne.NPCs.Enemies.BloodMoon
{
    public class EyeJelly: ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 18;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit25;
            NPC.DeathSound = SoundID.NPCDeath28;
            NPC.knockBackResist = .35f;
            NPC.catchItem = (short)ModContent.ItemType<EyeJellyItem>();

            NPC.aiStyle = 64;
            NPC.scale = 1f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = NPCID.Firefly;
            NPC.dontCountMe = true;
            NPC.npcSlots = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("RealmOne/NPCs/Enemies/BloodMoon/EyeJelly_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer && Main.bloodMoon ? SpawnCondition.OverworldNightMonster.Chance * 0.16f : 0f;



        public override void HitEffect(NPC.HitInfo hit) 
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }


        public override void AI()
        {
            Lighting.AddLight(NPC.position, 1.1f, 0.2f, 0.6f);

            NPC.rotation = NPC.velocity.X * .15f;
            NPC.spriteDirection = NPC.direction;
        }
    }
}
