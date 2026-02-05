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
            int forestIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Piles"));

            if (forestIndex != -1)
            {
                tasks.Insert(forestIndex + 1, new ForestAmbient("Ambients", 100f));
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

                int[] tileTypes = new int[] { TileType<WoodLog1>(), TileType<WoodLog2>(), TileType<WoodLog3>(), TileType<OrchidTree>() };

                // To not be annoying, we'll only spawn 15 Example Rubble near the spawn point.
                // This example uses the Try Until Success approach: https://github.com/tModLoader/tModLoader/wiki/World-Generation#try-until-success
                for (int k = 0; k < 25; k++)
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
                        int x = WorldGen.genRand.Next(Main.maxTilesX / 2 - 200, Main.maxTilesX / 2 + 200);
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

        public override void PostAddRecipes()
        {
            RemoveSpookyArmorRecipes();
            AddModifiedSpookyArmorRecipes();
        }

        private void RemoveSpookyArmorRecipes()
        {
            for (int i = Recipe.numRecipes - 1; i >= 0; i--)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.createItem.type == ItemID.SpookyHelmet ||
                    recipe.createItem.type == ItemID.SpookyBreastplate ||
                    recipe.createItem.type == ItemID.SpookyLeggings)
                {
                    recipe.DisableRecipe();
                }
            }
        }

        private static void AddModifiedSpookyArmorRecipes()
        {
            // Helmet
            Recipe.Create(ItemID.SpookyHelmet)
                .AddIngredient(ItemID.SpookyWood, 150)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            // Breastplate
            Recipe.Create(ItemID.SpookyBreastplate)
                .AddIngredient(ItemID.SpookyWood, 250)
                .AddIngredient(ItemID.Ectoplasm, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            // Leggings
            Recipe.Create(ItemID.SpookyLeggings)
                .AddIngredient(ItemID.SpookyWood, 200)
                .AddIngredient(ItemID.Ectoplasm, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void PostWorldGen()
        {
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

                            Wchest.item[WinventoryIndex].stack = WorldGen.genRand.Next(15, 25);

                            waterchestchoice = (waterchestchoice + 1) % waterchest.Length;
                            //Wchest.item[WinventoryIndex].SetDefaults(Main.rand.Next(WinventoryIndex));
                            break;
                        }
                    }
                }
            }
        }
    }
}