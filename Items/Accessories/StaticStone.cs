using Microsoft.Xna.Framework;
using RealmOne.Items.Weapons.PreHM.Impact;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class StaticStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Static Stone");
            Tooltip.SetDefault("Increases the effectiveness of the Electrified buff"
                + "\nFull immunity to the Electrified debuff"
                + "\nBuffs E-Weapons");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "StaticStone", "'Portable Electricity!'")
            {
                OverrideColor = new Color(0, 255, 255)
            };
            tooltips.Add(line);
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.material = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Electrified] = true;
            if (Item.type == ModContent.ItemType<ImpactPiercer>())
                Item.damage += 40;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "ImpactTech", 6);
            recipe.AddIngredient(ItemID.StoneBlock, 6);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}