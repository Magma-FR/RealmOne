/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc;
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
    public class GlitterdustSlime : ModNPC
    {
        static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glitterdust Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];
            NPCID.Sets.TrailCacheLength[NPC.type] = 3;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            glowmask = ModContent.Request<Texture2D>(Texture + "_Tex");
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 20;
            NPC.damage = 12;
            NPC.lifeMax = 62;
            NPC.value = Item.buyPrice(0, 0, 7, 44);
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.MaxMana;
            NPC.DeathSound = SoundID.NPCDeath7;
            AIType = NPCID.IlluminantSlime;
            AnimationType = NPCID.GreenSlime;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.defense = 1;
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
            Lighting.AddLight(NPC.position, r: 0.55f, g: 0.6f, b: 0.1f);

            NPC.TargetClosest(true);
            Player Player = Main.player[NPC.target];
            dustTimer++;
            if (dustTimer >= 18)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(NPC.Center, DustID.YellowStarDust, speed * 10, Scale: 1f);
                    ;
                    d.noGravity = true;
                }

                dustTimer = 0;
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item9);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
         {
                 Color color = Helper.IndicatorColor;
                 spriteBatch.Draw(Request<Texture2D>("RealmOne/NPCs/Enemies/GlitterdustSlime_Tex").Value, NPC.position - screenPos + new Vector2(-3, -6), color);
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
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Sent from the stars, this slime bounces around with a star lodged in its asteroid flesh."),
            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EnchantedStarglitter>(), 1, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 1, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 4));
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