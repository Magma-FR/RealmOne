/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Whip;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Summoner
{
    public class ExtensionCord : ModItem
    {
        // The texture doesn't have the same name as the item, so this property points to it.
        //public override string Texture => "RealmOne/Items/Weapons/Summoner/ExtensionCord";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Extension Cord");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("'An unstable and live wire, containing many volts'"
                + "\nHold to charge the whip, fully charging the whip increases the whips length by 40% and inflicts Electricity"
                + "\nHas a multihit feature where the end of the whip increases in damage the amount of enemies you hit in a single row");
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "ExtensionCord", "'However, rats can connect many things together, building and multiplying at ease'")
            {
                OverrideColor = new Color(141, 209, 169)
            };
            tooltips.Add(line);
        }
        public override void SetDefaults()
        {
            // Call this method to quickly set some of the properties below.
            //Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 24;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<ExtensionCordProjectile>();
            Item.shootSpeed = 4;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.UseSound = SoundID.Item152;
            Item.channel = true; // This is used for the charging functionality. Remove it if your whip shouldn't be chargeable.
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(Mod, "ScavengerSteel", 8)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}*/