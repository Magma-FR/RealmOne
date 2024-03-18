using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class TreeHuggerLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tree Hugger Trousers");
            Tooltip.SetDefault("5% increased movement speed and acceleration"
                + "\n'Trousers of the tree tops!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
            player.accRunSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 20)
            .AddIngredient(ItemID.Acorn, 6)
            .AddIngredient(Mod, "CrushedAcorns", 20)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}