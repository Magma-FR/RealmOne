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
    public class HoneyHare : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Honey Hare");

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.GoldBunny];
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
            NPC.catchItem = (short)ModContent.ItemType<HoneyHareItem>();

            NPC.width = 23;
            NPC.height = 20;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.value = Item.buyPrice(0, 0, 5, 0);
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.dontCountMe = true;
            NPC.npcSlots = 0;

            NPC.HitSound = SoundID.NPCHit1;

            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Bunny;
            AnimationType = NPCID.Bunny;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };



        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 1.3f : 0f;
            }
            return 0f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                  
                new FlavorTextBestiaryInfoElement("This bunny got a bit too curious hanging around in the farm, its got a bucket full of honey wedged onto its head, and a beehive on its back!!??"),

            });
        }
        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.HoneyBucket, 5, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.BottomlessHoneyBucket, 30, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));


        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            
                if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore2").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoneyHareGore4").Type, 1f);


                }


            for (int k = 0; k < 30; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Honey, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void OnKill()
        {
            Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ProjectileID.BeeHive, 10, 1);

        }
    }
}
