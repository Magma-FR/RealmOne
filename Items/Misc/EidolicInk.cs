/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
    public class EidolicInk : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eidolic Ink");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(silver: 25);
            Item.rare = ItemRarityID.Purple;
            Item.maxStack = 999;
            Item.ammo = Item.type;
            Item.consumable = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "EidolicInk", "'Such elegant and expensive ink, use it effectively'")
            {
                OverrideColor = new Color(95, 6, 195)
            };
            tooltips.Add(line);
        }
    }
}*/