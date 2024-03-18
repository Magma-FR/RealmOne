using Microsoft.Xna.Framework;
using RealmOne.Tiles.Blocks;
using StructureHelper;
using System.Collections.Generic;
using Terraria;

//using StructureHelper;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace RealmOne.Biomes.GenPasses
{
    public class AbandondedFarmGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            if (genIndex != -1)
                tasks.Insert(genIndex - 1, new PassLegacy("Abandoned Farm", FarmBiomeGen));
        }

        private int startX;

        public void FarmBiomeGen(GenerationProgress progress, GameConfiguration config)
        {
            startX = (Main.maxTilesX / 2) + WorldGen.genRand.Next(150, 250);
            int endX = startX + WorldGen.genRand.Next(200, 250) + Main.maxTilesY / 200;
            int attempts = 0;
            bool validLocation = false;
            for (int i = startX; i < endX; i++)
            {
                int y = (int)(Main.worldSurface * 0.5f);
                while (y < Main.worldSurface)
                {
                    if (WorldGen.SolidTile(i, y))
                    {
                        if (i == startX)
                        {
                            Dictionary<ushort, int> dictionary = new Dictionary<ushort, int>();
                            WorldUtils.Gen(new Point(startX, y + 15), new Shapes.Rectangle(endX - startX, 30), new Actions.TileScanner(TileID.Dirt, TileID.Cloud).Output(dictionary));
                            int dirtCount = dictionary[TileID.Dirt];
                            int cloudCount = dictionary[TileID.Cloud];
                            if (dirtCount > endX * 30 / 3 && cloudCount == 0)
                            {
                                validLocation = true;
                            }
                            else if (cloudCount != 0)
                            {
                                y++;
                            }
                        }
                        if (validLocation || attempts >= 40)
                        {
                            WorldGen.EmptyLiquid(i, y);
                            WorldGen.TileRunner(i, y, WorldGen.genRand.Next(35, 45), WorldGen.genRand.Next(10, 15), ModContent.TileType<FarmSoil>());
                        }
                        else
                        {
                            attempts++;
                            endX--;
                            startX--;
                            i = startX;
                        }
                        break;
                    }
                    y++;
                }
            }
        }

        public override void PostWorldGen()
        {
            int barnStartX = startX + WorldGen.genRand.Next(10, 25);
            int y = 0;
            bool validLocation = false;
            int barnWidth = 83;
            int barnHeight = 41;
            while (y < Main.worldSurface)
            {
                y++;
                if (WorldGen.SolidTile(barnStartX, y) && Framing.GetTileSafely(barnStartX, y).TileType == ModContent.TileType<FarmSoil>())
                {
                    validLocation = true;
                    break;
                }
            }
            if (validLocation)
            {
                for (int i = barnStartX; i < barnStartX + barnWidth; i++)
                {
                    WorldGen.TileRunner(i, y + 7, 12, 10, ModContent.TileType<FarmSoil>(), true);
                }
                Generator.GenerateStructure("Structures/Barn1", new Point16(barnStartX, y - barnHeight), Mod, false);
            }
        }
    }
}