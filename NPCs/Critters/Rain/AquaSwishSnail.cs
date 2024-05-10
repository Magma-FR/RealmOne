using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Items.ItemCritter;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Critters.Rain
{
    public class AquaSwishSnail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AquaSwish Snail");
            Main.npcFrameCount[NPC.type] = 6;
            Main.npcCatchable[NPC.type] = true;

            NPCID.Sets.CountsAsCritter[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.catchItem = (short)ModContent.ItemType<AquaSwishSnailItem>();
            NPC.width = 24;
            NPC.height = 20;
            NPC.dontCountMe = true;

            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.34f;
            NPC.dontTakeDamageFromHostiles = true;

            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Snail;
            AIType = NPCID.Snail;
            AnimationType = NPCID.Snail;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterSnailGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterSnailGore2").Type, 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
          => spawnInfo.Player.ZoneForest && Main.raining ? 0.5f : 0f;

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.09f, g: 0.2f, b: 0.2f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowmMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("RealmOne/NPCs/Critters/Rain/AquaSwishSnail_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Aquablossom>(), 3, 1, 3));
            //  npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterDriplets>(), 3, 1, 3));
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,

                new FlavorTextBestiaryInfoElement("From the constant dew of the morning moisture, some species of snails have been blended with a zealous and unusual source of water magic!"),
            });
        }
    }
}