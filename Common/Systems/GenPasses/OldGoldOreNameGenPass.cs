using RealmOne.Tiles.Blocks;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace RealmOne.Common.Systems.GenPasses;

internal class OldGoldOreNameGenPass : GenPass
{
    public OldGoldOreNameGenPass(string name, float loadWeight)
        : base(name, loadWeight)
    {
    }

    protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
    {
        progress.Message = "Spawning an ancient and pristine ore";
        for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); i++)
        {
            int num = WorldGen.genRand.Next(0, Main.maxTilesX);
            int y = WorldGen.genRand.Next((int)GenVars.worldSurface, Main.maxTilesY);
            WorldGen.TileRunner(num, y, WorldGen.genRand.Next(3, 4), WorldGen.genRand.Next(1, 4), ModContent.TileType<OldGoldOreT>(), false, 0f, 0f, false, true, -1);
        }
    }
}