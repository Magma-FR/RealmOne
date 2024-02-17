using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Orchid
{
    [AutoloadEquip(EquipType.Body)]
    public class OrchidBody: ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            
            player.GetDamage(DamageClass.Generic) += 0.03f;
            player.maxMinions += 1;

        }
    }
}