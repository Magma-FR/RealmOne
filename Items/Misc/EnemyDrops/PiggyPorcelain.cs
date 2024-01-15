using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.EnemyDrops
{
    public class PiggyPorcelain : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Piggy Porcelain"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Sharp, pink porcelain, dropped from the ones that carry all the gold"
                + "\n'Tell em to bring me my money!'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 50;
        }

        public override void SetDefaults()
        {
            Item.material = true;
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 2);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            float time = ((float)Math.Sin(Item.timeSinceItemSpawned / 60f * MathHelper.TwoPi) + 1) * .5f;
            Color color;
            if (time < 0.5f)
                color = Color.Lerp(Color.HotPink, Color.LightPink, time * 2f);
            else
                color = Color.Lerp(Color.Purple, Color.Yellow, time * 2f - 1);
            tooltips.Add(new TooltipLine(Mod, "PiggyPorcelain", Language.GetTextValue("'Imagine using this for armour'")) { OverrideColor = color });
        }
    }
}