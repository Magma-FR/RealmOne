using RealmOne.Armor.Verminhide;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Goretooth
{
    [AutoloadEquip(EquipType.Head)]
    public class ToothHead : ModItem
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
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3; //
        }

        public override void UpdateEquip(Player player)
        {
            {
                player.GetDamage(DamageClass.Melee) += 0.08f;
                player.GetArmorPenetration(DamageClass.Melee) += 0.05f;
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ToothBody>() && legs.type == ModContent.ItemType<ToothLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            //      string tapDir = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
            player.setBonus = "When you are under 50% of health, you are surrounded by teeth. These teeth attack nearby enemies. 2+ extra defence";
            //        player.GetModPlayer<>().SpecialSetBonus = true;

            player.statDefense += 2;
        }
    }
}