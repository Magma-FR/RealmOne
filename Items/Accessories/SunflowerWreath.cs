using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    [AutoloadEquip(EquipType.Head)]
    public class SunflowerWreath : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            if (Main.netMode != NetmodeID.Server)
                ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.defense += 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.dayTime && player.ZoneOverworldHeight)
            {
                // Apply the 'Happy' buff
                player.AddBuff(BuffID.Sunflower, 1); // Buff lasts for 5 in-game hours (5 minutes)
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SunflowerPetal>(), 5)

                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}