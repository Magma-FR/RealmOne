using Microsoft.Xna.Framework;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using RealmOne.Common.Core;
using RealmOne.Items.Weapons.PreHM.Classless;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Common.Events.CursedForest
{
    public class CursedForestItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 10;
            Item.noMelee = true;
            Item.consumable = true;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item43;
        }

        public override bool CanUseItem(Player player)
        {
            if ((player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && !Main.pumpkinMoon && !Main.snowMoon)
                return false;

            if (CursedForestEvent.CursedForest || Main.dayTime)
                return false;

            return true;
        }

        public override bool? UseItem(Player player)
        {
            Main.NewText("The Forest has shifted into a transcendental state!!", 200, 0, 80);
            SoundEngine.PlaySound(rorAudio.LeechHeartEat, new Vector2((int)player.position.X, (int)player.position.Y));
            player.AddBuff(BuffID.Battle, 12000);
            CursedForestEvent.CursedForest = true;
            for (int i = 0; i < 5; i++)
            {
                GenericGlowParticle particle = new(new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), Color.IndianRed, 0.5f, 120);
                SparkleParticle sparkle = new(Color.Red, 1, new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

                ParticleSystem.GenerateParticle(sparkle);
                ParticleSystem.GenerateParticle(particle);
            }
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
            else
            {
                CursedForestEvent.CursedForest = true;
            }
            return true;
        }
    }

    internal class CrossLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            // Place some additional items in Frozen Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInWoodChests1 = { ItemType<CursedForestItem>(), ItemType<CursedForestItem>(), ItemType<CursedForestItem>() };
            // This variable will help cycle through the items so that different Frozen Chests get different items
            int itemsToPlaceInWoodChestsChoice1 = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each).
            int itemsPlaced1 = 0;
            int maxItems1 = 20;
            // Loop over all the chests
            for (int chestIndex1 = 0; chestIndex1 < Main.maxChests; chestIndex1++)
            {
                Chest chest1 = Main.chest[chestIndex1];
                if (chest1 == null)
                {
                    continue;
                }
                Tile chestTile1 = Main.tile[chest1.x, chest1.y];
                // We need to check if the current chest is the Frozen Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Frozen Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Frozen Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Frozen_Chest
                if (chestTile1.TileType == TileID.Containers && chestTile1.TileFrameX == 12 * 36)
                {
                    // We have found a Frozen Chest
                    // If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(2))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex1 = 0; inventoryIndex1 < Chest.maxItems; inventoryIndex1++)
                    {
                        if (chest1.item[inventoryIndex1].type == ItemID.None)
                        {
                            // Place the item
                            chest1.item[inventoryIndex1].SetDefaults(itemsToPlaceInWoodChests1[itemsToPlaceInWoodChestsChoice1]);
                            // Decide on the next item that will be placed.

                            itemsToPlaceInWoodChestsChoice1 = (itemsToPlaceInWoodChestsChoice1 + 1) % itemsToPlaceInWoodChests1.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInFrozenChests));
                            itemsPlaced1++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced1 >= maxItems1)
                {
                    break;
                }
            }
        }
    }
}