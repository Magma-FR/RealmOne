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
        private int timer = 0;
        private bool circlingPlayer = false;

        public override int BodyType => NPCType<ToothSerpentBody>();

        public override int TailType => NPCType<ToothSerpentTail>();

        float offset;

        public override void AI()
        {
            NPC.ai[2]++;
            if (NPC.ai[2] == 300)
                offset = Main.rand.NextFloat(1.5f, 3);

        

            Player player = Main.player[NPC.target];
            if (player.dead)
            {
                NPC.velocity.Y -= 0.2f;
                NPC.EncourageDespawn(10);
                return;
            }
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                NPC.velocity = Vector2.Zero;
                return;
            }

            if (timer == 0)
            {
                circlingPlayer = true;
                NPC.velocity = Vector2.Zero;
            }
            if (timer >= 200) 
            {
                circlingPlayer = false;
                timer = -200;
            }

            if (timer < 0)
            {
                timer++;
                if (timer >= 0)
                    timer = 0;
                return;
            }

            if (circlingPlayer)
            {
                Vector2 pos = player.Center + new Vector2(300, 0).RotatedBy(MathHelper.ToRadians(NPC.ai[2] * offset));

                float distance = 120f;
                Vector2 targetPosition = player.Center + new Vector2(distance, 0f).RotatedBy(timer * 0.05f);
                NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;
                Vector2 direction = targetPosition - NPC.Center;
                direction.Normalize();
                NPC.velocity = direction * 7f;
                if (NPC.ai[2] % 10 == 0 && NPC.ai[2] > 340)
                {
                    int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, ModContent.ProjectileType<BloodToothProj>(), 15, 0, Main.myPlayer, 0, 0);
                    Main.projectile[p].scale = 1f;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].hostile = true;
                }
            }
        
            timer++;
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
            NPC.damage = 14;
            NPC.defense = 1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.aiStyle = -1;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToothedTendril>(), 40, 1, 1));

        }

        public override void Init()
        {
            MinSegmentLength = 8;
            MaxSegmentLength = 8;
            MoveSpeed = 6f;
            Acceleration = 0.5f;
            CanFly = true;

            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            worm.MoveSpeed = 6f;
            worm.Acceleration = 0.06f;
        }
        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
            writer.Write(offset);

        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
            offset = reader.ReadSingle();

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
            NPC.damage = 8;
            NPC.defense = 2;
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
            NPC.damage = 12;
            NPC.defense = 2;
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

