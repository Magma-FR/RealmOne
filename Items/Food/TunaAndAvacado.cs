/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class TunaAndAvacado : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tuna and Avocado Sushi Roll");
            Tooltip.SetDefault("'A simple roll of sushi with beautiful Japanese Red Fin Tuna and creamy slices of avacado'"
            + "\nGives Minor Improvements to all stats"
            + "\nIncreases Charm Duration by 6+ seconds"
            + "\nHeals 60 life");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.IsFood[Type] = true;
            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(50, 200,50),
                new Color(20, 230, 180),
                new Color(20, 140, 20)

            };
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(22, 22, BuffID.WellFed, 57600);

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.healLife = 60;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "FreshSeaweed", 2);
            recipe.AddIngredient(ItemID.Tuna, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}*/