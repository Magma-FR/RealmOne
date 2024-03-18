/*using RealmOne.RealmPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class TheOutcastsOverseer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Outcast's Overseer");
            Tooltip.SetDefault("'A physical form of the dirt titan's purpose'"
                + "\n6% increased critical chance"
                + "\nCrits have a chance to increase your crit chance"
                + "\nThe effect increases your crit chance by %4+"
                + "\nMaximum crit stack is +20% (5 times)");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<RealmModPlayer>().Overseer = true;
            player.GetCritChance(DamageClass.Generic) += 6f;
        }
    }
}*/