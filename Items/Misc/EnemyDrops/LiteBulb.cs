using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc.EnemyDrops
{
    public class LiteBulb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0,0,0,80);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        
    }
}