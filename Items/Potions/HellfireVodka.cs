/*using RealmOne.Buffs;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions
{
    public class HellfireVodka : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Vodka");
            Tooltip.SetDefault("15% running speed, movement speed and accerelation\r\nAll weapons inflict OnFire\r\n10% increased damage and knockback\r\n5- Defense\r\nYou are On Fire!\r\n");
        }

        public override void SetDefaults()
        {
            Item.height = 32;
            Item.width = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 50000;
            Item.rare = ItemRarityID.Orange;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<HellfireVodkaBuff>();
            Item.buffTime = 9000;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/DrinkingFire");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.HellstoneBar, 4);
            recipe.AddIngredient(ItemID.LavaBucket, 1);
            recipe.AddIngredient(ModContent.ItemType<HellishMembrane>());
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
*/