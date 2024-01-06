using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using RealmOne.Items.ItemCritter;
using Terraria.GameContent.Bestiary;
using RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

namespace RealmOne.NPCs.Enemies.BloodMoon
{
    internal class SanguineSlug : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 16;
            NPC.height = 12;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.dontCountMe = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.catchItem = (short)ModContent.ItemType<SanguineSlugItem>();
            NPC.knockBackResist = .35f;
            NPC.aiStyle = 66;
            NPC.npcSlots = 0;
            NPC.noGravity = false;
            AIType = NPCID.Grubby;
            NPC.dontTakeDamageFromHostiles = false;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

                new FlavorTextBestiaryInfoElement("While you fight all the gorey and bloody creatures of the night, this vampiric critter crawls around the surface sucking up excess carcass and blood. "),


            });
        }
        public override void FindFrame(int frameHeight)
        {
            if (NPC.velocity != Vector2.Zero || NPC.IsABestiaryIconDummy)
            {
                NPC.frameCounter += 0.13f;
                NPC.frameCounter %= Main.npcFrameCount[NPC.type];
                int frame = (int)NPC.frameCounter;
                NPC.frame.Y = frame * frameHeight;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 12; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 1.75f * hit.HitDirection, -1.75f, 0, new Color(), 0.6f);
            }
            NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<BloodRat>(), ai3: 1).scale = 1f;

        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) =>  Main.bloodMoon && spawnInfo.Player.ZoneOverworldHeight ? 0.17f : 0f;
        public override void AI() => NPC.spriteDirection = NPC.direction;


    }
}
