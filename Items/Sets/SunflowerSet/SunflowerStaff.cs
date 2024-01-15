using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.Plants;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.SunflowerSet
{
    public class SunflowerStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemGlowy.AddItemGlowMask(Item.type, Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 8;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 0, 8, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<SpinFlower>();
            Item.shootSpeed = 15f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SunflowerPetal>(), 15)
                .AddIngredient(ItemID.Sunflower, 1)
                .AddIngredient(ItemID.Wood, 15)

                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Vector2 mouse = Main.MouseWorld;
                Projectile.NewProjectile(source, mouse.X, mouse.Y, 0, 0, ModContent.ProjectileType<SpinFlower>(), damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}