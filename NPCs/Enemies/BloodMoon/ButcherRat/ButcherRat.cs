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
using Terraria.Chat;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat
{
    public class ButcherRat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butcher Rat");
            
            Main.npcFrameCount[NPC.type] = 1;
            
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f 
            };
            
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.noGravity = false;
            NPC.boss = true;
            
            NPC.width = 110;
            NPC.height = 100;
            
            NPC.damage = 30;
            NPC.defense = 2;
            NPC.lifeMax = 1000;
            NPC.knockBackResist = 0f;
            
            NPC.value = Item.buyPrice(0, 2, 50, 50);
            
            NPC.aiStyle = -1;
            AIType = -1;
            
            NPC.HitSound = SoundID.NPCHit19;
            NPC.DeathSound = SoundID.NPCDeath2;
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Bleeding;
            int timeToAdd = 20 * 60;
            
            target.AddBuff(buffType, timeToAdd);
        }
        
        public override void OnKill()
        {
            // TODO: Change the text from literal strings to localized strings.
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"[i:{ItemID.BloodyMachete}]The dreaded frenzying rodent has been slaughtered, the curse has awoken the neverending plague of rats[i:{ItemID.ButchersChainsaw}]"), new Color(249, 45, 99));
            
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedRat, -1);

            var rat = NPC.NewNPCDirect(NPC.GetSource_Death(), NPC.Center, ModContent.NPCType<BloodRat>(), ai3: 1);

            rat.scale = 3f;
            rat.life = rat.lifeMax;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 25; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 3.2f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
            
            if (NPC.life > 0) {
                return;
            }
            
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore1").Type, 1f);

            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore2").Type, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore3").Type, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore3").Type, 1f);

            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore4").Type, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("RatGore4").Type, 1f);
        }
    }
}
