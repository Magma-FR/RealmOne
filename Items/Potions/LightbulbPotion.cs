using RealmOne.Buffs;
using RealmOne.Items.Misc;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions
{
    public class LightbulbPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb Potion"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'An ultra bright fluid that consists of large groups of luminosity minerals'"
            + "\n'It's safe to drink, however it feels overly warm'"
            + "\n'Gives the player a bright aura that provides warmth and reduces enemy aggression'"
            + "\nThe aura gets brighter when underground");
        }

        public override void SetDefaults()
        {

            Item.height = 32;
            Item.width = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 500;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<LightbulbBuff>();
            Item.buffTime = 9000;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/LightbulbShine");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Mushroom, 2);
            recipe.AddIngredient(ModContent.ItemType<LiteBulb>(), 2);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}