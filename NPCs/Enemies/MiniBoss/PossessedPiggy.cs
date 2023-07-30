using Microsoft.Xna.Framework;
using RealmOne.BossBars;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.Piggy;
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

    public class PossessedPiggy : ModNPC
    {
        int move;

        bool fell = false;

        int falling;


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
            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 40;
            NPC.damage = 18;
            NPC.defense = 15;
            NPC.lifeMax = 225;
            NPC.knockBackResist = 0.6f;
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


        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
            NPCLoader.blockLoot.Add(ItemID.Heart);
        }

        public override void FindFrame(int frameHeight)
        {

            if (NPC.frameCounter == 5)
            {
                NPC.frame.Y = 0;
            }
            else if (NPC.frameCounter == 10)
            {
                NPC.frame.Y = 50;
            }
            else if (NPC.frameCounter == 15)
            {
                NPC.frame.Y = 100;
            }
            else if (NPC.frameCounter == 20)
            {
                NPC.frame.Y = 150;
            }
            else if (NPC.frameCounter == 25)
            {
                NPC.frame.Y = 200;
            }
            else if (NPC.frameCounter >= 26)
            {
                NPC.frameCounter = 0;
            }
        }


        public override void AI()
        {

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest(true);

            Player t = Main.player[NPC.target];

            Vector2 d = (t.Center - NPC.Center).SafeNormalize(Vector2.UnitX);

            if (t.dead)
            {
                NPC.velocity.Y -= 0.5f;
                NPC.EncourageDespawn(30);
            }



            if (move > 0 && !t.dead)
            {
                move--;
            }

            if (!t.dead)
            {

                if (move == 0 && fell == false && !t.dead)
                {
                    if (t.Center.Y - 200 > NPC.Center.Y)
                    {

                    }
                    else if (t.Center.Y + 200 < NPC.Center.Y)
                    {
                        NPC.velocity = d * 8;
                    }
                    else
                    {
                        NPC.velocity = d * 4;
                    }
                    move = 35;
                }
            }
            if (move == 33 && fell == false && !t.dead)
            {
                if (t.Center.Y - 200 > NPC.Center.Y)
                {
                    // falling
                }
                else if (t.Center.Y + 200 < NPC.Center.Y)
                {
                    NPC.velocity.Y -= 12;
                }
                else
                {
                    NPC.velocity.Y -= 6;
                }
            }

            if (fell == true && !t.dead)
            {
                NPC.velocity.X = 0;
                if (NPC.collideY == true)
                {
                    fell = false;
                    SoundEngine.PlaySound(SoundID.Item89, NPC.position);
                    int dust = Dust.NewDust(NPC.Center, NPC.width * 2, NPC.height * 2, DustID.Torch);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Vector2 dir = NPC.velocity.RotatedBy(MathHelper.ToRadians(-280));
                    Vector2 dir2 = NPC.velocity.RotatedBy(MathHelper.ToRadians(280));
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, dir, ModContent.ProjectileType<Shockwave>(), NPC.damage, 8f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, dir2, ModContent.ProjectileType<Shockwave>(), NPC.damage, 8f, Main.myPlayer);
                }
            }
            if (falling >= 90 || fell == true && !t.dead)
            {
                falling = 0;
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);
                fell = true;
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
            }
            if (t.Center.Y - 200 > NPC.Center.Y && !t.dead)
            {
                falling++;
            }
            else
            {
                NPC.frameCounter++;
                falling = 0;
            }
        }

        public override bool? CanFallThroughPlatforms()
        {
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.087f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

				new FlavorTextBestiaryInfoElement("An evil piggy bank that got a bit too empty of money through its lifetime. Even though it has wings, it cannot fly (lol)."),

				
            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.rand.Next(100) < 45)
            {
                Vector2 down = new Vector2(0, -1f).RotatedBy(MathHelper.ToRadians(-100));
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, down, ModContent.ProjectileType<Porcelain>(), NPC.damage / 2, 0f, Main.myPlayer);
            }

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("PiggyGore3").Type, 1f);

            }

            for (int i = 0; i < 26; i++)
            {

                Vector2 speed = Main.rand.NextVector2Square(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.DungeonPink, speed * 5, Scale: 1.5f);
                ;
                d.noGravity = true;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PiggyPorcelain>(), 1, 4, 5));
            npcLoot.Add(ItemDropRule.Common(ItemID.PiggyBank, 20, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.MoneyTrough, 30, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 1, 5, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.Bacon, 20));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Midas;
            int timeToAdd = 5 * 60;
            target.AddBuff(buffType, timeToAdd);

            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height), new Color(234, 129, 178, 110), "That's my money, fool!", false, false);

        }
        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(234, 129, 178, 180), "Ow that hurt!", false, false);

        }

        public override void OnKill()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue($"[i:{ItemID.GoldCoin}]The angry lil loot scavenger has been shattered!![i:{ItemID.GoldCoin}]"), 71, 229, 231);

            }
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedPiggy, -1);

            if (Main.netMode != NetmodeID.Server)
                CombatText.NewText(new Rectangle((int)NPC.position.X + 30, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(234, 129, 178, 190), "You got lucky that time!", false, false);
        }
        public override void OnSpawn(IEntitySource source)
        { 
            Player player = Main.player[NPC.target];

            SoundEngine.PlaySound(SoundID.Item59);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(234, 129, 178, 180), "Oink!! Oi buddy, over here!!", false, false);

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit)
        {
            CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y - 20, NPC.width, NPC.height), new Color(258, 27, 124, 200), "I got your little friends as well!", false, false);

        }
    }
}

