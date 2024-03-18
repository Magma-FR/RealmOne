using Microsoft.Xna.Framework;
using RealmOne.Items.Food;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Weapons.PreHM.Throwing;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Forest
{
    public class BarrenBarkSlime2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barrensludge Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 20;
            NPC.damage = 10;
            NPC.defense = 1;
            NPC.lifeMax = 80;
            NPC.value = Item.buyPrice(silver: 6);
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.JungleSlime;
            AnimationType = NPCID.GreenSlime;
            NPC.netAlways = true;
            NPC.netUpdate = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneForest)
                return SpawnCondition.OverworldDaySlime.Chance * 0.2f;
            return base.SpawnChance(spawnInfo);
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
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("A larger and more poisonous variant of its smaller size. Soaks up more damage with its large size"),
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GoopyGrass>(), 1, 3, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TreeSapJuice>(), 18));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PoisonPrickles>(), 13, 4, 6));

            npcLoot.Add(ItemDropRule.Common(ItemID.MudBlock, 2, 3, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 8));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 20; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BubbleBurst_Green, 2.7f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Poisoned;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 6 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}