/*using RealmOne.RealmPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class GreenNecklace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Renaissance Cross");
            Tooltip.SetDefault("'Wrap Evolution around your neck'"
                 + "\n'Worn from the dying soldiers of the past'"
                 + "\nAll weapons and equipment inflict 'Tangled'"
                 + "\nYou gain Night Owl, Thorns, Swiftness and Titan while equipping this"
                 + "\nWhen in the Renaissance Biome, all enemies and bosses instantly get inflicted with the 'Withering Evolution' debuff, which obliterates their health by 30%");

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
            player.GetModPlayer<RealmModPlayer>().GreenNeck = true;
        }
    }
}*/