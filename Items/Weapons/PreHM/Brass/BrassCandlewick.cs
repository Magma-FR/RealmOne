using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Brass
{
    public class BrassCandlewick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hold an antique and powerful candle that shoots out burning wax in a diversed and wide spread");
            DisplayName.SetDefault("Brass Candlewick");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //  ItemGlowy.AddGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Brass/BrassCandlewick_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.damage = 9;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.mana = 5;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 27f;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.UseSound = SoundID.Item131;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(gold: 3, silver: 20);

            Item.shoot = ProjectileID.MolotovFire;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Brass/BrassCandlewick_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.Orange,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 6 + Main.rand.Next(1); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(40);
            position += Vector2.Normalize(velocity) * 20f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.Candle, 1)
           .AddIngredient(Mod, "BrassIngot", 6)
            .AddIngredient(ItemID.PinkGel, 10)

            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe(1)
           .AddIngredient(ItemID.PlatinumCandle, 1)
           .AddIngredient(Mod, "BrassIngot", 6)
           .AddIngredient(ItemID.PinkGel, 10)

           .AddTile(TileID.Anvils)
           .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(2, 0);
            return offset;
        }
    }
}