using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles
{
    public class StoneOvenTilee : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = false;

            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

            DustType = DustID.Torch;
            AdjTiles = new int[] { TileID.Furnaces };

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;

            TileObjectData.addTile(Type);

            // Etc
            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Stone Oven");
            AddMapEntry(new Color(200, 200, 200), name);
            //   AnimationFrameHeight = 38;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = Main.rand.Next(1, 3);
        }

        //  public override void AnimateTile(ref int frame, ref int frameCounter)
        //  {
        // We can change frames manually, but since we are just simulating a different tile, we can just use the same value
        //    frame = Main.tileFrame[TileID.GoldGrasshopperCage];
        //  }
    }
}