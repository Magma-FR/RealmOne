using Microsoft.Xna.Framework;
using RealmOne.Common.Events.CursedForest;
using RealmOne.Items.Food.FarmFood;
using RealmOne.Items.Misc;
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

namespace RealmOne.NPCs.Enemies.Forest
{
    public class ZombieFarmer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zombie Farmer");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 42;
            NPC.damage = 18;
            NPC.defense = 1;
            NPC.lifeMax = 65;
            NPC.value = Item.buyPrice(0, 0, 2, 35);
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && !CursedForestEvent.CursedForest && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 0.811f : 0f;
            }
            return 0f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.

				new FlavorTextBestiaryInfoElement("A withered and undead zombie, looking for its next meal to plant in its fields"),
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Hay, 1, 6, 10));
            npcLoot.Add(ItemDropRule.Common(ItemID.Sickle, 40, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Carrot>(), 3, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wheat>(), 2, 5, 8));
            // npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SquirmoSummon>(), 60, 1, 1));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ZombieFarmerGore4").Type, 1f);
            }

            for (int k = 0; k < 25; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.7f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
    }
}