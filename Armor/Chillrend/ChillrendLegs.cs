using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Chillrend
{
    [AutoloadEquip(EquipType.Legs)]
    public class ChillrendLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chillrend Boots");
            Tooltip.SetDefault("6% increased movement speed\nYou leave a trail of ice");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.06f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.IceBlock, 14)
                                    .AddIngredient(ItemID.GoldBar, 5)

                        .AddIngredient(ItemID.Snowball, 35)

            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()

         .AddIngredient(ItemID.IceBlock, 14)
                                 .AddIngredient(ItemID.PlatinumBar, 5)

                        .AddIngredient(ItemID.Snowball, 35)

         .AddTile(TileID.Anvils)
         .Register();
        }
    }
}