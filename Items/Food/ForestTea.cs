using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class ForestTea : ModItem
    {
        public override void SetStaticDefaults()
        {
            

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
                new Color(228, 154, 106 ),
                new Color(137, 54, 0),
            };
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(20, 20, BuffID.WellFed2, 20600);
                
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = Item.buyPrice(0, 0, 3, 25);
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;

        }
        


    }
}