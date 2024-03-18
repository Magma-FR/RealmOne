/*
using Microsoft.Xna.Framework;
using RealmOne.BossBars;
using RealmOne.Common.Systems;
using RealmOne.Items.BossBags;
using RealmOne.Items.Placeables.Furniture.BossThing;
using RealmOne.Items.Vanities;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Bosses
{
    [AutoloadBossHead]
    internal class SquirmoHead : WormHead
    {
        public ref float RemainingShields => ref NPC.localAI[2];

        public int MinionMaxHealthTotal
        {
            get => (int)NPC.ai[1];
        }
        public int MinionHealthTotal { get; set; }

        public override int BodyType => ModContent.NPCType<SquirmoBody>();

        public override int TailType => ModContent.NPCType<SquirmoTail>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirmo");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DiggerHead];

            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "RealmOne/Assets/Textures/NewSquirmo",
                PortraitScale = 1f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = 0f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 2800;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 30;
            NPC.defense = 4;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
            NPC.BossBar = ModContent.GetInstance<SquirmoBar>();
            NPC.dontTakeDamageFromHostiles = true;
            NPC.scale = 1f;
            NPC.noTileCollide = true;
            NPC.SpawnWithHigherTime(30);
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.boss = true;
            NPC.npcSlots = 10f;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/InfestedSoil");
            }
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            NPC.lifeMax = (int)(3000 * bossAdjustment);
            NPC.damage = 40;
            NPC.defense = 5;

            if (Main.masterMode)
            {
                NPC.lifeMax = (int)(3300 * bossAdjustment);
                NPC.damage = 60;
                NPC.defense = 6;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                           BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("The dreaded controller and ruler of all creepy crawlies of the soil. Attacks anything that touches a grain of dirt, without any care of destruction"),
            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                /*int SquirmoGoreBody = Mod.Find<ModGore>("SquirmoGore1").Type;
               int SquirmoGoreHead = Mod.Find<ModGore>("SquirmoGore2").Type;
                IEntitySource entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreBody);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreHead);
                }

                SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, NPC.Center);
            }

            for (int i = 0; i < 25; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SquirmoBossBag>()));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<SquirmoRelicItem>()));

            var notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SquirmoTrophyItem>(), 10));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SquirmoMask>(), 7));
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedSquirmo, -1);

            if (Main.netMode != NetmodeID.Server)
            {
                Main.NewText(Language.GetTextValue("The soil has been adhered, the ground has been enchanted!"), 71, 229, 231);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
            return true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Slow;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
            int buffType1 = BuffID.OgreSpit;

            int timeToAdd = 4 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);

            int timeToAdd1 = 4 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType1, timeToAdd1);
        }
        public override void Init()
        {
            // Set the segment variance
            // If you want the segment length to be constant, set these two properties to the same value
            MinSegmentLength = 18;
            MaxSegmentLength = 18;

            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            worm.MoveSpeed = 16f;
            worm.Acceleration = 0.2f;
        }
        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.2f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }
            //     SpawnMinions();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (attackCounter > 0)
                    attackCounter--; // tick down the attack counter.

                Player target = Main.player[NPC.target];

                // If the attack counter is 0, this NPC is less than 12.5 tiles away from its target, and has a path to the target unobstructed by blocks, summon a projectile.
                if (attackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 450 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                {
                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(8));

                //    int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 2, ModContent.ProjectileType<GlobGunProjectileHostile>(), 16, 1, Main.myPlayer);
                   // Main.projectile[projectile].timeLeft = 60;
                   // attackCounter = 250;
                    NPC.netUpdate = true;
                   // Main.projectile[projectile].velocity *= 6f;
                }
            }
        }
    }
    internal class SquirmoBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirmo Body");

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 20;
            NPC.defense = 2;
            NPC.scale = 1f;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int SquirmoGoreBody = Mod.Find<ModGore>("SquirmoGore1").Type;
                int SquirmoGoreHead = Mod.Find<ModGore>("SquirmoGore2").Type;

                IEntitySource entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreBody);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreHead);
                }

                SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, NPC.Center);
            }

            for (int i = 0; i < 18; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }
        }
        public override void Init()
        {
            SquirmoHead.CommonWormInit(this);
        }
    }

    internal class SquirmoTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirmo Tail");

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 17;
            NPC.defense = 2;
            NPC.scale = 1f;

            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                int SquirmoGoreBody = Mod.Find<ModGore>("SquirmoGore1").Type;
                int SquirmoGoreHead = Mod.Find<ModGore>("SquirmoGore2").Type;
                IEntitySource entitySource = NPC.GetSource_Death();

                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreBody);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5)), SquirmoGoreHead);
                }

                SoundEngine.PlaySound(rorAudio.SquirmoMudBubblePop, NPC.Center);
            }

            for (int i = 0; i < 18; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }
        }
        public override void Init()
        {
            SquirmoHead.CommonWormInit(this);
        }
    }
}
*/