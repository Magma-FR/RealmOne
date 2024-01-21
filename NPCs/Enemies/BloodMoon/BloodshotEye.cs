using Microsoft.Xna.Framework;
using RealmOne.Items.Food;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.BloodMoon
{
    public class BloodshotEye : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 9;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 18;
            NPC.damage = 10;
            NPC.defense = 3;
            NPC.lifeMax = 45;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.HitSound = SoundID.NPCHit19;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.noGravity = true;
            NPC.aiStyle = NPCAIStyleID.DemonEye;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer && Main.bloodMoon ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

        private double Timer;
        private bool Attacking;

        public override void OnSpawn(IEntitySource source)
        {
            Timer = 0;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.rotation = NPC.velocity.ToRotation() + (float)Math.PI;

            if (Main.rand.NextBool(4))
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * -0.5f, NPC.velocity.Y * -0.5f);
            }

            var entitySource = NPC.GetSource_FromThis();
            if (Vector2.Distance(player.Center, NPC.Center) <= 250)
            {
                Timer++;
                if (Timer >= 50 && Timer <= 71)
                {
                    Attacking = true;
                }
                else
                {
                    Attacking = false;
                }
                if (Timer == 60)
                {
                    Lighting.AddLight(NPC.position, r: 1.5f, 0.2f, 0.6f);
                }
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;

                if (Timer == 80)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit19, NPC.position);
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + (6 * NPC.spriteDirection), NPC.Center.Y - 8, direction.X, direction.Y, ProjectileID.BloodShot, 9, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (Timer == 120)
                {
                    Timer = 0;
                }
            }
            else
            {
                Attacking = false;
                Timer = 0;
            }

            FindFrame(20);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EyeballSoup>(), 15));
        }

        public override void HitEffect(NPC.HitInfo hit) // when the npc is hit, do smth (better than checking when hit by proj or melee)
        {
            for (int k = 0; k < 18; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

                new FlavorTextBestiaryInfoElement("Small but deadly, the bloodshot eye lights up and blinks a powerful blood shot that is aimed at the player"),
            });
        }

        private int AnimFrameCount;
        private int AnimTimer;

        public override void FindFrame(int frameHeight)
        {
            if (!Attacking)
            {
                if (AnimFrameCount >= 4)
                {
                    AnimFrameCount = 1;
                }
                AnimTimer++;
                if (AnimTimer % 10 == 0)
                    AnimFrameCount++;
            }
            else
            {
                if (((int)Timer - 50) % 5 == 0)
                {
                    AnimFrameCount = 5 + (((int)Timer - 50) / 5);
                }
                if (Timer == 71)
                {
                    AnimFrameCount = 1;
                }
            }

            NPC.frame.Y = NPC.frame.Height * AnimFrameCount;
        }

        public class BloodshotEyeProjectile : ModProjectile
        {
            public override void SetDefaults()
            {
                Projectile.width = 4; // The width of projectile hitbox
                Projectile.height = 4; // The height of projectile hitbox
                Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
                Projectile.friendly = false; // Can the projectile deal damage to enemies?
                Projectile.hostile = true; // Can the projectile deal damage to the player?
                Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
                Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
                Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
                Projectile.damage = 10;
                Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
                Projectile.tileCollide = true; // Can the projectile collide with tiles?
                Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

                ; // Act exactly like default Bullet
            }

            public override void AI()
            {
                Projectile.velocity.Y += .1f;
                Projectile.rotation += (float)Math.PI / 12;
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                }
            }

            public override bool OnTileCollide(Vector2 oldVelocity)
            {
                for (int i = 0; i < 7; i++)
                {
                    Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1));
                }
                Projectile.Kill();
                return false;
            }

            public override void OnKill(int timeLeft) // the proj already dies when it hits the player so you can just make it do the dust then
            {
                for (int i = 0; i < 7; i++)
                {
                    Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Main.rand.Next(-1, 1), Main.rand.Next(-1, 1));
                }
            }
        }
    }
}