using Microsoft.Xna.Framework;
using RealmOne.Items.ItemCritter;
using RealmOne.Items.Others;
using RealmOne.Items.Placeables;
using RealmOne.Items.Placeables.FarmStuff;
using RealmOne.RealmPlayer;
using RealmOne.Tiles.Blocks;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Critters.Farm
{
    public class OldSnail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grandfather Snail");

            Main.npcCatchable[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 7;
        
            NPCID.Sets.CountsAsCritter[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 16;
            NPC.damage = 0;
            NPC.defense = 5;
            NPC.lifeMax = 5;
            NPC.value = 0f;
            NPC.knockBackResist = 0f;
            NPC.dontCountMe = true;
            NPC.value = Item.buyPrice(0, 0, 0, 95);
            NPC.catchItem = (short)ModContent.ItemType<TatteredBarrelItem>();
            NPC.catchItem = (short)ModContent.ItemType<OldSnailItem>();
            NPC.aiStyle = NPCAIStyleID.Snail;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.dontTakeDamageFromHostiles = true;

            AnimationType = NPCID.Snail;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };


        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            if (player.ZoneFarmy() && !spawnInfo.PlayerSafe && (player.ZoneOverworldHeight || player.ZoneSkyHeight) && !(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon || Main.eclipse) && SpawnCondition.GoblinArmy.Chance == 0)
            {
                int[] spawnTiles = { ModContent.TileType<FarmSoil>() };
                return spawnTiles.Contains(spawnInfo.SpawnTileType) ? 1.2f : 0f;
            }
            return 0f;
        }


        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                new FlavorTextBestiaryInfoElement("One of the oldest creatures to ever migrate to the Abandoned Farm is the Grandfather Snail. This ol man has been carrying his home for 80 years!!"),
            });
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, Color.White.ToVector3() * 0.1f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
          
                if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail2").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OldSnail4").Type, 1f);


                }

            for (int k = 0; k < 15; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Snail, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TatteredBarrelItem>(), 3, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TatteredWood>(), 1, 1, 4));

        }
    }
}
