using RealmOne.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class MidnightHunterOptics : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnight Hunter Optics");
            Tooltip.SetDefault("'A necessity when hunting at night'"
                 + "\nHunter and Nightowl affects"
                 + "\n+6% increased crit chance");

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
            player.GetCritChance(DamageClass.Generic) += 6f;
            player.AddBuff(ModContent.BuffType<OpticBuff>(), 60);
        }
    }
}