using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food.FarmFood
{
    public class Carrot : ModItem
    {
        public bool IsFood = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carrot");
            Tooltip.SetDefault("'Crunchy and amazing for your eyesight!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(246, 136, 26),
                new Color(255, 187, 119),
                new Color(243, 202, 84)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(20, 20, BuffID.WellFed, 30600);

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.buyPrice(0, 0, 2, 95);
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
        }

        public override bool CanUseItem(Player player)
        {
            player.AddBuff(BuffID.NightOwl, 1000);
            return true;
        }
    }
}