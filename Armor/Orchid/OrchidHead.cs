using RealmOne.Items.Sets.OrchidSet;
using RealmOne.RealmPlayer;
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
            Item.value = Item.sellPrice(gold: 0, silver: 57, copper: 80);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 1; //
        }

        public override void UpdateEquip(Player player)
        {
            {
                player.GetCritChance(DamageClass.Generic) += 4f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<OrchidEssence>(), 5)
            .AddIngredient(ItemID.Mushroom, 2)

            .AddTile(TileID.WorkBenches)
            .Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OrchidBody>() && legs.type == ModContent.ItemType<OrchidLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            //      string tapDir = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
            player.setBonus = "Conjures a leafy aura from nearby spirits that increases health- and mana regen.\nRandomly conjures homing forest spirits \nMana and heart drops inside the aura will be carried by leaves to you";
            //        player.GetModPlayer<>().SpecialSetBonus = true;

            player.GetModPlayer<RealmModPlayer>().OrchidBonus = true;
            player.lifeRegen += 2;
            player.manaRegen += 2;
        }
    }
}