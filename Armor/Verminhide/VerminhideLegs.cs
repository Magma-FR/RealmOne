using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Sets.OrchidSet;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Verminhide
{
    [AutoloadEquip(EquipType.Legs)]
    public class VerminhideLegs : ModItem
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
            Item.defense = 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)

            .AddIngredient(ModContent.ItemType<GrislyHide>(), 4)

            .AddTile(TileID.WorkBenches)
            .Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.08f;
            player.moveSpeed += 0.08f;
        }
    }
}