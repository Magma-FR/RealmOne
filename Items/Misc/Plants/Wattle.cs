/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.Plants
{
    public class Wattle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wattle");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(0, 0, 1, 10);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            maxFallSpeed = 0.15f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.NavajoWhite;
        }
    }
}
*/