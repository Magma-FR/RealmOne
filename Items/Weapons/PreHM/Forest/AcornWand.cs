using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Magic;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class AcornWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acorn Wand");
            Tooltip.SetDefault("Shoots out an acorn from the staff and the sky"
                + "\nThese snowflakes can pierce up to 3 enemies!'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 5;
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 9f;

            Item.useAnimation = 38;
            Item.useTime = 38;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 11);

            Item.shoot = ModContent.ProjectileType<BouncingAcorn>();
        }

        public int spreadMax = 22;
        public int spreadMin = -20;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int a = 0; a < Main.rand.Next(1, 3); a++)
            {
                float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi - MathHelper.PiOver2);

                Vector2 PositionArea = Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 20f;
                if (Collision.CanHit(position, 0, 0, position + PositionArea, 0, 0))
                    position += PositionArea;
            }

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 4f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            float numberProjectiles = 1 + Main.rand.Next(1); // 3, 4, or 5 shotsx`
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(velocity) * 3;
            for (int i = 1; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<BouncingAcorn>(), damage, knockback, player.whoAmI);
            }

            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 +
                             (Main.rand.Next(201) * -player.direction) + (Main.mouseX +
                                 Main.screenPosition.X - player.position.X)),
                    (float)(player.position.Y + player.height * 0.5 -
                             600.0));
                vector2_1.X = (float)((vector2_1.X + player.Center.X) / 2.0) +
                              Main.rand.Next(-200, 201);
                vector2_1.Y -= 100 * index;
                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if (num13 < 0.0) num13 *= -1f;
                if (num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + Main.rand.Next(spreadMin, spreadMax) * 0.02f; //Projectile Spread
                float SpeedY = num17 + Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(Terraria.Entity.GetSource_None(), vector2_1.X, vector2_1.Y, SpeedX, SpeedY, type, damage,
                                 knockback, Main.myPlayer, 0.0f, Main.rand.Next(1));
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.Wood, 12)
            .AddIngredient(ItemID.Acorn, 6)
            .AddTile(TileID.WorkBenches)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(2, 0);
            return offset;
        }
    }
}