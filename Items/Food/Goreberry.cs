using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class Goreberry : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goreberry");
            Tooltip.SetDefault("Dangerously sweet, increases your heart rate'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
                new Color(191, 0, 47),
                new Color(247, 0, 170),
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(20, 20, BuffID.WellFed, 20600);

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.buyPrice(0, 0, 3, 95);
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
        }

        public override bool CanUseItem(Player player)
        {
            player.AddBuff(BuffID.Lifeforce, 3000);
            return true;
        }
    }
}