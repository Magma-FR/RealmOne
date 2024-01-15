/*using Microsoft.Xna.Framework;
using RealmOne.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables
{
    /// <summary>
    /// The coupled item that places the Example Pylon tile. For more information on said tile,
    /// see <seealso cref="ExamplePylonTile"/>.
    /// </summary>
    public class GrassPylonItem : ModItem
    {
        public override void SetDefaults()
        {
            // Basically, this a just a shorthand method that will set all default values necessary to place
            // the passed in tile type; in this case, the Example Pylon tile.
            Item.DefaultToPlaceableTile(ModContent.TileType<GrassPylonTile>());

            // Another shorthand method that will set the rarity and how much the item is worth.
            Item.SetShopValues(ItemRarityColor.Green2, Terraria.Item.buyPrice(gold: 10));
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Renaissance Pylon"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Right Click to teleport to another pylon and back!");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "GrassPylonItem", "'The Crystal was constructed by elder mages when they confronted unexpectedly huge amount of energy from the biome'")
            {
                OverrideColor = new Color(134, 232, 81)
            };
            tooltips.Add(line);
        }
    }
}
*/