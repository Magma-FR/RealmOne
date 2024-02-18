using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Underground
{
    public class HeartBat : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heartbat");
            Main.npcFrameCount[NPC.type] = 5;
            //     Main.npcCatchable[NPC.type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            //  NPC.catchItem = (short)ModContent.ItemType<ThornyDevilItem>();
            NPC.width = 20;
            NPC.height = 20;
            //  NPC.dontCountMe = true;

            NPC.damage = 8;
            NPC.defense = 5;
            NPC.lifeMax = 58;
            NPC.value = Item.buyPrice(0, 0, 5, 55);
            NPC.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
            NPC.DeathSound = SoundID.DD2_WitherBeastDeath;

            NPC.knockBackResist = 0.34f;
            // NPC.dontTakeDamageFromHostiles = true;

            //     NPC.npcSlots = 0;

            NPC.aiStyle = NPCAIStyleID.Bat;
            AIType = NPCID.CaveBat;
            AnimationType = NPCID.CaveBat;
            NPC.lifeRegen = 5;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("RealmOne/NPCs/Enemies/Underground/HeartBat_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HeartbatGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HeartbatGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HeartbatGore3").Type, 1f);
            }
            for (int k = 0; k < 16; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.LifeCrystal, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe)

                return SpawnCondition.Underground.Chance * 0.20f;
            return SpawnCondition.Cavern.Chance * 0.20f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Rabies, 180);
            target.AddBuff(BuffID.Regeneration, 120);
        }

        public override void AI()
        {
            NPC.lifeRegen += 5;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal, 20, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Heart, 1, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Heart, 1, 1, 1));

            npcLoot.Add(ItemDropRule.Common(ItemID.Heart, 1, 1, 1));

            npcLoot.Add(ItemDropRule.Common(ItemID.LifeforcePotion, 12, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.RegenerationPotion, 6, 1, 1));
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground ,

                new FlavorTextBestiaryInfoElement("Crystalised with pure life, this fortunate bat can regenerate its health super fast"),
            });
        }
    }
}