using RealmOne.RealmPlayer;
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
            player.setBonus = "[c/9BFF01:+2 defense]\n[c/9BFF01:+2 life regen]\nUpon dropping below half your health [c/FF5F5F:health], gain:\n [c/9BFF01:15% increased movement speed]\n[c/9BFF01:10% increased summon damage]\n[c/FF5F5F:Teeth Shroud]:\nsurround yourself with [c/FF5F5F:spinning teeth]\nUpon colliding with an enemy, the teeth lose their durability\nOnce the teeth 'break' they fly out\nThese teeth scale with your currently held summon weapon (otherwise deals true dmg)\nIf you are surrounded by teeth and you [c/9BFF01:regenerate] to past half of your [c/FF5F5F:health],\nlaunch all currently active teeth away from you";
            player.GetModPlayer<RealmModPlayer>().GoreToothBonus = true;
            player.lifeRegen += 2;
            player.statDefense += 2;

            if (player.statLife <= player.statLifeMax2 / 2)
            {
                player.moveSpeed += 0.15f;
                player.GetDamage(DamageClass.Generic) += 0.1f;
            }
        }
    }
}