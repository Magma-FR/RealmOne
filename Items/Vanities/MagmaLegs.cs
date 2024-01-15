using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Vanities
{
    [AutoloadEquip(EquipType.Legs)]
    public class MagmaLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darklava Greaves");
            Tooltip.SetDefault("Put this on, and you'll feel like a GOD");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Expert;
            Item.defense = 1; 
        }

        public override void UpdateArmorSet(Player player)
        {
            player.statDefense += 2;
        }
    }
}