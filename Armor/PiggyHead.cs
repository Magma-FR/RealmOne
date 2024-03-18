using RealmOne.RealmPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class PiggyHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Piggy Patroller Mask");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.07f;
            player.runAcceleration -= 0.03f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PiggyBody>() && legs.type == ModContent.ItemType<PiggyLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased armor penetration\nGreatly increased coin pickup range\nTaking damage has a chance to rain coins around you\nUpon going under 60% health, a healing piggy bank drops on your cursor\nIt explodes, dealing damage to enemies\nThis has a minute cooldown"; // This is the setbonus tooltip
            player.GetArmorPenetration(DamageClass.Generic) += 10f;
            player.GetModPlayer<RealmModPlayer>().PiggySet = true;
            player.goldRing = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "PiggyPorcelain", 5)
            .AddTile(TileID.Furnaces)
            .Register();
        }
    }
}