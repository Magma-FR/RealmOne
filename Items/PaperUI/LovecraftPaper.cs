using RealmOne.Common.Systems;
using RealmOne.Common.UI;
using RealmOne.Items.Misc;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.PaperUI
{
    public class LovecraftPaper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The World we live in - H.P Lovecraft"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.

            Tooltip.SetDefault("Open a scroll of Lovecraft's Ancient Scripts.");
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Parchment>(), 15)

                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<ScrollySlime>().ScrollContext != 2)
            {
                player.GetModPlayer<ScrollySlime>().ScrollContext = 2;
            }
            else
            {
                player.GetModPlayer<ScrollySlime>().ScrollContext = 0;
            }

            return base.UseItem(player);
        }
    }
}