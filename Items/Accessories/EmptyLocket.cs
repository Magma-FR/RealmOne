using RealmOne.Items.Others;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class EmptyLocket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empty Locket of The Lords");
            Tooltip.SetDefault("'The internal chaos from the locket was too much for the gods to bespoke..."
                + "\nSo they abandoned it to a new rival that can withstand the essence'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.defense += 1;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EntropyMedallion>(), 1, 1, 1));
        }

        public override void UpdateEquip(Player player)
        {
            Lighting.AddLight(player.position, r: 0.6f, 0.3f, b: 1f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<EntropyMedallion>(), 1);

            recipe.Register();
        }
    }
}