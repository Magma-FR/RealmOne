using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class ImpactMine : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 28;
            NPC.height = 38;
            NPC.defense = 1;
            NPC.damage = 16;
            NPC.lifeMax = 70;
            NPC.value = Item.buyPrice(0, 0, 1, 5);
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.knockBackResist = 0.50f;
            NPC.friendly = false;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath44;

            NPC.aiStyle = NPCAIStyleID.Unicorn;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

        public override void AI()
        {
            NPC.rotation += NPC.velocity.X / 2;
            Player player = Main.player[NPC.target];
            Vector2 center = NPC.Center;
            if (NPC.Distance(player.Center) < 55f)
            {
                Explode();
            }
            for (int j = 0; j < 60; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.Electric, 0f, 0f, 100, default, 0.5f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;
            }
        }

        private void Explode()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath44, NPC.position);

            NPC.active = false;
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 0.5f, 0f, 100, default, 2f);
            }

            Player player = Main.player[NPC.target];
            var p = Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), Vector2.Zero, ProjectileID.ClusterGrenadeI, 35, 2, Main.myPlayer);
            Main.projectile[p].timeLeft = 10;
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore1").Type, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore2").Type, 1f);

            Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore3").Type, 1f);

            Gore.NewGore(NPC.GetSource_Death(), NPC.Top, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore4").Type, 1f);
            Gore.NewGore(NPC.GetSource_Death(), NPC.Bottom, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore4").Type, 1f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore2").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore3").Type, 1f);

                Gore.NewGore(NPC.GetSource_Death(), NPC.Top, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Bottom, NPC.velocity, Mod.Find<ModGore>("ImpactMineGore4").Type, 1f);
            }

            for (int k = 0; k < 16; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("An explosive and speedy droid that tumbles on the ground and explodes into explosive scrap when near the player"),
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>(Texture + "_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 2, 1, 3));
        }
    }
}