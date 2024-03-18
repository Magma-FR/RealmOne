using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class TreeSapJuice : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tree Sap Juice");
            Tooltip.SetDefault("Enemies are less likely to target you\r\nWhen in the forest you are granted 5% increased movement speed\r\n");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(50, 200,50),
                new Color(200,50 , 180),
                new Color(20, 140, 20)
            };
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 500;
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.healLife = 70;
            Item.buffType = ModContent.BuffType<TreeSapBuff>();
            Item.buffTime = 3600;
            Item.UseSound = SoundID.Item3;
        }

        public override void AddRecipes()
        {
            CreateRecipe(2)
             .AddIngredient(ItemID.BottledWater, 1)
             .AddIngredient(ItemID.Wood, 4)
             .AddIngredient(ItemID.Daybloom, 1)

            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}