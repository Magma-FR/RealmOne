using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Emit;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.DataStructures;
using RealmOne.Common.Systems;
using RealmOne.Items.Weapons.PreHM.Piggy;
using RealmOne.RealmPlayer;
using MonoMod.Utils;
using RealmOne.Projectiles.Piggy;
using RealmOne.Buffs;

namespace RealmOne.Projectiles.HeldProj
{
    public class PiggyPursuerHeld : ModProjectile
    {
        int d = 0;
        int speed = 0;
        int acceleration = 0;
        int speedaddon = 0;
        int frame = 0;

        int livingTime = 53;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 63;
            Projectile.height = 62;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.knockBack = 4;
            Projectile.ownerHitCheck = true;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }

        public override bool? CanDamage()
        {
            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            //Main.NewText($"{Projectile.velocity.X}, {Projectile.velocity.Y}");
            if (player.GetModPlayer<RealmModPlayer>().PiggySwing == 0)
            {
                if (Main.LocalPlayer.direction == 1)
                {
                    Projectile.velocity = new Vector2(1, -29);
                }
                if (Main.LocalPlayer.direction == -1)
                {
                    Projectile.velocity = new Vector2(-3, -31);
                }
            }
            if (player.GetModPlayer<RealmModPlayer>().PiggySwing == 1)
            {
                if (Main.LocalPlayer.direction == 1)
                {
                    Projectile.velocity = new Vector2(6, 34);
                }
                if (Main.LocalPlayer.direction == -1)
                {
                    Projectile.velocity = new Vector2(-3, 34);
                }
            }


            frame = player.GetModPlayer<RealmModPlayer>().PorceDMG;
            if (player.GetModPlayer<RealmModPlayer>().DMGPor == 1)
            {
                Projectile.width = 58;
                Projectile.height = 56;
                speedaddon = 1;
                livingTime = 39;
            }
            else if (player.GetModPlayer<RealmModPlayer>().DMGPor == 2)
            {
                Projectile.width = 56;
                Projectile.height = 54;
                speedaddon = 2;
                livingTime = 33;
            }
            else if (player.GetModPlayer<RealmModPlayer>().DMGPor == 3)
            {
                Projectile.width = 46;
                Projectile.height = 46;
                speedaddon = 4;
                livingTime = 27;
            }
            else if (player.GetModPlayer<RealmModPlayer>().DMGPor == 4)
            {
                Projectile.width = 40;
                Projectile.height = 38;
                speedaddon = 6;
                livingTime = 20;
            }

            if (player.HasBuff(ModContent.BuffType<PursuerNo>()))
            {
                player.GetModPlayer<RealmModPlayer>().PigSwings++;

                if (player.GetModPlayer<RealmModPlayer>().PigSwings >= 3)
                {
                    player.GetModPlayer<RealmModPlayer>().PigSwings = 0;
                    Vector2 r = (player.Center - Main.MouseWorld).SafeNormalize(Vector2.UnitX);
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, r * -8f, ModContent.ProjectileType<StickyPorcelain>(), 0, 0f, Main.myPlayer);
                }
            }

            if (player.GetModPlayer<RealmModPlayer>().cd == 1)
            {
                player.GetModPlayer<RealmModPlayer>().cd = 0;
                player.GetModPlayer<RealmModPlayer>().PorceDMG = 0;
                player.GetModPlayer<RealmModPlayer>().DMGPor = 0;
                frame = 0;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.Zero);
            d = Projectile.velocity.X > 0 ? 1 : -1;
            Projectile.spriteDirection = d;

            player.heldProj = Projectile.whoAmI;
            player.direction = d;

            float rotation = MathHelper.ToRadians(d * speed);
            Projectile.Center = player.MountedCenter + Projectile.velocity.RotatedBy(rotation) * 40f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4 + rotation;
            if (Projectile.spriteDirection == -1)
                Projectile.rotation += MathHelper.PiOver2;
            float rotational = Projectile.rotation - MathHelper.PiOver4 - MathHelper.PiOver2;
            if (Projectile.spriteDirection == -1)
                rotational -= MathHelper.PiOver2;

            player.compositeFrontArm = new Player.CompositeArmData(true, Player.CompositeArmStretchAmount.Full, rotational);
            acceleration++;
            if (livingTime > 0)
            {
                livingTime--;
            }


            if (player.GetModPlayer<RealmModPlayer>().cd == 1)
            {
                player.GetModPlayer<RealmModPlayer>().cd = 0;
                player.GetModPlayer<RealmModPlayer>().PorceDMG = 0;
                player.GetModPlayer<RealmModPlayer>().DMGPor = 0;
                frame = 0;
            }


            if (livingTime == 0)
            {
                if (player.GetModPlayer<RealmModPlayer>().PiggySwing == 1)
                {
                    player.GetModPlayer<RealmModPlayer>().PiggySwing = 0;
                    player.GetModPlayer<RealmModPlayer>().PorceWidth = 58;
                }
                else
                {
                    player.GetModPlayer<RealmModPlayer>().PiggySwing = 1;
                    player.GetModPlayer<RealmModPlayer>().PorceWidth = 63;
                }
                Projectile.Kill();
            }

            
            if (player.GetModPlayer<RealmModPlayer>().PiggySwing == 0)
            {
                if (acceleration < 10)
                {
                    speed += 1 + speedaddon;
                }
                if (acceleration > 10 && acceleration < 15)
                {
                    speed += 4 + speedaddon;
                }
                if (acceleration > 15 && acceleration < 20)
                {
                    speed += 6 + speedaddon;
                }
                if (acceleration > 20 && acceleration < 25)
                {
                    speed += 8 + speedaddon;
                }
                if (acceleration > 25 && acceleration < 30)
                {
                    speed += 10 + speedaddon;
                }
                if (acceleration > 30 && acceleration < 35)
                {
                    speed += 14 + speedaddon;
                }
                if (acceleration > 35 && acceleration < 40)
                {
                    speed += 6 + speedaddon;
                }
                if (acceleration > 40 && acceleration < 45)
                {
                    speed += 4 + speedaddon;
                }
                if (acceleration > 45 && acceleration < 50)
                {
                    speed += 2 + speedaddon;
                }
                
            }
            else
            {
                if (acceleration < 10)
                {
                    speed -= 1 + speedaddon;
                }
                if (acceleration > 10 && acceleration < 15)
                {
                    speed -= 4 + speedaddon;
                }
                if (acceleration > 15 && acceleration < 20)
                {
                    speed -= 6 + speedaddon;
                }
                if (acceleration > 20 && acceleration < 25)
                {
                    speed -= 8 + speedaddon;
                }
                if (acceleration > 25 && acceleration < 30)
                {
                    speed -= 10 + speedaddon;
                }
                if (acceleration > 30 && acceleration < 35)
                {
                    speed -= 14 + speedaddon;
                }
                if (acceleration > 35 && acceleration < 40)
                {
                    speed -= 6 + speedaddon;
                }
                if (acceleration > 40 && acceleration < 45)
                {
                    speed -= 4 + speedaddon;
                }
                if (acceleration > 45 && acceleration < 50)
                {
                    speed -= 2 + speedaddon;
                }
                
            }



            bool stillInUse = !player.noItems && !player.CCed && player.HeldItem.ModItem is PiggyPursuer;
            if (!stillInUse)
                Projectile.Kill();

        }

       

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            Vector2 r = (Projectile.Center - target.Center).SafeNormalize(Vector2.UnitX);
            Vector2 rr = r.RotatedByRandom(MathHelper.ToRadians(360));
            Projectile.NewProjectile(player.GetSource_FromThis(), target.Center, new Vector2(0, 0), ModContent.ProjectileType<StickyPorcelain>(), 10, 0f, Main.myPlayer);
            if (player.GetModPlayer<RealmModPlayer>().DMGPor == 0)
            {
                target.immune[Projectile.owner] = 30;
            }
            else if (player.GetModPlayer<RealmModPlayer>().DMGPor == 1)
            {
                target.immune[Projectile.owner] = 23;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, rr, ModContent.ProjectileType<StickyPorcelain>(), 10, 0f, Main.myPlayer);
            }
            else if (player.GetModPlayer<RealmModPlayer>().DMGPor == 2)
            {
                target.immune[Projectile.owner] = 16;
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, rr, ModContent.ProjectileType<StickyPorcelain>(), 10, 0f, Main.myPlayer);
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, rr, ModContent.ProjectileType<StickyPorcelain>(), 10, 0f, Main.myPlayer);
            }
            else if (player.GetModPlayer<RealmModPlayer>().DMGPor == 3)
            {
                target.immune[Projectile.owner] = 10;
                SoundEngine.PlaySound(rorAudio.BulbShatter);
                player.AddBuff(ModContent.BuffType<PursuerNo>(), 720);
                player.GetModPlayer<RealmModPlayer>().cd = 680;
                target.immune[Projectile.owner] = 6;
                for (int i = 0; i < 80; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.PinkCrystalShard, speed * 12, Scale: 2f);
                    dust1.noGravity = true;
                }
                for (int i = 0; i < 7; i++)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, rr, ModContent.ProjectileType<StickyPorcelain>(), 10, 0f, Main.myPlayer);
                }
                if (Main.rand.NextBool(3))
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(3f, 2f), ModContent.ProjectileType<HoppingPiggy>(), 20, 0f, Main.myPlayer);
                }
                if (Main.rand.NextBool(3))
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(-3f, 2f), ModContent.ProjectileType<HoppingPiggy>(), 20, 0f, Main.myPlayer);
                }
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(2f, 3f), ModContent.ProjectileType<HoppingPiggy>(), 20, 0f, Main.myPlayer);
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(4f, 3f), ModContent.ProjectileType<HoppingPiggy>(), 20, 0f, Main.myPlayer);
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(-2f, 3f), ModContent.ProjectileType<HoppingPiggy>(), 20, 0f, Main.myPlayer);
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, new Vector2(-4f, 3f), ModContent.ProjectileType<HoppingPiggy>(), 20, 0f, Main.myPlayer);
                player.GetModPlayer<RealmModPlayer>().DMGPor = 4;
                frame = 248;
            }
            



            SoundEngine.PlaySound(rorAudio.SFX_Porce);
            SoundEngine.PlaySound(SoundID.Shatter);
            if (player.GetModPlayer<RealmModPlayer>().PorceDMG >= 248)
            {
                player.GetModPlayer<RealmModPlayer>().PorceDMG = 0;
                player.GetModPlayer<RealmModPlayer>().DMGPor = 0;
                frame = 0;
            }
            else if (!player.HasBuff(ModContent.BuffType<PursuerNo>()))
            {
                player.GetModPlayer<RealmModPlayer>().DMGPor++;
                player.GetModPlayer<RealmModPlayer>().PorceDMG += 62;
                frame = player.GetModPlayer<RealmModPlayer>().PorceDMG;
            }
            frame = player.GetModPlayer<RealmModPlayer>().PorceDMG;


        }


        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            


            var texture = (Texture2D)ModContent.Request<Texture2D>("RealmOne/Projectiles/HeldProj/PiggyPursuerHeld");
            if (player.GetModPlayer<RealmModPlayer>().PiggySwing == 1)
            {
                texture = (Texture2D)ModContent.Request<Texture2D>("RealmOne/Projectiles/HeldProj/PiggyPursuerFlip");
            }
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            var sourceRectangle = new Rectangle(0, frame, player.GetModPlayer<RealmModPlayer>().PorceWidth, 62);
            Vector2 origin = sourceRectangle.Size() / 2f;

            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            return false;
        }


    }
}