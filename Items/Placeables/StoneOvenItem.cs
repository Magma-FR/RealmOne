using RealmOne.Rarities;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables
{
    public class StoneOvenItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Oven");
            Tooltip.SetDefault("Capable of cooking wheat based foods"
                + "\n'Always cooked to perfection!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            //  Item.createTile = ModContent.TileType<Tiles.StoneOvenTilee>(); // This sets the id of the tile that this item should place when used.
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.StoneOvenTilee>());

            Item.width = 28; // The item texture's width
            Item.height = 14; // The item texture's height

            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.rare = ModContent.RarityType<ModRarities>();

            Item.maxStack = 99;
            Item.consumable = true;
            Item.value = 150;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Furnace, 1)
                .AddIngredient(ItemID.StoneBlock, 50)
                .AddIngredient(ItemID.Torch, 25)
                 .AddIngredient(ItemID.Wood, 20)

                .Register();
        }
    }
}