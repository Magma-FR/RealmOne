using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Orchid
{
    [AutoloadEquip(EquipType.Head)]
    public class OrchidHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1; //
        }

        public override void UpdateEquip(Player player)
        {
            {
                player.GetCritChance(DamageClass.Generic) += 5f;

            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OrchidBody>() && legs.type == ModContent.ItemType<OrchidLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            //      string tapDir = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
            player.setBonus = "Summon an aura that gives you extra regen and heart and mana pickup. This aura slows down enemies and changes their colour slightly.";
            //        player.GetModPlayer<>().SpecialSetBonus = true;

            player.lifeRegen += 5;
            player.lifeMagnet = true;
            player.manaMagnet = true;
        }
    }
}