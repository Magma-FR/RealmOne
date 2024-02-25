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
            NPC.damage = 12;
            NPC.lifeMax = 56;
            AIType = NPCID.Bee;

            NPC.value = Item.buyPrice(0, 0, 0, 80);
            NPC.aiStyle = NPCAIStyleID.Flying;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
            NPC.netUpdate = true;

            AIType = NPCID.Bee;

            SpawnModBiomes = new int[]
            {
                ModContent.GetInstance<CursedForestBiome>().Type
            };
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !(CursedForestEvent.CursedForest && spawnInfo.Player.ZoneOverworldHeight && !Main.bloodMoon)
            ? 0 : 6f;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            NPC.spriteDirection = NPC.direction;
            alphaCounter += .05f;

            Player player = Main.player[NPC.target];
            Vector2 center = NPC.Center;
            if (NPC.Distance(player.Center) < 55f)
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
            Player player = Main.player[NPC.target];
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;

            Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0, 1), ProjectileID.GreekFire1, 15, 2, Main.myPlayer);
            Projectile.NewProjectile(NPC.GetSource_Death(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(1, 0), ProjectileID.GreekFire1, 15, 2, Main.myPlayer);
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

            Texture2D ripple = Mod.Assets.Request<Texture2D>("Assets/Effects/lilglow").Value;

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

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color color) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>(Texture + "_Glow", AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            int buffType = BuffID.Bleeding;

            int timeToAdd = 2 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }
    }
}