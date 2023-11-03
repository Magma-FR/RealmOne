using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Food;
using RealmOne.Items.Placeables.BannerItems;
using RealmOne.NPCs.Enemies.Corruption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.BloodMoon
{
    public class JumboEye: ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            // DisplayName.SetDefault("Eye1");
            Main.npcFrameCount[NPC.type] = 7;


        }

        public override void SetDefaults()
        {
            NPC.width = 90;
            NPC.height = 56;
            NPC.damage = 13;
            NPC.defense = 1;
            NPC.lifeMax = 200;
            NPC.npcSlots = 2;
            NPC.knockBackResist = 0.3f;


            NPC.value = Item.buyPrice(0, 0, 4, 55);
            NPC.aiStyle = NPCAIStyleID.DemonEye;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            AIType = NPCID.DemonEye;
            AnimationType = NPCID.DemonEye;
          

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer && Main.bloodMoon  ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EyeballSoup>(), 15));

        }

        public override bool CheckDead()
        {
            for (int i = 0; i < Main.rand.Next(1, 1); i++)
            {
             NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Left, ModContent.NPCType<JumboEyeMedium>(), ai3: 1).scale = 0.65f;
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Right, ModContent.NPCType<JumboEyeMedium>(), ai3: 1).scale = 0.65f;

            }
            return true;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Bottom, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore4").Type, 1f);


            }
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
        public override void AI()
        {
            NPC.rotation = NPC.velocity.ToRotation();
    
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

				new FlavorTextBestiaryInfoElement("Mistaken by the seer of the land, this oversized eyeball is full of other eyeballs!"),

			
            });
        }
    }
}
