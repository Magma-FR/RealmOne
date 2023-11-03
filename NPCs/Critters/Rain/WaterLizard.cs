using Microsoft.Xna.Framework;
using RealmOne.Items.ItemCritter;
using RealmOne.Items.Misc;
using RealmOne.Items.Misc.Plants;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Critters.Rain
{
    public class WaterLizard : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Lizard");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GoldfishWalker];
            Main.npcCatchable[NPC.type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            NPCID.Sets.CountsAsCritter[Type] = true;

        }

        public override void SetDefaults()
        {

            NPC.catchItem = (short)ModContent.ItemType<WaterLizardCritter>();
            NPC.width = 40;
            NPC.height = 20;
            NPC.dontCountMe = true;

            NPC.defense = 0;
            NPC.lifeMax = 75;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.34f;
            NPC.dontTakeDamageFromHostiles = true;

            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Passive;
            AnimationType = NPCID.GoldfishWalker;

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterLizardGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterLizardGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("WaterLizardGore3").Type, 1f);

            }

            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
            => spawnInfo.Player.ZoneForest && Main.raining ? 0.4f : 0f;

        int Watertimer = 0;

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.02f, g: 0.7f, b: 1.1f);
            Watertimer++;

            if (Watertimer == 9)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Water);
                Main.dust[d].scale = 1.2f;
                Main.dust[d].velocity *= 0.6f;
                Main.dust[d].noLight = false;

                Watertimer = 0;
            }
        }

        /*  public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
          {
              drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
              var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
              spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY), NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
              return false;
          }*/
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
        //    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Aquablossom>(), 3, 1, 3));
        //    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterDriplets>(), 3, 1, 3));


        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,

                new FlavorTextBestiaryInfoElement("Bred from the lush and damp waters of the ponds, this overly large amphibian seems to walk around when its nice and wet."),

            });
        }
    }
}
