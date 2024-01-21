using RealmOne.NPCs.Critters.Rain;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.ItemCritter
{
    public class WaterLizardCritter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Lizard");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 30;
            Item.maxStack = 99;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 4, silver: 10);
        }

        public override bool? UseItem(Player player)
        {
            NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<WaterLizard>());
            return true;
        }
    }
}