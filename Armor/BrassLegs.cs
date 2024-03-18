using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BrassLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Greaves");
            Tooltip.SetDefault("7% increased running and movement speed");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.07f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "BrassIngot", 4)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}