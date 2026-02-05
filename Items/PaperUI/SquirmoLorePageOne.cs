/*using RealmOne.Common.Systems;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.PaperUI
{
    public class SquirmoLorePageOne : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lore Scroll (Squirmo)"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Open up a scroll to reveal the secrets of the soil");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.maxStack = 1;
            Item.UseSound = rorAudio.SFX_Scroll;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.autoReuse = false;
            Item.reuseDelay = 29;
            Item.noUseGraphic = true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<Scroll>().ScrollContext != 3)
            {
                player.GetModPlayer<Scroll>().ScrollContext = 3;
            }
            else
            {
                player.GetModPlayer<Scroll>().ScrollContext = 0;
            }

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Parchment", 5);
            recipe.AddIngredient(ItemID.Worm, 5);

            //recipe.AddTile(Mod, "SquirmoRelic");
            recipe.Register();
        }
    }
}*/