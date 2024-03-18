using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food.FarmFood
{
    [AutoloadEquip(EquipType.Head)]
    public class FloralDelight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floral Delight");
            Tooltip.SetDefault("'Sweet, eadable flowers, filled with nutrients and goodness!"
                + "Gives the Heartreach buff"
             + "You can wear this on your head as well!");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(250, 0, 0),
                new Color(0 ,250, 0),
                new Color(0, 0, 250)
            };
            ItemID.Sets.IsFood[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.DefaultToFood(20, 20, BuffID.WellFed, 16000);

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.buyPrice(0, 0, 5, 95);
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.vanity = true;

            Item.UseSound = SoundID.Item2;
        }

        public override bool CanUseItem(Player player)
        {
            player.AddBuff(BuffID.Heartreach, 1000);
            return true;
        }
    }
}