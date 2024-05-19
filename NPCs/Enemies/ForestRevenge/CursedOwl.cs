using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RealmOne.Common.Events.CursedForest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using RealmOne.Common.Core;
using ReLogic.Content;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Weapons.PreHM.Classless;
using Terraria.GameContent.ItemDropRules;

namespace RealmOne.NPCs.Enemies.ForestRevenge
{
    public class CursedOwl : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.TrailCacheLength[NPC.type] = 9;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 32;
            NPC.noGravity = true;
            NPC.damage = 9;
            NPC.lifeMax = 50;
            AIType = NPCID.ShadowFlameApparition;

            NPC.value = Item.buyPrice(0, 0, 2, 60);
            NPC.aiStyle = NPCAIStyleID.AncientVision;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.netUpdate = true;

            AIType = NPCID.EyeballFlyingFish;

            SpawnModBiomes = new int[]
            {
                GetInstance<CursedForestBiome>().Type
            };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !(CursedForestEvent.CursedForest && spawnInfo.Player.ZoneOverworldHeight && !Main.bloodMoon)
            ? 0 : 40f;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            NPC.spriteDirection = NPC.direction;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SafeHook>(), 25));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SafeHook>(), 18));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedOwlGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedOwlGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("CursedOwlGore3").Type, 1f);
            }

            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }

        private float alphaCounter;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.instance.LoadNPC(NPC.type);
            Asset<Texture2D> tex = Request<Texture2D>("RealmOne/Assets/Effects/GlowLight");
            for (int i = 0; i < NPC.oldPos.Length; i++)
            {
                var offset = new Vector2(NPC.width / 2f, NPC.height / 2f);
                var frame = tex.Frame(1, Main.npcFrameCount[NPC.type], 0);
                Vector2 drawPos = (NPC.oldPos[i] - Main.screenPosition) + offset;
                float sizec = NPC.scale * (NPC.oldPos.Length - i) / (NPC.oldPos.Length * 1.6f);
                Color color = new Color(255, 0, 0) * (2f - NPC.alpha) * ((NPC.oldPos.Length - i) / (float)NPC.oldPos.Length);
                spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, drawPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }

        /*  public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
          {
              var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
              var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
              spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
              return false;
          }*/

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color color) => GlowmMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Bleeding;

            int timeToAdd = 2 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}