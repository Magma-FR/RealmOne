using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    [AutoloadEquip(EquipType.Body)]
    public class TatteredBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tattered Wood Breastplate");
            Tooltip.SetDefault("5% increased damage and knockback");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Generic) += 0.05f;
            player.moveSpeed -= 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ModContent.ItemType<TatteredWood>(), 20)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}