using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Weapons.Magic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon
{
    public class JumboEyeMedium : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            // DisplayName.SetDefault("Eye1");
            Main.npcFrameCount[NPC.type] = 7;

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Hide = true,
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 27;
            NPC.scale = 0.6f;
            NPC.damage = 10;
            NPC.defense = 0;
            NPC.lifeMax = 100;
            NPC.knockBackResist = 0.6f;

            NPC.value = Item.buyPrice(0, 0, 4, 55);
            NPC.aiStyle = NPCAIStyleID.DemonEye;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            AIType = NPCID.DemonEye;
            AnimationType = NPCID.DemonEye;
        }

        /*     public override void FindFrame(int frameHeight)
             {
                 NPC.frameCounter += 0.25f;
                 NPC.frameCounter %= Main.npcFrameCount[NPC.type];
                 int frame = (int)NPC.frameCounter;
                 NPC.frame.Y = frame * frameHeight;
             }
        */

        public override bool CheckDead()
        {
            for (int i = 0; i < Main.rand.Next(1, 1); i++)
            {
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, NPCID.DemonEye, ai3: 1).scale = 1f;
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, NPCID.DemonEye, ai3: 1).scale = 1f;
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, NPCID.DemonEye, ai3: 1).scale = 1f;
                NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, NPCID.DemonEye, ai3: 1).scale = 1f;
            }
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScathedFlesh>(), 7, 1, 2));

            npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 8, 1, 1));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore1").Type, 0.7f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore2").Type, 0.7f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore3").Type, 0.7f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Bottom, NPC.velocity, Mod.Find<ModGore>("JumboEyeGore4").Type, 0.7f);
            }
            for (int k = 0; k < 30; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void AI()
        {
        }
    }
}