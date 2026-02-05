using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using RealmOne.Common.Events.CursedForest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using RealmOne.Common.Core;
using ReLogic.Content;
using Terraria.GameContent;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using RealmOne.RealmPlayer;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Items.Weapons.PreHM.Throwing;
using Terraria.GameContent.ItemDropRules;
using RealmOne.Items.Sets.ForestRevengeSet;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Weapons.PreHM.Classless;

namespace RealmOne.NPCs.Enemies.ForestRevenge
{
    public class CursedFirefly : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.TrailCacheLength[NPC.type] = 9;
            NPCID.Sets.TrailingMode[NPC.type] = 1;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 12;
            NPC.height = 9;
            NPC.noGravity = true;
            NPC.damage = 15;
            NPC.lifeMax = 24;
            AIType = NPCID.NebulaBrain;

            NPC.value = Item.buyPrice(0, 0, 0, 80);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.netUpdate = true;
            NPC.aiStyle = NPCAIStyleID.Flying;
            SpawnModBiomes = new int[]
            {
                GetInstance<CursedForestBiome>().Type
            };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !(CursedForestEvent.CursedForest && spawnInfo.Player.ZoneOverworldHeight && !Main.bloodMoon)
            ? 0 : 40f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireflyJar>(), 2, 5, 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SafeHook>(), 35));
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.14f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            NPC.rotation = 0;
            NPC.spriteDirection = NPC.direction;
            alphaCounter += .10f;

            Player player = Main.player[NPC.target];
            Vector2 center = NPC.Center;
            if (NPC.Distance(player.Center) < 60f)
            {
                Explode();
            }
        }

        private void Explode()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath44, NPC.position);

            NPC.active = false;
            for (int i = 0; i < 25; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Flesh, 0.5f, 0f, 100, default, 2f);
            }
            Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0, 4), ProjectileType<InvisibleExplosion>(), 10, 2, Main.myPlayer);

            for (int i = 0; i < 100; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(2.5f, 2.5f);
                var d = Dust.NewDustPerfect(NPC.Center, DustID.RedTorch, speed * 8, Scale: 3f);
                d.noGravity = true;
            }

            Player player = Main.player[NPC.target];
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;

            var p = Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0, 4), ProjectileID.MolotovFire, 10, 2, Main.myPlayer);
            Main.projectile[p].friendly = false;
            Main.projectile[p].hostile = true;
            var p1 = Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0, -4), ProjectileID.MolotovFire2, 10, 2, Main.myPlayer);
            Main.projectile[p1].friendly = false;
            Main.projectile[p1].hostile = true;
            var p2 = Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(4, 0), ProjectileID.MolotovFire3, 10, 2, Main.myPlayer);
            Main.projectile[p2].friendly = false;
            Main.projectile[p2].hostile = true;
            var p3 = Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-4, 0), ProjectileID.MolotovFire3, 10, 2, Main.myPlayer);
            Main.projectile[p3].friendly = false;
            Main.projectile[p3].hostile = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.7f);
            }
        }

        private float alphaCounter;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);

            float sineWave = MathHelper.Clamp(MathF.Sin(alphaCounter / 2f) + 1f, 0f, 1f);

            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            int PositionX = (int)(NPC.Center.X - screenPos.X);
            int PositionY = (int)(NPC.Center.Y - screenPos.Y);
            var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);

            Texture2D ripple = Mod.Assets.Request<Texture2D>("Assets/Effects/lighty").Value;

            Main.spriteBatch.Draw(ripple, new Vector2(PositionX, PositionY), null, new Color(255, 20, 10) * sineWave, NPC.rotation, ripple.Size() / 2f, 1f, effects, 0);

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);

            return false;
        }

        /*  public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
          {
              var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
              var pos = NPC.Center - screenPos + new Vector2(0, NPC.gfxOffY);
              spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, pos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0);
              return false;
          }*/

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color color) => GlowmMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Bleeding;

            int timeToAdd = 2 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }

    public class InvisibleExplosion : ModProjectile
    {
        public override string Texture => Helper.Empty;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 2;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}