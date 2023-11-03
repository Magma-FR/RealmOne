using Microsoft.Xna.Framework;
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
    public class Rooster : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rooster");

            Main.npcFrameCount[NPC.type] = 9;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.CountsAsCritter[Type] = true;
            Main.npcCatchable[NPC.type] = true;


        }

        public override void SetDefaults()
        {
            NPC.catchItem = (short)ModContent.ItemType<RoosterItem>();

            NPC.width = 25;
            NPC.height = 25;
            NPC.defense = 0;
            NPC.lifeMax = 7;
            NPC.value = Item.buyPrice(0,0 , 4, 0);
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.HitSound = SoundID.NPCHit1;

            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Penguin;
            AnimationType = NPCID.Turtle;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };


        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 0.8f : 0f;
            }
            return 0f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                  

                new FlavorTextBestiaryInfoElement("The leader of all cocks, this brave and fiesty rooster will protect the fields, and also his hens."),

            });
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {


            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));





        }

        public override void HitEffect(NPC.HitInfo hit)
        {
       
                if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RoosterGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RoosterGore2").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RoosterGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RoosterGore4").Type, 1f);


                }


            for (int k = 0; k < 30; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }


    }
}
