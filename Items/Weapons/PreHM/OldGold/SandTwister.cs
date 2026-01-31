/*using RealmOne.Items.Misc.Bars;
using RealmOne.Projectiles.HeldProj;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.OldGold
{
    public class SandTwister : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Twister Yoyo"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Summons mini sandstorms near the yoyo"
                + "\n'Don't get twisted!");
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<SandTwisterProj>();
            Item.shootSpeed = 20f;

            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Shameful, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool MeleePrefix()
        {
            return true;
        }

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
            .AddIngredient(ModContent.ItemType<OldGoldBar>(), 10)
            .AddIngredient(ItemID.Cobweb, 25)
            .AddIngredient(ItemID.Sandstone, 15)

            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}*/