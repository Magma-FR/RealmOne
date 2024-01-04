/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class SalmonAvoSushi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Salmon Avo Sushi Roll");
            Tooltip.SetDefault("'A tasty blend of the most carefully caught salmon in the ocean and fresh creamy slices of avocado'"
            + "\nGives Minor Improvements to all stats"
            + "\nIncreases Charm Duration by 8+ seconds"
            + "\nHeals 70 life");

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

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item2;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.healLife = 70;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "FreshSeaweed", 2);
            recipe.AddIngredient(ItemID.Salmon, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
*/