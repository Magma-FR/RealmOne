using Microsoft.Xna.Framework;
using RealmOne.Items.Food;
using RealmOne.Items.Placeables.BannerItems;
using RealmOne.Items.Weapons.PreHM.Throwing;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.Forest
{
    public class AcornStiltWalker : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acorn Stilt Walker");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GiantWalkingAntlion];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 108;
            NPC.damage = 13;
            NPC.defense = 2;
            NPC.lifeMax = 85;
            NPC.value = Item.buyPrice(silver: 8);
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.DD2_KoboldFlyerHurt;
            NPC.DeathSound = SoundID.DD2_KoboldFlyerDeath;
            NPC.netAlways = true;
            NPC.netUpdate = true;

            AIType = NPCID.GiantWalkingAntlion;
            AnimationType = NPCID.GiantWalkingAntlion;
            Banner = Type;
            BannerItem = ModContent.ItemType<BannerItem.AcornStiltB>();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneForest && Main.dayTime && !spawnInfo.PlayerSafe ? 0.14f : 0f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("Being in the forest gets a bit boring sometimes, so this Acorn found some tree branches and stuck it to its legs! "),
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("AcornStiltGore3").Type, 1f);
            }

            for (int k = 0; k < 15; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WoodFurniture, 3.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Acorn, 1, 2, 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.Wood, 2, 4, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.LivingWoodWand, 40, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.PortableStool, 42, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToastedNutBar>(), 20));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PoisonPrickles>(), 13, 5, 8));
        }
    }
}