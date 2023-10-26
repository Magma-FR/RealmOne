using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Placeables.BannerItems;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class ImpactTurret : ModNPC
    {

        static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact Turret");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[2];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 34;
            NPC.damage = 0;
            NPC.lifeMax = 27;
            NPC.aiStyle = NPCAIStyleID.TeslaTurret;
            NPC.value = Item.buyPrice(0, 0, 2, 0);
            AIType = NPCID.MartianTurret;
            NPC.HitSound = SoundID.NPCHit53;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_ElectricDeath");
            AnimationType = NPCID.MartianTurret;
            Banner = Type;
            BannerItem = ModContent.ItemType<BannerItem.ImpactTurretB>();

        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.LightBlue) ?? Color.LightBlue;

            if (NPC.IsABestiaryIconDummy)
                color = Color.LightBlue;

            Main.EntitySpriteDraw(glowmask.Value, NPC.Center - screenPos + new Vector2(0, 0), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
        }
        // public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneForest && spawnInfo.Player.ZoneRockLayerHeight && spawnInfo.Player.ZoneNormalUnderground && spawnInfo.Water ? 1f : 0f;

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneForest ? 0.031f : 0f;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("An unknown device that teleports around the world. Almost scanning as an envoy for something much more larger in hiarachy.Be careful it can deal a lot of damage!"),

            });
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 1, 2, 3));

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TurretGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("TurretGore2").Type, 1f);

            }

            for (int k = 0; k < 30; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.1f, g: 0.2f, b: 1f);
        }
    }
}
