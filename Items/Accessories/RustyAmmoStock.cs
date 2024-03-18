using RealmOne.RealmPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class RustyAmmoStock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusty Ammo Stock");
            Tooltip.SetDefault("30% chance to not consume ammo"
                + "\nDue to rust, you are bleeding"
                + "\n10 second affect after unequipped");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Bleeding, 300);
            player.GetModPlayer<RealmModPlayer>().Rusty = true;
        }
    }
}