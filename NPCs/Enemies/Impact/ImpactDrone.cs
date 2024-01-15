using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Items.Misc.EnemyDrops;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class ImpactDrone : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 0.8f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[Type] = 4;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("Programmed to zoom and cause doom, unfortunately this reckless and hyperactive drone has its CPU coded to be a complete trigger happy drone."),
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 20;
            NPC.damage = 10;
            NPC.defense = 2;
            NPC.lifeMax = 58;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = Item.buyPrice(0, 0, 2, 15);
            NPC.knockBackResist = 0.5f;

            NPC.noGravity = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

        private double Timer;
        private double SinThing;

        public override void OnSpawn(IEntitySource source)
        {
            Timer = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("RealmOne/NPCs/Enemies/Impact/ImpactDrone_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void AI()
        {
            NPC.TargetClosest();
            Lighting.AddLight(NPC.position, r: 0.1f, g: 0.2f, b: 1.1f);

            Vector2 center = NPC.Center;
            for (int j = 0; j < 70; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.Electric, 0f, 0f, 100, default, 0.6f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;
            }

            Timer++;
            if (Timer % 15 == 0)
                SinThing++;
            Player player = Main.player[NPC.target];
            Vector2 TargetLocation = new Vector2(player.position.X + ((float)Math.Sin(SinThing) * 100), player.position.Y - 150 - ((float)Math.Sin(SinThing) * 10));

            float speed = 13f;
            float inertia = 30f;
            Vector2 direction = TargetLocation - NPC.Center;
            direction.Normalize();
            direction *= speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;

            NPC.rotation = NPC.velocity.X * 0.3f;
            var entitySource = NPC.GetSource_FromThis();
            if (Vector2.Distance(TargetLocation, NPC.Center) <= 130 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Timer % 160 == 0)
                {
                    NPC.velocity.X = .008f * NPC.direction;

                    var p = Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(0f, 2f), ProjectileID.PulseBolt, 8, 0f);
                    Main.projectile[p].scale = 0.6f;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].hostile = true;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactDroneGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.TopLeft, NPC.velocity, Mod.Find<ModGore>("ImpactDroneGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.TopRight, NPC.velocity, Mod.Find<ModGore>("ImpactDroneGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.Top, NPC.velocity, Mod.Find<ModGore>("ImpactDroneGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactDroneGore4").Type, 1f);
            }
            for (int k = 0; k < 14; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 2, 2, 3));
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.14f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
    }
}