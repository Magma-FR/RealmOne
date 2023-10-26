using Microsoft.Xna.Framework;
using RealmOne.Items.Food.FarmFood;
using RealmOne.Items.ItemCritter;
using RealmOne.Items.Others;
using RealmOne.RealmPlayer;
using RealmOne.Tiles.Blocks;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Critters.Farm
{
    public class Berryback : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berryback");

                    Main.npcFrameCount[NPC.type] = 9;

          
            NPCID.Sets.CountsAsCritter[Type] = true;
            Main.npcCatchable[NPC.type] = true;


        }

        public override void SetDefaults()
        {
            NPC.catchItem = (short)ModContent.ItemType<BerrybackItem>();

            NPC.width = 23;
            NPC.height = 20;
            NPC.defense = 5;
            NPC.lifeMax = 5;
            NPC.value = Item.buyPrice(0, 0,6, 0);
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.dontCountMe = true;
            NPC.npcSlots = 0;


            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Turtle;
            AnimationType = NPCID.Turtle;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };


        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 1f : 0f;
            }
            return 0f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
               

                new FlavorTextBestiaryInfoElement("The mix of bacteria on its shell makes a ever-growing field of bright, colourful flowers to grow on it!!"),

            });
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FloralDelight>(), 6, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Blinkroot, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Waterleaf, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Daybloom, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Moonglow, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Shiverthorn, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Fireblossom, 9, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));







        }

        public override void HitEffect(NPC.HitInfo hit)
        {

            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BerryBackGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BerryBackGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BerryBackGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("BerryBackGore4").Type, 1f);


            }
            for (int k = 0; k < 14; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Grass, 2.5f * hit.HitDirection, -2.5f, Alpha: 25);
            }
        }


    }
}
