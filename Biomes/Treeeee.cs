using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Biomes
{
    //# Spirit Mod lil helper. All credit goes for them.
    public static class Treeeee
    {
        public static Vector2 GetTreeSize(this ModTree tree, Tile tile)
        {
            int discard = 0;
            int width = 0;
            int height = 0;
            tree.SetTreeFoliageSettings(tile, ref discard, ref discard, ref discard, ref width, ref height);
            return new Vector2(width, height);
        }

        public static Vector2 GetRandomTreePosition(this ModTree tree, Tile tile)
        {
            var size = GetTreeSize(tree, tile);
            var halfSize = size / 2f;
            var offset = new Vector2(Main.rand.NextFloat(-halfSize.X, halfSize.X), -Main.rand.NextFloat(size.Y * 0.1f, size.Y * 0.8f));
            return offset;
        }
    }
}