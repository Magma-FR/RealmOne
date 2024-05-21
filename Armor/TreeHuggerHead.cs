using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TreeHuggerHead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tree Hugger Helmet");
            Tooltip.SetDefault("5% increased damage"
                + "\n'Helmet of the tree tops!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TreeHuggerBody>() && legs.type == ModContent.ItemType<TreeHuggerLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Gives a permanent Sunflower Happy Effect and 1+ life regeneration"; // This is the setbonus tooltip
            player.lifeRegen += 1;
            player.AddBuff(BuffID.Sunflower, 5);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 25)
            .AddIngredient(ItemID.Acorn, 5)
            .AddIngredient(Mod, "CrushedAcorns", 10)
            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}