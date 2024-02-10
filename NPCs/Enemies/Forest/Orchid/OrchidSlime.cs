using RealmOne.Items.Food;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Weapons.PreHM.Throwing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using RealmOne.Items.Sets.OrchidSet;
using RealmOne.Common.Core;
using Terraria.GameContent;
namespace RealmOne.NPCs.Enemies.Forest.Orchid
{
    public class OrchidSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 2;


            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 18;
            NPC.damage = 10;
            NPC.defense = 1;
            NPC.lifeMax = 80;
            NPC.value = Item.buyPrice(silver: 3, copper: 80);
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneForest)
                return SpawnCondition.OverworldDaySlime.Chance * 0.15f;
            return base.SpawnChance(spawnInfo);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("A quite stunning and beautiful looking slime, it contains a small amount of orchid essence in its slimy texture that glows and provides healing"),
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<OrchidEssence>(), 2, 3, 5));
            npcLoot.Add(ItemDropRule.Common(ItemType<PoisonPrickles>(), 13, 4, 6));

            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 6));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 16; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BubbleBurst_Green, 2.7f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.BubbleBurst_Pink, 2.7f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);

            }
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("orchiD").Type, 1f);
            }
        }
        public override void AI()
        {
            NPC.spriteDirection -= NPC.direction;

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int PositionX = (int)(NPC.Center.X - screenPos.X);
            int PositionY = (int)(NPC.Center.Y - screenPos.Y);

            Texture2D ripple = Mod.Assets.Request<Texture2D>("Assets/Effects/lilglow").Value;

            Main.spriteBatch.Draw(ripple, new Vector2(PositionX, PositionY), new Microsoft.Xna.Framework.Rectangle?(), Color.Yellow, NPC.rotation, ripple.Size() / 2f, 1f, effects, 0);



            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, Request<Texture2D>(Texture + "_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);
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
