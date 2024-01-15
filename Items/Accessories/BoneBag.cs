/*using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class BoneBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Bag");
            Tooltip.SetDefault("'Used from the Clothier a long time when he was still trapped in the dungeon'"
                + "\n'Ya bonehead!'"
                + "\nSkeletons and dungeon enemies drop more loot and bones"
              + "\nShows the location of treasure and enemies in the dungeon");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;

            Item.material = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneDungeon)
            {
                player.AddBuff(BuffID.Spelunker, 20);
                player.AddBuff(BuffID.Hunter, 20);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.Silk, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe balls = CreateRecipe();
            balls.AddIngredient(ItemID.Bone, 25);
            balls.AddIngredient(ItemID.DemoniteBar, 10);
            balls.AddIngredient(ItemID.Silk, 3);
            balls.AddTile(TileID.Anvils);
            balls.Register();
        }
    }
}*/