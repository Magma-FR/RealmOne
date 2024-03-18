using Microsoft.Xna.Framework;
using RealmOne.Items.Food;
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
    public class HaySlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hay Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 20;
            NPC.damage = 10;
            NPC.lifeMax = 60;
            NPC.value = Item.buyPrice(silver: 5);
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.JungleSlime;
            AnimationType = NPCID.GreenSlime;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 0.811f : 0f;
            }
            return 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 20)
                NPC.frameCounter = 0;
            NPC.frame.Y = (int)NPC.frameCounter / 10 * frameHeight;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
          new FlavorTextBestiaryInfoElement("What would you get if you had a slime that had to much playtime in haybales, well there we have it. This hay slime decreases in size the more it gets!")
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wheat>(), 1, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VegeToast>(), 18));

            npcLoot.Add(ItemDropRule.Common(ItemID.Hay, 2, 3, 4));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));
        }

        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, GoreID.TreeLeaf_Normal, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, GoreID.TreeLeaf_Jungle, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.Left, NPC.velocity, GoreID.TreeLeaf_Normal, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Right, NPC.velocity, GoreID.TreeLeaf_Jungle, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.Top, NPC.velocity, GoreID.TreeLeaf_Normal, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Top, NPC.velocity, GoreID.TreeLeaf_Jungle, 1f);
            }
            for (int k = 0; k < 30; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Hay, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
    }
}