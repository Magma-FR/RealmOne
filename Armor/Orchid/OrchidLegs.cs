using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Orchid
{
    [AutoloadEquip(EquipType.Legs)]
    public class OrchidLegs : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 0, silver: 53, copper: 35);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.runAcceleration += 0.05f;
            player.moveSpeed += 0.05f;
        }
    }
}