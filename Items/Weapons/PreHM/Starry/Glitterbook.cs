/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc;
using RealmOne.Projectiles.Magic;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Starry
{
    public class Glitterbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glitterbook"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Starstriking Fury!!'"
            + "\n'Shoots out fast cosmic stars stars that eventually spread out in a cone'");

            ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Starry/Glitterbook_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 30;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ProjectileType<Stary>();
            Item.shootSpeed = 40f;
            Item.value = Item.buyPrice(gold: 2, silver: 75);
            Item.mana = 3;
            Item.noMelee = true;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(1, 0);
            return offset;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Starry/Glitterbook_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.LightYellow,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int a = 0; a < Main.rand.Next(1, 5); a++)
            {
                float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi - MathHelper.PiOver2);

                Vector2 PositionArea = Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 20f;
                if (Collision.CanHit(position, 0, 0, position + PositionArea, 0, 0))
                    position += PositionArea;
            }

            Lighting.AddLight(player.position, r: 0.3f, g: 0.25f, b: 0.1f);
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 4f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            float numberProjectiles = 2 + Main.rand.Next(1); // 3, 4, or 5 shotsx`
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(velocity) * 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileType<Stary>(), damage, knockback, player.whoAmI);
            }

            for (int i = 0; i < 130; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1.3f, 1.3f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.YellowStarDust, speed * 8, Scale: 1f);
                ;
                d.noGravity = true;
            }

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Top, DustID.CrystalSerpent_Pink, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Right, DustID.CrystalSerpent_Pink, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Bottom, DustID.CrystalSerpent_Pink, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Left, DustID.CrystalSerpent_Pink, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            return false;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemType<EnchantedStarglitter>(), 15)
            .AddIngredient(Mod, "Parchment", 5)
            .AddIngredient(ItemID.FallenStar, 1)

            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
*/