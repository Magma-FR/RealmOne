using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class EyeballSoup : ModItem
    {
        public override void SetStaticDefaults()
        {
            

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(196, 59, 59  ),
                new Color(97, 97, 198   ),
                new Color(181, 79, 201 ),

            };
            ItemID.Sets.IsFood[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(20, 20, BuffID.WellFed2, 20600);
                
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = Item.buyPrice(0, 0, 3, 25);
            Item.rare = ItemRarityID.Orange;
            Item.consumable = true;
            Item.UseSound = SoundID.Item3;

        }
        public override bool CanUseItem(Player player)
        {
            player.AddBuff(BuffID.Panic, 1000);

            return true;
        }



    }
}