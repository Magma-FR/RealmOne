using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.Magic;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class Twigbulb : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Casts a bright, dangly lightbulb at the mouse position." + "\nLights up a large area.");
            DisplayName.SetDefault("Twigbulb");
            ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Forest/Twigbulb_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 10;
            Item.damage = 6;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 3f;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item132;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 60);
            Item.channel = true;
            Item.scale = 1f;

            Item.shoot = ProjectileType<LightBulbRing1>();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Forest/Twigbulb_Glow", AssetRequestMode.ImmediateLoad).Value;
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

        /*  public override Color? GetAlpha(Color lightColor)
          {
              // Aside from SetDefaults, when making a copy of a vanilla weapon you may have to hunt down other bits of code. This code makes the item draw in full brightness when dropped.
              return Color.NavajoWhite;
          }*/

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 mouse = Main.MouseWorld;
            Projectile.NewProjectile(source, mouse.X, mouse.Y, 0f, 0f, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.Glass, 15)
            .AddIngredient(ModContent.ItemType<LiteBulb>(), 6)

            .AddRecipeGroup("Wood", 16)
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