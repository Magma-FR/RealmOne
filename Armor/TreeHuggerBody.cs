using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TreeHuggerBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Tree Hugger Chestplate");
            Tooltip.SetDefault("5% increased knockback on all weapons"
                + "\n'Chestplate of the tree tops!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetKnockback(DamageClass.Generic) += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 30)
            .AddIngredient(ItemID.Acorn, 6)
            .AddIngredient(Mod, "CrushedAcorns", 30)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}