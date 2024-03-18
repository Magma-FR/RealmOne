using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    [AutoloadEquip(EquipType.Legs)]
    public class TatteredLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tattered Wood Grieves");
            Tooltip.SetDefault("5% increased movement speed"
                + "\n''Kinda uncomfortable'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 12);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TatteredWood>(), 18)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}