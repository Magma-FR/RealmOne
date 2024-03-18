using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class Parchment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Parchment");
            Tooltip.SetDefault("'Parchment paper is made from cellulose fibers prepared from trees or plants such as cotton or flax.'"
                + "\n'Paper can be made which mimics the thickness and smooth surface of parchment.'"
                + "\n'Animal-free Parchment is used'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(copper: 69);
            Item.rare = ItemRarityID.White;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("Wood", 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}