
/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.HeldProj;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops
{
    public class SquirYo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squir-Yo"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Shoots out worms that seek nearby enemies'"
                + "\nShoots out more worms the more the yoyo hits an enemy");
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 17;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<SquirYoProjectile>();
            Item.shootSpeed = 22f;

            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

        }
        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool AllowPrefix(int pre)
        {
            // return false to make the game reroll the prefix

            // DON'T DO THIS BY ITSELF:
            // return false;
            // This will get the game stuck because it will try to reroll every time. Instead, make it have a chance to return true

            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
                // IndexOf returns a positive index of the element you search for. If not found, it's less than 0. Here check the opposite
                // Rolled a prefix we don't want, reroll
                return false;
            // Don't reroll
            return true;
        }

           public override void AddRecipes()
           {
               CreateRecipe(1)
               .AddIngredient(ItemID.Worm, 3)
               .AddIngredient(ItemID.Cobweb, 15)
               .AddIngredient(ItemID.MudBlock, 25)

               .AddTile(TileID.Anvils)
               .Register();
           }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "SquirYo", "'Don't tell me how worms are supposed to come out of a spinning yo-yo? :shrug:'")
            {
                OverrideColor = new Color(195, 118, 155)

            };
            tooltips.Add(line);

        }
        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath11);
            return true;
        }
    }
}*/