using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Chillrend
{
    [AutoloadEquip(EquipType.Body)]
    public class ChillrendBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault("Chillrend Breastplate");
            Tooltip.SetDefault("6% increases ranged damage\n6% increased ranged speed'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[((ModItem)this).Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Ranged) += 0.06f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.IceBlock, 22)
                        .AddIngredient(ItemID.GoldBar, 10)

                                    .AddIngredient(ItemID.Snowball, 50)

            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()

        .AddIngredient(ItemID.IceBlock, 22)
                    .AddIngredient(ItemID.PlatinumBar, 10)

                                .AddIngredient(ItemID.Snowball, 50)

        .AddTile(TileID.Anvils)
        .Register();
        }
    }
}