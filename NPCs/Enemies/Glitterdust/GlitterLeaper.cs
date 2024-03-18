/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc;
using RealmOne.Projectiles.Magic;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Glitterdust
{
    public class GlitterLeaper : ModNPC
    {
        static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glitter Leaper");
            Main.npcFrameCount[NPC.type] = 5;

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            glowmask = ModContent.Request<Texture2D>(Texture + "_Tex");
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 16;
            NPC.damage = 10;
            NPC.lifeMax = 50;
            NPC.value = Item.buyPrice(0, 0, 3, 70);
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.HitSound = SoundID.MaxMana;
            NPC.DeathSound = SoundID.DD2_LightningBugDeath;
            NPC.netAlways = true;
            NPC.netUpdate = true;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.LightBlue) ?? Color.LightYellow;

            if (NPC.IsABestiaryIconDummy)
                color = Color.LightYellow;

            Main.EntitySpriteDraw(glowmask.Value, NPC.Center - screenPos + new Vector2(0, 0), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneOverworldHeight)
                return SpawnCondition.OverworldNightMonster.Chance * 0.20f;

            return base.SpawnChance(spawnInfo);
        }
        private int dustTimer;

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.4f, g: 0.5f, b: 0.1f);
            Player player = Main.player[NPC.target];

            NPC.TargetClosest(true);

            NPC.velocity *= 1.012f;
            Player target = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction;

            if (++NPC.ai[0] % 180 == 0)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(NPC.Center, DustID.YellowStarDust, speed * 6, Scale: 1f);
                    ;
                    d.noGravity = true;
                }
            }

            /*   dustTimer++;
               if (dustTimer >= 18)
               {
                   for (int i = 0; i < 60; i++)
                   {
                       Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                       var d = Dust.NewDustPerfect(NPC.Center, DustID.YellowStarDust, speed * 6, Scale: 1f);
                       ;
                       d.noGravity = true;
                   }

                   dustTimer = 0;
        }

         public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
         {
                 Color color = Helper.IndicatorColor;
                 spriteBatch.Draw(Request<Texture2D>("RealmOne/NPCs/Enemies/GlitterdustSlime_Tex").Value, NPC.position - screenPos + new Vector2(-3, -6), color);
         }
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.14f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Critter from a comet! This creepy crawly tends to leap around and then shoot a star out of its body."),
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnchantedStarglitter>(), 1, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 1, 1, 1));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 23; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.YellowStarDust, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }
        }
    }
}
*/