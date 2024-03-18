using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.ItemCritter;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Critters
{
    public class GiantDragonfly : ModNPC
    {
        // static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Dragonfly");
            Main.npcFrameCount[NPC.type] = 4;
            Main.npcCatchable[NPC.type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            NPCID.Sets.CountsAsCritter[Type] = true;

            //  glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            NPC.catchItem = (short)ModContent.ItemType<GiantDragonflyItem>();
            NPC.width = 15;
            NPC.height = 8;
            NPC.dontCountMe = true;

            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.knockBackResist = 0.34f;
            NPC.dontTakeDamageFromHostiles = true;
            AnimationType = NPCID.BlueDragonfly;
            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Dragonfly;
            AIType = NPCID.BlueDragonfly;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantDragonflyGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GiantDragonflyGore2").Type, 1f);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldWaterSurfaceCritter.Chance * 0.3f;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.04f, g: 0.04f, b: 0.08f);
            ;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.White) ?? Color.White;

            if (NPC.IsABestiaryIconDummy)
                color = Color.White;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Larger from their normal counterpart of dragonfly, this dragonfly is a hunter of other larger prey, including ants and even water beetles!"),
            });
        }
    }
}