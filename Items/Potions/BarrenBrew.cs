using RealmOne.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions
{
    public class BarrenBrew : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Barren Brew"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'An environmentally friendly type of drink, not sure if you should drink it though'"
            + "\nInflicts Tangled"
            + "\nInflicts Poison");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.potion = true;
            Item.healLife = 70;
            Item.buffType = ModContent.BuffType<TangledBuff>();
            Item.buffTime = 10000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MudBlock, 2);
            recipe.AddIngredient(Mod, "GoopyGrass", 4);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}