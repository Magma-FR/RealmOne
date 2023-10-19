using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using static Terraria.ModLoader.ModContent;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.Items.Weapons.PreHM.BloodMoon;
using Terraria.GameContent.ItemDropRules;

namespace RealmOne.NPCs.Enemies.BloodMoon.ToothSerpent
{
    public class ToothSerpentHead: WormHead
    {

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.bloodMoon) 
            {
                return .15f;
            }
            else
            {
                return 0;
            }
        }
        public override int BodyType => NPCType<ToothSerpentBody>();

        public override int TailType => NPCType<ToothSerpentTail>();

        public override void AI()
        {
     
            if (++NPC.ai[2] % 160 == 0)
            {
                int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, ModContent.ProjectileType<BloodToothProj>(), 15, 0, Main.myPlayer, 0, 0);
                Main.projectile[p].scale = 1f;
                Main.projectile[p].friendly = false;
                Main.projectile[p].hostile = true;
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Right, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore2").Type, 1f);


            }
            for (int k = 0; k < 15; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.damage = 18;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.aiStyle = -1;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToothedTendril>(), 4, 1, 1));

        }

        public override void Init()
        {
            MinSegmentLength = 9;
            MaxSegmentLength = 9;
            MoveSpeed = 8f;
            Acceleration = 6;
            CanFly = true;

            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            worm.MoveSpeed = 8f;
            worm.Acceleration = 0.07f;
        }
        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

   
    }
    public class ToothSerpentBody : WormBody
    {
        public override void SetStaticDefaults()
        {

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
     
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Right, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Left, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore4").Type, 1f);


     



            }
            for (int k = 0; k < 15; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
        
        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
        }
        
        public override void Init()
        {
            ToothSerpentHead.CommonWormInit(this);


        }
    }
    public class ToothSerpentTail : WormTail
    {
        public override void SetStaticDefaults()
        {

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }
      
        public override void SetDefaults()
        {

            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
          


                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore5").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToothSerpentGore6").Type, 1f);




            }
            for (int k = 0; k < 15; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
        public override void Init()
        {
            ToothSerpentHead.CommonWormInit(this);


        }
    }
}

