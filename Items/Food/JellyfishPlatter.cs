using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Food
{
    public class JellyfishPlatter : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(23, 234, 52 ),
                new Color(23, 179, 234 ),
                new Color(239, 26, 216 ),
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
            Item.value = Item.buyPrice(0, 0, 5, 25);
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.UseSound = SoundID.Item2;
        }

        public override bool CanUseItem(Player player)
        {
            player.AddBuff(BuffID.Fishing, 4000);
            player.AddBuff(BuffID.Crate, 4000);

            return true;
        }
    }
}