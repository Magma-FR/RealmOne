using RealmOne.Common.Core;
using RealmOne.Common.Systems.GenPasses;
using RealmOne.Items.Accessories;
using RealmOne.Items.Food;
using RealmOne.Items.Misc.Plants;
using RealmOne.Items.Opens;
using RealmOne.Items.Weapons.PreHM.Classless;
using RealmOne.Items.Weapons.PreHM.Grenades;
using RealmOne.Items.Weapons.PreHM.Throwing;
using RealmOne.NPCs.Critters;
using RealmOne.NPCs.Enemies.Underground;
using RealmOne.Tiles;
using RealmOne.Tiles.Blocks;
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
    public static class BiomePlayer
    {
    }

    public class TileDrops : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (!Main.dedServ)
            {
                Player player = Main.LocalPlayer;

                if (type == 3 && Main.rand.NextBool(90))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemType<RegenMush>(), 1);
                }

                if (type == TileID.Sunflower && Main.rand.NextBool(2))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ItemType<SunflowerPetal>(), 3);
                }

                if (type == TileID.Trees && Main.rand.NextBool(12) && player.ZoneCorrupt)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<CursedBerries>(), Main.rand.Next(1, 2));
                }
                if (type == TileID.Trees && Main.rand.NextBool(12) && player.ZoneCrimson)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<Goreberry>(), Main.rand.Next(1, 2));
                }

                if (type == 12 && Main.rand.NextBool(7))
                {
                    NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, NPCType<HeartBat>(), 1);
                }

                if (type == TileID.Cactus && Main.rand.NextBool(70))
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 48, ItemType<CactusFruit>(), Main.rand.Next(1, 1));
                }

                /*if (type == TileID.Dirt && DownedBossSystem.downedSquirmo == false && Main.rand.NextBool(20))
                {
                    NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, NPCType<Squirm>(), 1);
                }

                if (type == ModContent.TileType<FarmSoil>() && DownedBossSystem.downedSquirmo == false && Main.rand.NextBool(12))
                {
                    NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, NPCType<Squirm>(), 1);
                }*/
            }
        }
    }

    /* public sealed class SourceDependentItemTweaks : GlobalItem
      {
           public override void OnSpawn(Item item, IEntitySource source)
           {
               if (source is EntitySource_ShakeTree)
               {
                   IEntitySource newSource = item.GetSource_FromThis(); // Use a separate source for the newly created projectiles, to not cause a stack overflow.

                  if (Main.dayTime == true)
                  {
                      NPC.NewNPC(newSource, (int)item.position.X, (int)item.position.Y, NPCType<AcornSprinter>());
                  }
               }
           }

           */

    public class WorldSystem : ModSystem
    {
        /*    public class Test : GenPass
            {
                public Test(string name, double loadWeight) : base(name, loadWeight)
                {
                }

                protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
                {
                    int x = (int)(GenVars.worldSurfaceLow + GenVars.worldSurfaceLow / 2);
                    int y = (int)(GenVars.worldSurfaceLow + GenVars.worldSurfaceLow / 2);
                    Point16 point = new Point16(x, y);
                    Generator.GenerateStructure("Structures/Test", point, RealmOne.Instance, false);
                }
            }*/
       
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Shinies"));
            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, (GenPass)(object)new OldGoldOreNameGenPass("OldGoldOreNameGenPass", 320f));
            }
            /*  int shiniesIndex2 = tasks.FindIndex((GenPass genpass1) => genpass1.Name.Equals("Shinies"));
              if (shiniesIndex2 != -1)
              {
                  tasks.Insert(shiniesIndex2 + 1, (GenPass)(object)new FlorenceMarbleOreNameGenPass("FlorenceMarbleOreNameGenPass", 320f));
              }*/
            int forestIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Ambient"));

            if (forestIndex != -1)
            {
                tasks.Insert(forestIndex + 1, new ForestAmbient("Ambients", 130f));
            }

        }

        public class ForestAmbient : GenPass
        {
            public ForestAmbient(string name, float loadWeight) : base(name, loadWeight)
            {
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Ambients";

                int[] tileTypes = new int[] { ModContent.TileType<TatteredBarrel>() };

                // To not be annoying, we'll only spawn 15 Example Rubble near the spawn point.
                // This example uses the Try Until Success approach: https://github.com/tModLoader/tModLoader/wiki/World-Generation#try-until-success
                for (int k = 0; k < 15; k++)
                {
                    bool success = false;
                    int attempts = 0;

                    while (!success)
                    {
                        attempts++;
                        if (attempts > 1000)
                        {
                            break;
                        }
                        int x = WorldGen.genRand.Next(Main.maxTilesX / 2 - 40, Main.maxTilesX / 2 + 40);
                        int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, (int)GenVars.worldSurfaceHigh);
                        int tileType = WorldGen.genRand.Next(tileTypes);
                        if (Main.tile[x, y].TileType == tileType)
                        {
                            continue;
                        }

                        WorldGen.PlaceTile(x, y, tileType, mute: true);
                        success = Main.tile[x, y].TileType == tileType;
                    }
                }
            }
        }
        public override void PostWorldGen()
        {
            int[] goldenchest = { ItemType<MinersPouch>() };
            int goldenchestchoice = 0;

            for (int gchestIndex = 0; gchestIndex < 1000; gchestIndex++)
            {
                Chest gchest = Main.chest[gchestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (gchest != null && Main.tile[gchest.x, gchest.y].TileType == TileID.Containers && Main.tile[gchest.x, gchest.y].TileFrameX == 1 * 36)
                {
                    for (int ginventoryIndex = 0; ginventoryIndex < 40; ginventoryIndex++)
                    {
                        if (gchest.item[ginventoryIndex].type == ItemID.None)
                        {
                            gchest.item[ginventoryIndex].SetDefaults(goldenchest[goldenchestchoice]);
                            goldenchestchoice = (goldenchestchoice + 1) % goldenchest.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
            int[] acornchest = { ItemType<AcornGrenade>() };
            int acornchestchoice = 0;

            for (int acornchestIndex = 0; acornchestIndex < 1000; acornchestIndex++)
            {
                Chest acChest = Main.chest[acornchestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (acChest != null && Main.tile[acChest.x, acChest.y].TileType == TileID.Containers && Main.tile[acChest.x, acChest.y].TileFrameX == 1 * 36)
                {
                    for (int acorninventory = 0; acorninventory < 40; acorninventory++)
                    {
                        if (acChest.item[acorninventory].type == ItemID.None)
                        {
                            acChest.item[acorninventory].SetDefaults(acornchest[acornchestchoice]);
                            acChest.item[acorninventory].stack = WorldGen.genRand.Next(10, 18);

                            acornchestchoice = (acornchestchoice + 1) % acornchest.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
            int[] waterchest = { ItemType<EleJelly>() };
            int waterchestchoice = 0;
            for (int WchestIndex = 0; WchestIndex < 1000; WchestIndex++)

            {
                Chest Wchest = Main.chest[WchestIndex];
                if (Wchest != null && Main.tile[Wchest.x, Wchest.y].TileType == TileID.Containers && Main.tile[Wchest.x, Wchest.y].TileFrameX == 17 * 36)
                {
                    for (int WinventoryIndex = 0; WinventoryIndex < 40; WinventoryIndex++)
                    {
                        if (Wchest.item[WinventoryIndex].type == ItemID.None)
                        {
                            Wchest.item[WinventoryIndex].SetDefaults(waterchest[waterchestchoice]);

                            Wchest.item[WinventoryIndex].stack = WorldGen.genRand.Next(20, 30);

                            waterchestchoice = (waterchestchoice + 1) % waterchest.Length;
                            //Wchest.item[WinventoryIndex].SetDefaults(Main.rand.Next(WinventoryIndex));
                            break;
                        }
                    }
                }
            }

            int[] pot = { ItemType<StackPotions>() };
            int potchoice = 0;
            for (int PchestIndex = 0; PchestIndex < 1000; PchestIndex++)

            {
                Chest Pchest = Main.chest[PchestIndex];
                if (Pchest != null && Main.tile[Pchest.x, Pchest.y].TileType == TileID.Containers && Main.tile[Pchest.x, Pchest.y].TileFrameX == 1 * 36)
                {
                    for (int PinventoryIndex = 0; PinventoryIndex < 40; PinventoryIndex++)
                    {
                        if (Pchest.item[PinventoryIndex].type == ItemID.None)
                        {
                            Pchest.item[PinventoryIndex].SetDefaults(pot[potchoice]);

                            Pchest.item[PinventoryIndex].stack = WorldGen.genRand.Next(2, 5);

                            potchoice = (potchoice + 1) % pot.Length;
                            //chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests;
                            break;
                        }
                    }
                }
            }

            int[] itemsToPlaceInIceChests1 = { ItemType<MinersPouch>() };
            int itemsToPlaceInIceChestsChoice1 = 0;
            for (int chestIndex1 = 0; chestIndex1 < 1000; chestIndex1++)
            {
                Chest chest1 = Main.chest[chestIndex1];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (chest1 != null && Main.tile[chest1.x, chest1.y].TileType == TileID.Containers && Main.tile[chest1.x, chest1.y].TileFrameX == 11 * 36)
                {
                    for (int inventoryIndex1 = 0; inventoryIndex1 < 40; inventoryIndex1++)
                    {
                        if (chest1.item[inventoryIndex1].type == ItemID.None)
                        {
                            chest1.item[inventoryIndex1].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests1));
                            itemsToPlaceInIceChestsChoice1 = (itemsToPlaceInIceChestsChoice1 + 1) % itemsToPlaceInIceChests1.Length;
                            //Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }
        }
    }
}