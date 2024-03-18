using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class BarkShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bark Shield");
            Tooltip.SetDefault("'Carven from the bark of the trees'"
                + "\n10% increased axe and pickaxe speed when in the forest");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
            Item.defense += 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneForest)
            {
                player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}