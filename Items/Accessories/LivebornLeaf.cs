using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Sets.OrchidSet;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class LivebornLeaf : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Noxius Leaf");
            Tooltip.SetDefault("'Bacteria infected leaf, radiating with nature's power'"
                 + "\n10% increased summon damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OrchidEssence>(), 4);
            recipe.AddIngredient(ItemID.Wood, 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}