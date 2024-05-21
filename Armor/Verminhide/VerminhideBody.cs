/*using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Sets.OrchidSet;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Verminhide
{
    [AutoloadEquip(EquipType.Body)]
    public class VerminhideBody : ModItem
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
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)

            .AddIngredient(ModContent.ItemType<GrislyHide>(), 5)

            .AddTile(TileID.Loom)
            .Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions++;
            player.GetDamage(DamageClass.Summon) += 0.04f;
        }
    }
}*/