using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class StoneHilt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Hilt");
            Tooltip.SetDefault("'Smoothened stone, tied around the sword handle'"
                 + "\n'I'm positive this is what the vikings did when they invigorated their swords'"
                 + "\n7% increased melee speed and knockback"
                 + "\nThis also affects all tools");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetKnockback(DamageClass.Melee) += 0.07f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.07f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.StoneBlock, 12);
            recipe.AddIngredient(ItemID.Cobweb, 12);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}