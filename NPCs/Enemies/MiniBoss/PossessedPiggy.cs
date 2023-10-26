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

namespace RealmOne.NPCs.Enemies.MiniBoss
{
    [AutoloadBossHead]
    public class PossessedPiggy : ModNPC
    {
        //Movement and Frames, attempting and damage
        bool fallingInAi = false;
        int move;
        bool fell = false;
        int falling;
        int fallingCounter;
        int attempting;
        int soundLoop;
        int damage;
        int dmg;

        int porcelaindropcd;

        //GroundPound
        Vector2 loc;
        int groundPound;
        bool PoundAttacking = false;

        //Overheat
        int overHeat;
        bool OverHeatSlide = false;
        int chargingUp = 0;
        int direction = 0;
        int fireCD = 0;
        int maximunTileHop = 0;

        //Coin Rain
        int coinRain;
        bool CoinsAreRaining = false;
        int coinCD;
        int time;
        int burstCD;

        public ref float RemainingShields => ref NPC.localAI[2];

        public int MinionMaxHealthTotal
        {
            get => (int)NPC.ai[1];
        }
        public int MinionHealthTotal { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Possessed Piggybank");
            Main.npcFrameCount[NPC.type] = 5;
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
            NPC.width = 66;
            NPC.height = 40;
            NPC.damage = 20;
            NPC.defense = 7;
            NPC.lifeMax = 700;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 2, 50, 50);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.Item59;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            

            NPC.noGravity = false;
            NPC.boss = true;
            AnimationType = -1;
            AIType = -1;
            NPC.BossBar = ModContent.GetInstance<PiggyBossBar>();
            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/PiggyPatrol");

            if (Main.masterMode == true)
            {
                dmg = 40;
            }
            else if (Main.expertMode == true)
            {
                dmg = 30;
            }
            else
            {
                dmg = 20;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
            NPCLoader.blockLoot.Add(ItemID.Heart);
        }

        public override void FindFrame(int frameHeight)
        {

            if (OverHeatSlide == true || time >= 0 && time <= 119 && CoinsAreRaining == true)
            {
                if (NPC.frameCounter == 2)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter == 5)
                {
                    NPC.frame.Y = 50;
                }
                else if (NPC.frameCounter == 7)
                {
                    NPC.frame.Y = 100;
                }
                else if (NPC.frameCounter == 10)
                {
                    NPC.frame.Y = 151;
                }
                else if (NPC.frameCounter == 12)
                {
                    NPC.frame.Y = 200;
                }
                else if (NPC.frameCounter >= 13)
                {
                    NPC.frameCounter = 0;
                }
            }
            else
            {
                if (NPC.frameCounter == 6)
                {
                    NPC.frame.Y = 0;
                }
                else if (NPC.frameCounter == 12)
                {
                    NPC.frame.Y = 50;
                }
                else if (NPC.frameCounter == 18)
                {
                    NPC.frame.Y = 100;
                }
                else if (NPC.frameCounter == 24)
                {
                    NPC.frame.Y = 151;
                }
                else if (NPC.frameCounter == 30)
                {
                    NPC.frame.Y = 200;
                }
                else if (NPC.frameCounter >= 31)
                {
                    NPC.frameCounter = 0;
                }
            }
        }

      

        public override void AI()
        {
            if (porcelaindropcd > 0)
            {
                porcelaindropcd--;
            }

            NPC.netAlways = true;
            NPC.netUpdate = true;
            if (Main.masterMode == true)
            {
                damage = Main.rand.Next(45, 53);
            }
            else
            {
                if (Main.expertMode == true)
                {
                    damage = Main.rand.Next(36, 43);
                }
                else
                {
                    damage = Main.rand.Next(45, 52);
                }
            }


            if (OverHeatSlide == false)
            {
                NPC.spriteDirection = NPC.direction;
            }

            NPC.TargetClosest(true);

            Player t = Main.player[NPC.target];

            if (t.dead)
            {
                NPC.velocity.Y -= 0.5f;
                NPC.EncourageDespawn(30);
            }

            if (!t.dead)
            {
                Vector2 d = (t.Center - NPC.Center).SafeNormalize(Vector2.UnitX);



                // timer countdowns
                if (burstCD > 0)
                {
                    burstCD--;
                }


                if (coinRain > 0)
                {
                    coinRain--;
                }

                if (coinCD > 0)
                {
                    coinCD--;
                }

                if (fireCD > 0)
                {
                    fireCD--;
                }

                if (soundLoop > 0)
                {
                    soundLoop--;
                }

                if (move > 0)
                {
                    move--;
                }

                if (overHeat > 0)
                {
                    overHeat--;
                }

                if (groundPound > 0)
                {
                    groundPound--;
                }

                if (attempting > 0)
                {
                    attempting--;
                }

                if (fallingCounter > 0)
                {
                    fallingCounter--;
                }

                if (fallingCounter == 0 && falling == 1)
                {
                    NPC.defense = 10;
                    NPC.damage = dmg;
                    falling = 0;
                    overHeat = 230;
                }
                // end

                // ground pound
                if (groundPound == 0 && OverHeatSlide == false && CoinsAreRaining == false && falling == 0)
                {
                    groundPound = 1200;
                    PoundAttacking = true;
                    attempting = 300;
                }
                if (attempting == 0 && PoundAttacking == true && falling == 0)
                {
                    PoundAttacking = false;
                    groundPound = 400;
                }
                if (PoundAttacking == true && attempting > 0)
                {
                    loc = new Vector2(t.Center.X, t.Center.Y - 235);
                    Vector2 direct = (loc - NPC.Center).SafeNormalize(Vector2.UnitX);
                    if (Collision.CanHit(direct, 16, 16, NPC.Center, NPC.width, NPC.height))
                    {
                        if (move == 0 && fell == false)
                        {
                            NPC.velocity = direct * 10;
                            move = 35;
                        }
                        if (move == 33 && fell == false)
                        {
                            NPC.velocity.Y -= 6;
                        }
                        if (Vector2.Distance(NPC.Center, loc) < 80)
                        {
                            NPC.velocity.X = 0;
                            NPC.velocity.Y = 0;
                            PoundAttacking = false;
                            groundPound = 500;
                            coinRain = 800;
                            falling = 1;
                            fallingCounter = 240;
                            NPC.defense = 20;
                            NPC.damage = dmg * 2;
                            NPC.netAlways = true;
                            NPC.netUpdate = true;
                        }
                    }
                    else
                    {
                        groundPound = 500;
                        attempting = 0;
                        PoundAttacking = false;
                    }



                }
                // end

                //overheat
                if (overHeat == 0 && OverHeatSlide == false && PoundAttacking == false && falling == 0 && CoinsAreRaining == false)
                {
                    overHeat = 1200;
                    groundPound = 400;
                    coinRain = 600;
                    attempting = 0;
                    falling = 0;
                    OverHeatSlide = true;
                }
                if (OverHeatSlide == true)
                {
                    PoundAttacking = false;
                    chargingUp++;

                    if (chargingUp >= 60 && chargingUp < 120)
                    {
                        int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch);
                        Main.dust[dust].scale = 3.2f;
                        Main.dust[dust].noGravity = true;
                        int dustd = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke);
                        Main.dust[dustd].scale = 1.6f;
                    }
                    if (chargingUp == 61)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFlyingCircleAttack, NPC.position);
                        for (int i = 0; i < 40; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.Torch, speed * 8, Scale: 2.8f);
                            dust1.noGravity = true;
                        }
                    }
                    if (chargingUp > 120)
                    {
                        int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch);
                        Main.dust[dust].scale = 4f;
                        Main.dust[dust].noGravity = true;
                        int dustd = Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Smoke);
                        Main.dust[dustd].scale = 2f;
                    }
                    if (chargingUp == 120)
                    {
                        t.GetModPlayer<Screenshake>().ScreenShake = 45;
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);
                        for (int i = 0; i < 60; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.Torch, speed * 8, Scale: 3.3f);
                            dust1.noGravity = true;
                        }
                    }
                    if (chargingUp < 61 && chargingUp != -1)
                    {
                        NPC.dontTakeDamage = true;
                        int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch);
                        Main.dust[dust].scale = 1.6f;
                        Main.dust[dust].noGravity = true;
                    }
                    if (chargingUp == 15)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, NPC.position);
                        for (int i = 0; i < 80; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.Torch, speed * 8, Scale: 2f);
                            dust1.noGravity = true;
                        }
                    }

                    if (chargingUp == 165)
                    {
                        t.GetModPlayer<Screenshake>().BigShake = 15;
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);
                        for (int i = 0; i < 60; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.Torch, speed * 8, Scale: 4f);
                            dust1.noGravity = true;
                        }
                        NPC.damage = dmg * 2;
                        NPC.netAlways = true;
                        NPC.netUpdate = true;
                        NPC.defense = 0;
                    }
                    if (chargingUp >= 165)
                    {
                        if (soundLoop == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item13, NPC.position);
                            soundLoop = 31;
                        }

                        if (fireCD == 0)
                        {
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y + 8), new Vector2(0, 0), ModContent.ProjectileType<Fire>(), damage / 5, 8f, Main.myPlayer);
                            fireCD = 6;
                        }
                        if (direction == 1)
                        {
                            NPC.velocity.X = 10f;
                        }
                        if (direction == -1)
                        {
                            NPC.velocity.X = -10f;
                        }
                        if (NPC.collideX == true)
                        {
                            NPC.position.Y -= 8;
                            maximunTileHop++;
                            if (maximunTileHop == 6)
                            {
                                NPC.damage = dmg;
                                NPC.defense = 10;
                                chargingUp = -1;
                                overHeat = 600;
                                coinRain = 300;
                                groundPound = 400;
                                NPC.netAlways = true;
                                NPC.netUpdate = true;
                                OverHeatSlide = false;
                                NPC.dontTakeDamage = false;
                            }
                        }
                        else
                        {
                            maximunTileHop = 0;
                        }
                    }
                    else
                    {
                        NPC.velocity.X = 0;
                        if (NPC.direction == 1)
                        {
                            direction = 1;
                        }
                        else
                        {
                            NPC.spriteDirection = NPC.direction;
                        }
                        if (NPC.direction == -1)
                        {
                            direction = -1;
                        }
                        else
                        {
                            NPC.spriteDirection = NPC.direction;
                        }

                    }
                    if (chargingUp > 295)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);
                        for (int i = 0; i < 40; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.Torch, speed * 8, Scale: 1.8f);
                            dust1.noGravity = true;
                        }
                        NPC.damage = dmg;
                        NPC.defense = 15;
                        NPC.netAlways = true;
                        NPC.netUpdate = true;
                        chargingUp = -1;
                        groundPound = 500;
                        overHeat = 700;
                        coinRain = 300;
                        OverHeatSlide = false;
                        NPC.dontTakeDamage = false;

                    }


                }

                //end

                //coin rain
                if (coinRain == 0 && PoundAttacking == false && OverHeatSlide == false && falling == 0)
                {
                    coinRain = 1200;
                    groundPound = 1200;
                    overHeat = 1200;
                    CoinsAreRaining = true;
                }
                if (CoinsAreRaining == true)
                {
                    if (time >= 0 && time <= 119)
                    {
                        NPC.dontTakeDamage = true;
                        NPC.velocity.Y = 0;
                        NPC.noGravity = true;
                        NPC.velocity.X = 0;
                        t.GetModPlayer<Screenshake>().ScreenShake = 2;
                    }
                    int select = Main.rand.Next(1, 5);
                    time++;

                    if (time == 120)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.PinkCrystalShard, speed * 8, Scale: 1.5f);
                            dust1.noGravity = true;
                        }
                        NPC.noGravity = false;
                        NPC.dontTakeDamage = false;
                        SoundEngine.PlaySound(SoundID.Item6, NPC.position);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X + 5, NPC.Center.Y), new Vector2(0, -5f), ModContent.ProjectileType<HugeGoldCoin>(), 0, 0f, Main.myPlayer);
                    }
                    if (time >= 220)
                    {
                        if (coinCD == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                            coinCD = 4;
                            Vector2 coinLoc = new Vector2(t.Center.X - 900, t.Center.Y - 1000);
                            if (select == 1)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(coinLoc.X + Main.rand.Next(10, 1800), coinLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<PlatinumCoin>(), damage / 3, 0f, Main.myPlayer);
                            }
                            if (select == 2)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(coinLoc.X + Main.rand.Next(10, 1800), coinLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<GoldCoin>(), damage / 3, 0f, Main.myPlayer);
                            }
                            if (select == 3)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(coinLoc.X + Main.rand.Next(10, 1800), coinLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<SilverCoin>(), damage / 3, 0f, Main.myPlayer);
                            }
                            if (select == 4)
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(coinLoc.X + Main.rand.Next(10, 1800), coinLoc.Y), new Vector2(0, 9f), ModContent.ProjectileType<CopperCoin>(), damage / 3, 0f, Main.myPlayer);
                            }
                        }
                    }

                    if (time > 800)
                    {
                        groundPound = 300;
                        overHeat = 500;
                        coinRain = 1000;
                        CoinsAreRaining = false;
                        time = 0;
                    }
                }
                //end

                //movement ai, hopping
                if (move <= 0 && CoinsAreRaining == true && time >= 120)
                {
                    if (t.Center.Y - 200 > NPC.Center.Y)
                    {
                        fallingInAi = true;
                        NPC.frameCounter = 16;
                    }
                    else
                    {
                        fallingInAi = false;
                        NPC.velocity = d * 3;
                    }
                    move = 35;
                }
                if (move == 33 && CoinsAreRaining == true && time >= 120)
                {
                    if (t.Center.Y - 200 > NPC.Center.Y)
                    {
                        fallingInAi = true;
                        NPC.frameCounter = 16;
                    }
                    else
                    {
                        fallingInAi = false;
                        NPC.velocity.Y -= 5;
                    }
                }

                if (move <= 0 && PoundAttacking == false && falling == 0 && OverHeatSlide == false && time! >= 0 && time! <= 119 && CoinsAreRaining == false)
                {
                    if (t.Center.Y - 200 > NPC.Center.Y)
                    {
                        fallingInAi = true;
                        NPC.frameCounter = 16;
                    }
                    else
                    {
                        fallingInAi = false;
                        NPC.velocity = d * 4;
                    }
                    move = 35;
                }
                if (move == 33 && PoundAttacking == false && falling == 0 && OverHeatSlide == false && time! >= 0 && time! <= 119 && CoinsAreRaining == false)
                {
                    if (t.Center.Y - 200 > NPC.Center.Y)
                    {
                        fallingInAi = true;
                        NPC.frameCounter = 16;
                    }
                    else
                    {
                        fallingInAi = false;
                        NPC.velocity.Y -= 6;
                    }
                }
                //end



                //falling system
                if (falling == 1)
                {
                    overHeat = 10;
                    NPC.frameCounter = 16;
                    if (soundLoop == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item13, NPC.position);
                        soundLoop = 35;
                    }
                    Vector2 vec = NPC.Center + Vector2.Normalize(NPC.velocity) * 10f;
                    Dust dusted = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch)];
                    dusted.position = vec;
                    dusted.velocity = NPC.velocity.RotatedBy(1.5707963705062866) * 0.33f + NPC.velocity / 4f;
                    dusted.position += NPC.velocity.RotatedBy(1.5707963705062866);
                    dusted.fadeIn = 0.5f;
                    dusted.noGravity = true;
                    Dust ddusted = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch)];
                    ddusted.position = vec;
                    ddusted.velocity = NPC.velocity.RotatedBy(-1.5707963705062866) * 0.33f + NPC.velocity / 4f;
                    ddusted.position += NPC.velocity.RotatedBy(-1.5707963705062866);
                    ddusted.fadeIn = 0.5f;
                    ddusted.noGravity = true;
                    for (int num210 = 0; num210 < 1; num210++)
                    {
                        int dust = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch);
                        Main.dust[dust].velocity *= 0.5f;
                        Main.dust[dust].scale *= 1.3f;
                        Main.dust[dust].fadeIn = 1f;
                        Main.dust[dust].noGravity = true;
                    }
                    NPC.velocity.X = 0;
                    if (NPC.collideY == true)
                    {
                        t.GetModPlayer<Screenshake>().ScreenShake = 20;
                        NPC.defense = 15;
                        NPC.damage = dmg;
                        falling = 0;
                        overHeat = 230;
                        SoundEngine.PlaySound(SoundID.Item89, NPC.position);
                        int dust = Dust.NewDust(NPC.Center, NPC.width * 2, NPC.height * 2, DustID.Torch);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y + 8), new Vector2(0, 0), ModContent.ProjectileType<Fire>(), damage / 5, 8f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-2, 0), ModContent.ProjectileType<Shockwave>(), damage / 4, 8f, Main.myPlayer);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(2, 0), ModContent.ProjectileType<Shockwave>(), damage / 4, 8f, Main.myPlayer);
                    }
                }
                if (falling == 0)
                {
                    if (fallingInAi == false)
                    {
                        NPC.frameCounter++;
                    }
                }
                // end
            }


        }

        public override bool? CanFallThroughPlatforms()
        {
            return true;
        }



        

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("An abandoned piggy bank owned by a child who needed money for his dying mother. Unfortunate circumstances led the child to die and his soul fused into the piggy bank. He becomes mad when you waste money."),


            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.rand.Next(100) < 45 && porcelaindropcd == 0)
            {
                porcelaindropcd = 60;
                Vector2 down = new Vector2(0, -1f).RotatedBy(MathHelper.ToRadians(-100));
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, down, ModContent.ProjectileType<Porcelain>(), NPC.damage / 2, 0f, Main.myPlayer);
            }
           
                if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
                {
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore1").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore2").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore3").Type, 1f);

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore4").Type, 1.5f);


                
            }
            for (int k = 0; k < 25; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DungeonPink, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PiggyPorcelain>(), 1, 4, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.PiggyBank, 20, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.MoneyTrough, 30, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 5, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.Bacon, 20));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Midas;
            int timeToAdd = 7 * 60;
            target.AddBuff(buffType, timeToAdd);

        }
        /*     public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
             {
                 CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(234, 129, 178, 180), "Ow that hurt!", false, false);

             }*/

        public override void OnKill()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue($"[i:{ItemID.GoldCoin}]The damned piggy bank has been shattered[i:{ItemID.GoldCoin}]"), 71, 229, 231);

            }
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedPiggy, -1);


        }
        public override void OnSpawn(IEntitySource source)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(NPC.Center, DustID.PinkCrystalShard, speed * 8, Scale: 1.5f);
                dust1.noGravity = true;
            }

            groundPound = 240;
            overHeat = 400;
            coinRain = 500;

            Player player = Main.player[NPC.target];

            SoundEngine.PlaySound(SoundID.Item59);

        }
    }
}

