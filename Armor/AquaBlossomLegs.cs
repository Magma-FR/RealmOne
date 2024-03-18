/*using RealmOne.Items.Misc;
using RealmOne.Items.Misc.Plants;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class AquaBlossomLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqua Blossom Footwear");
            Tooltip.SetDefault("6% increased movement speed and jump speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 2;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed += 0.06f;
            player.jumpSpeedBoost += 0.06f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "Aquablossom", 6)
                                    .AddIngredient(ModContent.ItemType<WaterDriplets>(), 6)

            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
*/