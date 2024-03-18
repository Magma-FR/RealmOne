using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.TatteredWoodSet
{
    [AutoloadEquip(EquipType.Head)]
    public class TatteredMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tattered Wood Headmask");
            Tooltip.SetDefault("5% increased damage and crit"
                + "\n''You could get a splinter in your eye!");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 18);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.GetCritChance(DamageClass.Generic) += 0.3f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TatteredLegs>() && legs.type == ModContent.ItemType<TatteredBody>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "You are immune to Slow and Poisoned. 5% increased weapon knockback"; // This is the setbonus tooltip
            player.buffImmune[BuffID.Slow] = true;
            player.GetKnockback(DamageClass.Generic) += 0.05f;
            player.buffImmune[BuffID.Poisoned] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<TatteredWood>(), 18)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}