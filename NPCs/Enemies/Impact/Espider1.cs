using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc.EnemyDrops;
using ReLogic.Content;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class Espider1 : ModNPC
    {
        static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("E-Spider");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.DesertGhoul];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");

        }

        public override void SetDefaults()
        {
            NPC.width = 70;
            NPC.height = 22;
            NPC.damage = 21;
            NPC.defense = 2;
            NPC.lifeMax = 80;
            NPC.value = Item.buyPrice(0, 0, 6, 15);
            NPC.aiStyle = 3;
            NPC.HitSound = SoundID.NPCHit29;
            NPC.DeathSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_ElectricDeath");

            AIType = NPCID.DesertGhoul;
            AnimationType = NPCID.DesertGhoul;

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.15f;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.LightBlue) ?? Color.LightBlue;

            if (NPC.IsABestiaryIconDummy)
                color = Color.LightBlue;

            Main.EntitySpriteDraw(glowmask.Value, NPC.Center - screenPos + new Vector2(0, 0), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Be extra careful when walking around at night with these plasma arachnids walking around."),

				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
            });
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.1f, g: 0.2f, b: 1.0f);
        }
        
        public override void HitEffect(NPC.HitInfo hit)
        {
          
                if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
                {
                    // These gores work by simply existing as a texture inside any folder which path contains "Gores/"

                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EspiderGore1").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EspiderGore2").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EspiderGore3").Type, 1f);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EspiderGore4").Type, 1f);

                }

            for (int k = 0; k < 18; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.WebSlinger, 35, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Cobweb, 1, 3, 6));

        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Electrified;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 1 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}