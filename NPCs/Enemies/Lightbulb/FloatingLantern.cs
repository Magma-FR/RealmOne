using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Common.Events.CursedForest;
using RealmOne.Items.Misc.EnemyDrops;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.Lightbulb
{
    public class FloatingLantern : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 32;
            NPC.damage = 10;
            NPC.defense = 1;
            NPC.lifeMax = 50;

            NPC.value = Item.buyPrice(0, 0, 3, 50);
            NPC.aiStyle = NPCAIStyleID.Bat;
            NPC.HitSound = SoundID.DD2_WitherBeastHurt;
            NPC.DeathSound = SoundID.DD2_WitherBeastHurt;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            SpawnModBiomes = new int[]
            {
                ModContent.GetInstance<CursedForestBiome>().Type
            };
        }

        // public override float SpawnChance(NPCSpawnInfo spawnInfo)
        //  {
        //      return spawnInfo.SpawnTileY < Main.rockLayer && !Main.dayTime && !Main.bloodMoon ? 0.08f : 0f;
        //  }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !(CursedForestEvent.CursedForest && spawnInfo.Player.ZoneOverworldHeight && !Main.bloodMoon)
            ? 0 : 6f;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            alphaCounter += .07f;

            Lighting.AddLight(NPC.Center, 0.411f * 2, 0.431f * 2, 0.075f * 2);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("A fragile but nimble lantern that looks for any Terrarian that stumbles in its way at night"),
            });
        }

        private float alphaCounter;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);

            float sineWave = MathHelper.Clamp(MathF.Sin(alphaCounter / 2f) + 1f, 0f, 1f);

            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int PositionX = (int)(NPC.Center.X - screenPos.X);
            int PositionY = (int)(NPC.Center.Y - screenPos.Y);
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);

            Texture2D ripple = Mod.Assets.Request<Texture2D>("Assets/Effects/lilglow").Value;

            Main.spriteBatch.Draw(ripple, new Vector2(PositionX, PositionY), null, new Color(254, 164, 56) * sineWave, NPC.rotation, ripple.Size() / 2f, 1f, effects, 0);

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("LanternGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("LanternGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("LanternGore3").Type, 1f);
            }

            for (int i = 0; i < 10; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Teleporter, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LiteBulb>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Glass, 1, 1, 5));
        }
    }
}