/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Desert
{
    public class DesertWhisper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert's Whisper"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Shoots 2 fast moving cactus darts in a tight spread"
            + "\nRight click to summon rope snakes you can climb on");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item64;
            Item.autoReuse = true;
            Item.useAmmo = ItemID.Cactus;
            Item.noMelee = true;
            Item.shootSpeed = 36f;
            Item.shoot = ProjectileID.PoisonDartBlowgun;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int NumProjectiles = 2;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
                newVelocity *= 1f - Main.rand.NextFloat(0.4f);

                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.width = 32;
                Item.height = 32;
                Item.useTime = 28;
                Item.useAnimation = 28;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.rare = ItemRarityID.Green;
                Item.CloneDefaults(ItemID.MysticCoilSnake);
                Item.shoot = ProjectileID.MysticSnakeCoil;
                Item.shootSpeed = 1f;
            }
            else
            {
                Item.damage = 16;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 32;
                Item.height = 32;
                Item.useTime = 28;
                Item.useAnimation = 28;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.knockBack = 2f;
                Item.value = 30000;
                Item.rare = ItemRarityID.Green;
                Item.UseSound = SoundID.Item64;
                Item.autoReuse = true;
                Item.useAmmo = ItemID.Cactus;
                Item.noMelee = true;
                Item.shootSpeed = 36f;
                Item.shoot = ProjectileID.PoisonDartBlowgun;
            }

            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SandBlock, 15);
            recipe.AddIngredient(ItemID.SandstoneBrick, 15);
            recipe.AddIngredient(ItemID.MysticCoilSnake, 1);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-3, -3);
            return offset;
        }
    }
}*/