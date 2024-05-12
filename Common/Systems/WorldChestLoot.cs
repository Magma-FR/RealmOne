using RealmOne.Common.Systems.GenPasses;
using RealmOne.Items.Accessories;
using RealmOne.Items.Food;
using RealmOne.Items.Misc.Plants;
using RealmOne.Items.Opens;
using RealmOne.Items.Weapons.PreHM.Classless;
using RealmOne.Items.Weapons.PreHM.Grenades;
using RealmOne.Items.Weapons.PreHM.Throwing;
using RealmOne.NPCs.Enemies.Underground;
using RealmOne.Tiles;
using RealmOne.Tiles.Ambient;
using RealmOne.Tiles.PlantTiles;
using RealmOne.Tiles.Torches;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Common.Systems
{
    internal class WoodenChestLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            // These are the 3 new items we will place.
            int[] itemsToPlaceInWoodChests = { ItemType<StackPotions>(), ItemType<StackPotions>(), ItemType<StackPotions>() };
            // This variable will help cycle through the items so that different Frozen Chests get different items
            int itemsToPlaceInWoodChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each).
            int itemsPlaced = 0;
            int maxItems = 20;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 0 * 36)
                {
                    // We have found a Frozen Chest
                    // If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(3))
                        continue;
                    // Next we need to find the first empty slot for our istem
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWoodChests[itemsToPlaceInWoodChestsChoice]);
                            chest.item[inventoryIndex].stack = WorldGen.genRand.Next(10, 18);

                            itemsToPlaceInWoodChestsChoice = (itemsToPlaceInWoodChestsChoice + 1) % itemsToPlaceInWoodChests.Length;
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }
    }

    internal class GoldenChestLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            // Place some additional items in Frozen Chests:
            // These are the 3 new items we will place.
            int[] itemsToPlaceInWoodChests = { ItemType<MinersPouch>(), ItemType<MinersPouch>(), ItemType<MinersPouch>() };
            // This variable will help cycle through the items so that different Frozen Chests get different items
            int itemsToPlaceInWoodChestsChoice = 0;
            // Rather than place items in each chest, we'll place up to 6 items (2 of each).
            int itemsPlaced = 0;
            int maxItems = 20;
            // Loop over all the chests
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                {
                    continue;
                }
                Tile chestTile = Main.tile[chest.x, chest.y];
                // We need to check if the current chest is the Frozen Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Frozen Chest.
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Frozen Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Frozen_Chest
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 17 * 36)
                {
                    // We have found a Frozen Chest
                    // If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
                    if (WorldGen.genRand.NextBool(3))
                        continue;
                    // Next we need to find the first empty slot for our item
                    for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            // Place the item
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInWoodChests[itemsToPlaceInWoodChestsChoice]);
                            // Decide on the next item that will be placed.
                            chest.item[inventoryIndex].stack = WorldGen.genRand.Next(1, 1);

                            itemsToPlaceInWoodChestsChoice = (itemsToPlaceInWoodChestsChoice + 1) % itemsToPlaceInWoodChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInFrozenChests));
                            itemsPlaced++;
                            break;
                        }
                    }
                }
                // Once we've placed as many items as we wanted, break out of the loop
                if (itemsPlaced >= maxItems)
                {
                    break;
                }
            }
        }
    }
}