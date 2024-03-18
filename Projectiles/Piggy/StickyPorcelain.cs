using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Piggy
{
    public class StickyPorcelain : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Porcelain");
            Main.projFrames[Projectile.type] = 6;
        }

        private bool stick = false;
        private int targetPorce;
        private float rotation;

        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 2;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            Main.EntitySpriteDraw(texture, Projectile.position, texture.Frame(1, 6), Color.White, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return true;
        }

        public override void AI()
        {
            if (stick)
            {
                Projectile.rotation = rotation;
                int npcTarget = targetPorce;
                Projectile.Center = Main.npc[npcTarget].Center - Projectile.velocity;
                Projectile.gfxOffY = Main.npc[npcTarget].gfxOffY;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (stick == false)
            {
                stick = true;
                rotation = Projectile.rotation;
                targetPorce = target.whoAmI;
                target.AddBuff(ModContent.BuffType<PorcelainOvertime>(), Projectile.timeLeft);
                Projectile.damage = 0;
                Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
                Projectile.netUpdate = true;
                Projectile.tileCollide = false;
                if (!Main.npc[targetPorce].active)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (stick)
            {
                int npcIndex = targetPorce;
                if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
                {
                    if (Main.npc[npcIndex].behindTiles)
                    {
                        behindNPCsAndTiles.Add(index);
                    }
                    else
                    {
                        behindNPCsAndTiles.Add(index);
                    }

                    return;
                }
            }
            behindNPCsAndTiles.Add(index);
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.rand.Next(100) < 50)
            {
                Projectile.frame = 0;
            }
            else if (Main.rand.Next(100) < 50)
            {
                Projectile.frame = 1;
            }
            else if (Main.rand.Next(100) < 50)
            {
                Projectile.frame = 2;
            }
            else if (Main.rand.Next(100) < 50)
            {
                Projectile.frame = 3;
            }
            else if (Main.rand.Next(100) < 50)
            {
                Projectile.frame = 4;
            }
            else if (Main.rand.Next(100) < 50)
            {
                Projectile.frame = 5;
            }
            else
            {
                Projectile.frame = 0;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true;
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 9; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.PinkCrystalShard, speed * 8, Scale: 1f);
                dust1.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }
    }
}