using RealmOne.Tiles.Blocks;
using System;
using Terraria.ModLoader;

namespace RealmOne.Biomes
{
    public class BiomeTileCount : ModSystem
    {
        public int FarmCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            FarmCount = tileCounts[ModContent.TileType<FarmSoil>()];
        }
    }
}