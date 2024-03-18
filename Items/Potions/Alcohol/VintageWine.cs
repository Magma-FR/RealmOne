using RealmOne.Buffs.AlcoholBuffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions.Alcohol
{
    public class VintageWine : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spicegrass Wine"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            /* Tooltip.SetDefault("A tangy and almost spicy wine."
            + "\nGives the Spicegrass Wine Buff"
            + "\nIncreases acceleration and running speed by 8% and weapon velocity but -2 defence"
                        + $"\nIngreients: [i:{ItemID.Mushroom}], [i:{ItemID.Grapes}] [i:{ItemID.Fireblossom}]"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 5000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = BuffID.Ironskin;

            Item.buffType = ModContent.BuffType<WineBuff>();
            Item.buffTime = 10000;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddIngredient(ItemID.Mushroom, 3);
            recipe.AddIngredient(ItemID.Grapes, 1);

            recipe.AddIngredient(ItemID.Fireblossom, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}