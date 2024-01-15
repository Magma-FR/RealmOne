using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.EnemyDrops
{
    public class ScathedFlesh : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scathed Flesh");
            Tooltip.SetDefault("'Scarred, bloody flesh off a hungry vertabrae of the crimson'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 20000;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;
        }
    }
}