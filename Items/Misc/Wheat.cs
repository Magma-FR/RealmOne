using RealmOne.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class Wheat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wheat"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Hand picked from the fields, ready to be baked'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
        }

        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = 20000;
            Item.maxStack = 999;
            Item.rare = ModContent.RarityType<ModRarities>();
        }
    }
}