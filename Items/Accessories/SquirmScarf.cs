/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class SquirmScarf : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squirm Scarf");
            Tooltip.SetDefault("Reduces damage taken by 8%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 10000;
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance = 1f - 0.95f * (1f - player.endurance);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "SquirmScarf", "'Itchiness and worms crawlin' in your clothes is guaranteed'")
            {
                OverrideColor = new Color(195, 118, 155)
            };
            tooltips.Add(line);

            // Here we give the item name a rainbow effect.
        }
    }
}*/