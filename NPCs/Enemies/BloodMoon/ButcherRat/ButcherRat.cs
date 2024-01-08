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
using RealmOne.NPCs.Enemies.Corruption;

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
            NPC.width = 110;
            NPC.height = 100;
            NPC.damage = 30;
            NPC.defense = 2;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 2, 50, 50);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit19;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.netAlways = true;
            NPC.netUpdate = true;


            NPC.noGravity = false;
            NPC.boss = true;
            AnimationType = -1;
            AIType = -1;

            if (Main.masterMode == true)
            {
                dmg = 50;
            }
            else if (Main.expertMode == true)
            {
                dmg = 40;
            }
            else
            {
                dmg = 30;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Bleeding;
            int timeToAdd = 20 * 60;
            target.AddBuff(buffType, timeToAdd);

        }
        public override void OnKill()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue($"[i:{ItemID.BloodyMachete}]The dreaded frenzying rodent has been slaughtered, the curse has awoken the neverending plague of rats[i:{ItemID.ButchersChainsaw}]"), 249, 45, 99);

            }
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedRat, -1);


            var rat = NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<BloodRat>(), ai3: 1);


            rat.scale = 3f;
            rat.life = rat.lifeMax;


        }
        private int spawnTimer = 0;
        private bool spawnedEnemy = false;

        public override void HitEffect(NPC.HitInfo hit)
        {
          
      
         

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
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 3.2f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
    }
}
