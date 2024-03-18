using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Throwing
{
    public class StackPotionsProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ele Jelly");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = 2;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.light = 1f;

            Projectile.tileCollide = true;
            Projectile.penetrate = 0;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 2;
            Projectile.CloneDefaults(ProjectileID.RottenEgg);
        }

        public override void AI()
        {
            if (Main.rand.NextBool(7))
            {
                SoundEngine.PlaySound(SoundID.Drip, Projectile.position);
            }

            Lighting.AddLight(Projectile.position, r: 0.8f, g: 0.8f, b: 0.8f);

            Lighting.Brightness(1, 1);

            Dust.NewDust(Projectile.Right + Projectile.velocity, Projectile.width, Projectile.height, DustID.GreenTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.8f);
            Dust.NewDust(Projectile.Left + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.8f);
            Dust.NewDust(Projectile.Center + Projectile.velocity, Projectile.width, Projectile.height, DustID.PinkTorch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.8f);
        }

        public override void OnKill(int timeleft)

        {
            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
            SoundEngine.PlaySound(SoundID.Drip, Projectile.position);
            SoundEngine.PlaySound(SoundID.Drip, Projectile.position);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.IronskinPotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.SwiftnessPotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.RegenerationPotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.EndurancePotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.ManaRegenerationPotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.MagicPowerPotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.SpelunkerPotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.ShinePotion, 1, false, 0, false, false);

            if (Main.rand.Next(0, 6) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                        ItemID.NightOwlPotion, 1, false, 0, false, false);

            int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GreenTorch, 0f, 0f, 100, default, 1f);

            Main.dust[dustIndex].noGravity = true;
            Main.dust[dustIndex].position = Projectile.Right + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex].noLight = false;

            int dustIndex1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.YellowTorch, 0f, 0f, 255, default, 3f);

            Main.dust[dustIndex1].noGravity = true;
            Main.dust[dustIndex1].position = Projectile.Left + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex1].noLight = false;

            int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PinkTorch, 0f, 0f, 255, default, 3f);

            Main.dust[dustIndex2].noGravity = true;
            Main.dust[dustIndex2].position = Projectile.Center + new Vector2(0f, (float)(-(float)Projectile.height / 2)).RotatedBy(Projectile.rotation, default) * 1.1f;
            Main.dust[dustIndex2].noLight = false;

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("PotionGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("PotionGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("PotionGore1").Type, 1f);

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Left, Vector2.Zero, Mod.Find<ModGore>("PotionGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("PotionGore3").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Right, Vector2.Zero, Mod.Find<ModGore>("PotionGore4").Type, 1f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = 20;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.damage = 0;
            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.Ironskin, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.Swiftness, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.Regeneration, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.Endurance, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.ManaRegeneration, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.MagicPower, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.Spelunker, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.Shine, 240);

            if (Main.rand.NextBool(5))
                target.AddBuff(BuffID.NightOwl, 240);
        }
    }
}