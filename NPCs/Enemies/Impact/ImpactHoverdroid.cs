using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Weapons.PreHM.Throwing;
using Terraria.GameContent.ItemDropRules;
using RealmOne.Projectiles.Bullet;
using RealmOne.Items.Misc.EnemyDrops;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using Terraria.GameContent.Bestiary;
using RealmOne.Common.Systems;
using Terraria.GameContent;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class ImpactHoverdroid : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 0.8f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[Type] = 3;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("A hyperactive and trigger happy hoverdroid, shoots a bolt of electricity that stuns the player"),


            });
        }
        public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 30;
			NPC.damage = 10;
			NPC.defense = 2;
			NPC.lifeMax = 58;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath44;
			NPC.value = Item.buyPrice(0,0,2,20);
			NPC.knockBackResist = 0.5f; 
			
			NPC.noGravity = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("RealmOne/NPCs/Enemies/Impact/ImpactHoverdroid_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);

        private ref float MoveSpeed => ref NPC.ai[1];
        private ref float MoveSpeedY => ref NPC.ai[2];
        private ref float Counter => ref NPC.ai[3];
        private int timeSinceLastFire = 0;
        private bool isFiring = false;
        private float fireRate = 0.8f; // Adjust this value for the firing rate

        public override void AI()
        {

            Lighting.AddLight(NPC.position, r: 0f, g: 0.1f, b: 1.1f);

            if (Counter == 0)
                NPC.ai[0] = 160;

            Counter++;
            Player player = Main.player[NPC.target];
            NPC.rotation = NPC.velocity.X * 0.2f;
            NPC.spriteDirection = NPC.direction;

            if (NPC.Center.X 
                >= player.Center.X && MoveSpeed >= -90) 
                MoveSpeed--;


            if (NPC.Center.Y 
                <= player.Center.Y - NPC.ai[0] && MoveSpeedY <= 50)
                MoveSpeedY++;

            if (NPC.Center.X
                <= player.Center.X && MoveSpeed <= 90)
                MoveSpeed++;

            NPC.velocity.X = MoveSpeed * 0.1f;

            if (NPC.Center.Y
                >= player.Center.Y - NPC.ai[0] && MoveSpeedY >= -55) 
            {
                MoveSpeedY--;
                NPC.ai[0] = 160f;
            }


            NPC.velocity.Y = MoveSpeedY * 0.12f;
            if (Main.rand.NextBool(220) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[0] = -25f;
                NPC.netUpdate = true;
            }




            Vector2 targetDirection = player.Center - NPC.Bottom;
            targetDirection.Normalize();
            // Check if not currently firing, and if enough time has passed to fire again
            if (!isFiring && timeSinceLastFire > 30f * fireRate) // Assuming 60 frames per second
            {
                // Start firing
                isFiring = true;
                SoundEngine.PlaySound(SoundID.Item157, NPC.position);
                // Calculate bullet velocity based on the direction towards the player
                Vector2 bulletVelocity = targetDirection * 18f;

                for (int i = 0; i < 12; i++) // Change 10 to the number of bullets you want to fire
                {
                    // Spawn bullets
                  var p=   Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Bottom.X + (10 * NPC.spriteDirection), NPC.Bottom.Y - 8, targetDirection.X, targetDirection.Y, ProjectileID.PulseBolt, 9, 1, Main.myPlayer, 0, 0);
                    Main.projectile[p].timeLeft = 400;
                    Main.projectile[p].scale = 0.6f;
                    Main.projectile[p].friendly = false;
                    Main.projectile[p].hostile = true;
                    Main.projectile[p].velocity *= 1.5f;
                }
            }

            // Check if currently firing and if 2 seconds have passed
            if (isFiring && timeSinceLastFire > 80) // Assuming 60 frames per second
            {
                // Stop firing after 2 seconds
                isFiring = false;
                timeSinceLastFire = 0;
            }

            // Update time since last fire
            timeSinceLastFire++;
            FindFrame(44);

            //WIP AI CODE
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoverdroidGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.TopLeft, NPC.velocity, Mod.Find<ModGore>("HoverdroidGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.TopRight, NPC.velocity, Mod.Find<ModGore>("HoverdroidGore3").Type, 1f);



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

        private int AnimFrameCount;
        private int AnimTimer;
        public override void FindFrame(int frameHeight)
        {
            AnimTimer++;

            if (AnimTimer % 10 == 0)
                AnimFrameCount++;
            if (AnimFrameCount == 3)
            {
                AnimFrameCount = 1;
            }
            NPC.frame.Y = NPC.frame.Height * AnimFrameCount;
        }
    }


}
