using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions
{
    public class BreathePotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Breathe Potion"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Your lungs will thank you after 'drinking liquid air'"
            + "\nImmune to suffocation due to lack of air in space"
            + "\nIncreases jump speed and acceleration by 10%"
            + "\nThese stats increase by +10% when in space");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 500;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<BreatheBuff>();
            Item.buffTime = 10000;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "BreathePotion", "'BREATHE AIR!!!'")
            {
                OverrideColor = new Color(10, 255, 255)
            };
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(3);
            recipe.AddIngredient(ItemID.Feather, 1);
            recipe.AddIngredient(ItemID.Cloud, 4);
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}