using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Critters
{
    public class ImpactBunny : ModNPC
    {
       // static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("E-Bunny");
       //     glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Bunny];

            var value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 23;
            NPC.height = 40;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.value = 76f;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.HitSound = SoundID.NPCHit1;

            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Bunny;
            AnimationType = NPCID.Bunny;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNight.Chance * 0.16f;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.NPCGlowmask(spriteBatch, NPC, Mod.Assets.Request<Texture2D>(Texture + "_Glow").Value, screenPos);


        /* public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
         {
             Main.spriteBatch.Draw(
                 Mod.Assets.Request<Texture2D>("NPCs/Critters/ImpactBunny_Glow").Value,
                 NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY),
                 NPC.frame,
                 Color.White,
                 NPC.rotation,
                 NPC.frame.Size() / 2,
                 NPC.scale,
                 SpriteEffects.None,
                 0
             );

         }*/

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("These fluffy bunnies were unfortunately purged when a catastrophic and otherworldly raid started."),

            });
        }
        public override void HitEffect(NPC.HitInfo hit)
        {

            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);

                var d = Dust.NewDustPerfect(NPC.position, DustID.Electric, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0f, g: 0.3f, b: 1f);
        }
    }
}
