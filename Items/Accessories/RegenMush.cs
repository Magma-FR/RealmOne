using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class RegenMush : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom of Regeneration");
            Tooltip.SetDefault("'3+ Health Regen'"
                 + "\n'Do not eat it!'");

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
        public int lifeRegen = 3;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += lifeRegen;
        }
    }
}