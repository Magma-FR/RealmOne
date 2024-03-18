using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.EnemyDrops
{
    public class GizmoScrap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gizmo Scrap");
            Tooltip.SetDefault("A mixture of ores, metal and handy parts in one clump"
                + "\n'Dropped from the sneakiest of goblins'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 35;
        }

        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(silver: 19);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;
        }
    }
}