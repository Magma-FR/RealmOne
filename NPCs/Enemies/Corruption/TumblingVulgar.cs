using Microsoft.Xna.Framework;
using RealmOne.Items.Food;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Corruption
{
    public class TumblingVulgar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
           

        }

        public override void SetDefaults()
        {

            NPC.width = 26;
            NPC.height = 28;
            NPC.height = 38;
            NPC.defense = 1;
            NPC.damage = 20;
            NPC.lifeMax = 100;
            NPC.value = Item.buyPrice(0, 0, 6, 5);
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.knockBackResist = 0.50f;
            NPC.friendly = false;

            NPC.HitSound = SoundID.ChesterOpen;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.aiStyle = NPCAIStyleID.Unicorn;


        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneCorrupt)
            {
                return SpawnCondition.Corruption.Chance * 0.16f;
            }
            return 0;
        }

        public override void AI()
        {
            NPC.rotation += NPC.velocity.X / 5;
            Player player = Main.player[NPC.target];
            if (NPC.velocity.X / 4 >= 1 || NPC.velocity.X / 4 <= -1)
            {
                Dust.NewDustPerfect(NPC.Center, DustID.CorruptionThorns, Scale: 0.5f, Alpha: 120);
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore3").Type, 1.3f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore3").Type, 1.3f);


            }

            for (int k = 0; k < 16; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptionThorns, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }



        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,


                new FlavorTextBestiaryInfoElement("A tumbling and hungry entity of the corruption, it rolls around, attempting to penetrate anything with its sharp teeth."),


            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfectedViscus>(), 4, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedBerries>(), 16));


        }
    }
}
