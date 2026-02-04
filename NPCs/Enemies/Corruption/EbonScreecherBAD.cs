using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Food;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Weapons.PreHM.Corruption;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.Corruption
{
    public class EbonScreecherBAD : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;

            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 28;
            NPC.height = 38;
            NPC.defense = 7;
            NPC.damage = 20;
            NPC.lifeMax = 150;
            NPC.aiStyle = 44;
            NPC.damage = 20;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            AIType = NPCID.GiantFlyingAntlion;
            NPC.value = Item.buyPrice(0, 2, 50, 5);
            NPC.knockBackResist = 0.50f;

            NPC.HitSound = SoundID.ChesterOpen;
            NPC.DeathSound = SoundID.NPCDeath38;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.19f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, r: 0.1f, g: 0.8f, b: 0.1f);
            NPC.velocity *= 1.0075f;
            Player target = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction;

            Vector2 center = NPC.Center;
            for (int j = 0; j < 120; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.CursedTorch, 0f, 0f, 100, default, 0.7f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;
            }

            if (++NPC.ai[2] % 130 == 0)
            {
                int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity, ProjectileID.CursedFlameFriendly, 15, 0, Main.myPlayer, 0, 0);
                Main.projectile[p].scale = 0.5f;
                Main.projectile[p].friendly = false;
                Main.projectile[p].hostile = true;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("A silent hunter, the Ebon Screecher is a mysterious entity that hunts enemies that go anywhere near. It phases into an aggressive phase when it sheds its vile skin."),
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Vector2 drawOrigin = NPC.frame.Size() / 2;
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore1").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore3").Type, 1.3f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("VulgarGore3").Type, 1.3f);
            }

            for (int k = 0; k < 16; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.CorruptionThorns, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //       npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfectedViscus>(), 3, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VulgarShot>(), 15, 1, 1));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedBerries>(), 15, 1, 2));
        }
    }
}