using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Other;
using RealmOne.BossBars;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.Piggy;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat
{
    public class ButcherRat : ModNPC
    {
        int dmg;
        int lilrat;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butcher Rat");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

        }

        public override void SetDefaults()
        {
            NPC.width = 140;
            NPC.height = 150;
            NPC.damage = 20;
            NPC.defense = 2;
            NPC.lifeMax = 950;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 2, 50, 50);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item59;
            NPC.netAlways = true;
            NPC.netUpdate = true;


            NPC.noGravity = false;
            NPC.boss = true;
            AnimationType = -1;
            AIType = -1;

            if (Main.masterMode == true)
            {
                dmg = 40;
            }
            else if (Main.expertMode == true)
            {
                dmg = 30;
            }
            else
            {
                dmg = 20;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.rand.Next(100) < 45 && lilrat == 0)
            {
                lilrat = 60;
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<JumboEyeMedium>(), ai3: 1).scale = 1f;
            }

            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore3").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore4").Type, 1f);




            }
            for (int k = 0; k < 25; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }
    }
}
