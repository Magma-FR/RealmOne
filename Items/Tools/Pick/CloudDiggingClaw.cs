/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Tools.Pick
{
    public class CloudDiggingClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cloud Digging Claw"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Hand Crafted from the soft fluffy clouds of the sky'"
             + "\n'Light as the clouds'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 1;
            Item.crit = 3;
            Item.pick = 55;
            Item.useTurn = true;
            Item.value = Item.buyPrice(silver: 40, copper: 80);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Cloud, 15);
            recipe.AddIngredient(ItemID.SunplateBlock, 20);
            recipe.AddIngredient(ItemID.RainCloud, 15);
            recipe.AddTile(TileID.SkyMill);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}*/