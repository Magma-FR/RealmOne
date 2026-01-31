/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Throwing
{
    public class PreFlamedGel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pre-Flamed Gel"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Sparkling, but extremely flammable chunks of gel'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = 20000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.MolotovFire;
            Item.useAmmo = AmmoID.Gel;
            Item.shootSpeed = 9f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Gel, 20);
            recipe.AddIngredient(ItemID.Torch, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}*/