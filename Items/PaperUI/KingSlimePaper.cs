/*using RealmOne.Common.Systems;
using RealmOne.Items.Misc;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.PaperUI
{
    public class KingSlimePaper : ModItem
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
                .AddIngredient(ItemID.KingSlimeTrophy, 1)

                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<ScrollySlime>().ShowSlime == false)
            {
                player.GetModPlayer<ScrollySlime>().ShowSlime = true;
                player.GetModPlayer<Scrolly>().ShowScroll = false;
                player.GetModPlayer<ScrollyWorm>().ShowWorm1 = false;
            }
            else
            {
                player.GetModPlayer<ScrollySlime>().ShowSlime = false;
            }

            return base.UseItem(player);
        }
    }
}*/