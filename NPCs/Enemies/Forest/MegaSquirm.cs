using Microsoft.Xna.Framework;
using RealmOne.Bosses;
using RealmOne.Items.BossSummons;
using RealmOne.Items.Food.FarmFood;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Forest
{
    internal class MegaSquirmHead : WormHead
    {
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public int PositionIndex
        {
            get => (int)NPC.ai[1] - 1;
            set => NPC.ai[1] = value + 1;
        }

        public bool HasPosition => PositionIndex > -1;

        /*  public static new int BodyType1()
          {
              return ModContent.NPCType<SquirmoHead>();
          }*/
        public override int BodyType => ModContent.NPCType<MegaSquirmBody>();

        public override int TailType => ModContent.NPCType<MegaSquirmTail>();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Squirm");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DiggerHead];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                CustomTexturePath = "RealmOne/NPCs/Enemies/Forest/MegaSquirm1", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
                Position = new Vector2(40f, 28f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            // Head is 10 defence, body 20, tail 30.
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 12;
            NPC.value = Item.buyPrice(0, 0, 15, 0);
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.defense = 1;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDay.Chance * 0.09f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
              BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
              BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,

                new FlavorTextBestiaryInfoElement("Consuming and scavenging anything and anywhere it goes, serving for something much more disasterous"),


            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SquirmoGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SquirmoGore2").Type, 1f);

            }

            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Worm, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            npcLoot.Add(ItemDropRule.Common(ItemID.MudBlock, 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SquirmoSummon>(), 36, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Carrot>(), 10, 1, 2));



        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Slow;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 3 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
        public override void Init()
        {
            // Set the segment variance
            // If you want the segment length to be constant, set these two properties to the same value
            MinSegmentLength = 5;
            MaxSegmentLength = 10;

            CommonWormInit(this);
        }
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            worm.MoveSpeed = 6.2f;
            worm.Acceleration = 0.1f;
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

            /*   if (Despawn())
               {
                   return;
               }

               FadeIn();
            */

          
            
        }
        /*   private bool Despawn()
           {
               if (Main.netMode != NetmodeID.MultiplayerClient &&
                   (!HasPosition || !HasParent || !Main.npc[ParentIndex].active || Main.npc[ParentIndex].type != BodyType1()))
               {
                   // * Not spawned by the boss body (didn't assign a position and parent) or
                   // * Parent isn't active or
                   // * Parent isn't the body
                   // => invalid, kill itself without dropping any items
                   NPC.active = false;
                   NPC.life = 0;
                   NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
                   return true;
               }
               return false;
           }*/

        /*  private void FadeIn()
          {
              // Fade in (we have NPC.alpha = 255 in SetDefaults which means it spawns transparent)
              if (NPC.alpha > 0)
              {
                  NPC.alpha -= 10;
                  if (NPC.alpha < 0)
                  {
                      NPC.alpha = 0;
                  }
              }
          }*/

    }

    internal class MegaSquirmBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Squirm");

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 8;
            NPC.defense = 1;

            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SquirmoGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SquirmoGore1").Type, 1f);


            }
        }
        public override void Init()
        {
            MegaSquirmHead.CommonWormInit(this);
        }
    }

    internal class MegaSquirmTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mega Squirm");

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerTail);
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.damage = 6;
            NPC.defense = 1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubblePop");
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SquirmoGore1").Type, 1f);

            }
        }
        public override void Init()
        {
            MegaSquirmHead.CommonWormInit(this);
        }
    }
}

