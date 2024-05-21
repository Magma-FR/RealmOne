using Microsoft.Xna.Framework;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Items.Placeables.FarmStuff;
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
            //        ItemGlowy.AddGlowMask(Item.type, Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTurn = true;
            Item.damage = 11;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = ItemRarityID.Blue;
            Item.mana = 6;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.shoot = ModContent.ProjectileType<DesertHands>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book, 1)
                                .AddIngredient(ItemID.SandstoneBrick, 15)
                                .AddIngredient(ItemID.Cactus, 25)
                                .AddIngredient(ItemID.GoldBar, 10)

                .AddTile(TileID.WorkBenches)
                .Register();

            CreateRecipe()
              .AddIngredient(ItemID.Book, 1)
              .AddIngredient(ItemID.SandstoneBrick, 15)
              .AddIngredient(ItemID.Cactus, 25)
              .AddIngredient(ItemID.PlatinumBar, 10)

              .AddTile(TileID.WorkBenches)
              .Register();
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