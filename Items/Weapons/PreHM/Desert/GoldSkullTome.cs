using Microsoft.Xna.Framework;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Projectiles.Magic;
using RealmOne.RealmPlayer;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Desert
{
    public class GoldSkullTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemGlowy.AddGlowMask(Item.type, Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTurn = true;
            Item.damage = 11;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.mana = 6;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.shoot = ModContent.ProjectileType<DesertHands>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<DesertHands>();
            float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi * MathHelper.PiOver4);

            Vector2 spawnPlace = (type == ModContent.ProjectileType<DesertHands>()) ? Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 65f : Vector2.Zero;
            if (Collision.CanHit(position, 0, 0, position + spawnPlace, 0, 0))
                position += spawnPlace;

            velocity = Vector2.Normalize(Main.MouseWorld - position) * Item.shootSpeed;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            for (float num2 = 0.0f; (double)num2 < 16; ++num2)
            {
                int dustIndex = Dust.NewDust(position, 3, 3, DustID.Sandnado, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity = Vector2.Normalize(spawnPlace.RotatedBy(Main.rand.NextFloat(MathHelper.TwoPi))) * 1.6f;
            }
            for (int i = 0; i < 5; i++)
            {
                GenericGlowParticle particle = new(new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), Color.Yellow, 0.5f, 120);
                SparkleParticle sparkle = new(Color.Yellow, 1, new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

                ParticleSystem.GenerateParticle(sparkle);
                ParticleSystem.GenerateParticle(particle);
            }
            return false;
        }
    }
}

/* public class GoldSkullTomeHeld : ModProjectile
 {
     public override string Texture => "RealmOne/Items/Weapons/PreHM/Desert/GoldenSkullTome";

     public override void SetDefaults()
     {
         Projectile.DamageType = DamageClass.Magic;
         Projectile.width = 32;
         Projectile.height = 32;
         Projectile.aiStyle = -1;
         Projectile.friendly = true;
         Projectile.hostile = false;
         Projectile.ignoreWater = true;
         Projectile.tileCollide = false;
     }
             private bool rightClicked;

     private Player player => Main.player[Projectile.owner];
     public override bool? CanDamage() => false;

     public override void AI()
     {
         int frameDelay = (int)(rightClicked? 20 * player.GetAttackSpeed(DamageClass.Magic) : 8 * player.GetAttackSpeed(DamageClass.Magic));

         player.heldProj = Projectile.whoAmI;
         if (Main.myPlayer == Projectile.owner)
         {
             Projectile.direction = player.direction;
             Projectile.spriteDirection = Projectile.direction;
             player.direction = Math.Sign(player.DirectionTo(Main.MouseWorld).X);
             player.heldProj = Projectile.whoAmI;
             player.itemTime = 2;
             player.itemAnimation = 2;
             Projectile.rotation = player.Center.DirectionTo(Main.MouseWorld).ToRotation();
             Projectile.Center = player.Center + player.Center.DirectionTo(Main.MouseWorld).ToRotation().ToRotationVector2() * 10;
             Projectile.netUpdate = true;
             player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.PiOver2);

             if (!player.channel)
             {
                 Projectile.Kill();
             }
             Projectile.frameCounter++;

             if (Projectile.frameCounter % frameDelay == 0)
             {
                 Projectile.frame++;
             }
             if (Projectile.frame == 3)
             {
                 ShootGoldenSkull();
             }

             if (Projectile.frame == 6)
             {
                ShootWoodenSkull();
             }
         }
     }
     private void ShootGoldenSkull()
     {
         if (!player.PickAmmo(player.HeldItem, out int type, out float speed, out int damage, out float knockBack, out int ammoItemID, false))
         {
             Projectile.Kill();
             Projectile.active = false;
         }

         SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
         type = ProjectileID.FlamingArrow;
       Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.DirectionTo(Main.MouseWorld) * 12, type, damage, 1, Projectile.owner, 0, 0);
     }

     private void ShootWoodenSkull()
     {
         if (!player.PickAmmo(player.HeldItem, out int type, out float speed, out int damage, out float knockBack, out int ammoItemID, false))
         {
             Projectile.Kill();
             Projectile.active = false;
         }
         type = ProjectileID.GoldenBullet;
         SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);

         Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.DirectionTo(Main.MouseWorld) * 12, type, damage, 1, Projectile.owner, 0, 0);
     }*/