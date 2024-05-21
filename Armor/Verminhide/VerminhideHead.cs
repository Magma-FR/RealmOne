/*using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Sets.OrchidSet;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Verminhide
{
    [AutoloadEquip(EquipType.Head)]
    public class VerminhideHead : ModItem
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
                player.GetDamage(DamageClass.Summon) += 0.05f;
                player.maxMinions += 1;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)

            .AddIngredient(ModContent.ItemType<GrislyHide>(), 4)

            .AddTile(TileID.WorkBenches)
            .Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VerminhideBody>() && legs.type == ModContent.ItemType<VerminhideLegs>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "When you are under 50% of health, you are surrounded by teeth. These teeth attack nearby enemies. \n2+ maximum minions";
            //        player.GetModPlayer<>().SpecialSetBonus = true;

            player.statDefense += 2;
        }
    }
}*/