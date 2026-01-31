/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class VerdantBindis : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sticky Bindis");
            Tooltip.SetDefault("2+ Base Damage on all weapons"
                 + "\nYou are poisoned when equipping this, I mean you're carrying around Poisonous Bindis? :skull:");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic).Base += 2f;
            player.AddBuff(BuffID.Poisoned, 60);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "VerdantBindis", "'Australians would even think this is the devil itself!'")
            {
                OverrideColor = new Color(0, 204, 102)
            };
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddIngredient(Mod, "GoopyGrass", 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}*/