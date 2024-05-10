using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ObjectData;
using RealmOne.Items.Sets.OrchidSet;
using Terraria.DataStructures;
using Terraria.Enums;

namespace RealmOne.Tiles.Ambient
{
    public class WoodLog1 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(186, 149, 85));
            AdjTiles = new int[] { 93 };
            TileID.Sets.BreakableWhenPlacing[Type] = true;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(1))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ItemID.Wood, Main.rand.Next(3, 5));
            }
            return false;
        }
    }

    public class WoodLog2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(186, 149, 85));
            AdjTiles = new int[] { 93 };
            TileID.Sets.BreakableWhenPlacing[Type] = true;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(1))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ItemID.Wood, Main.rand.Next(3, 5));
            }
            return false;
        }
    }

    public class WoodLog3 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(186, 149, 85));
            AdjTiles = new int[] { 93 };
            TileID.Sets.BreakableWhenPlacing[Type] = true;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(1))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ItemID.Mushroom, Main.rand.Next(1, 2));
            }
            return false;
        }
    }

    public class DesertVase1 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(186, 149, 85));
            AdjTiles = new int[] { 93 };
            TileID.Sets.BreakableWhenPlacing[Type] = true;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(1))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ItemID.Sandstone, Main.rand.Next(1, 2));
            }
            return false;
        }
    }

    public class DesertVase2 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            //   TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };

            TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(186, 149, 85));
            AdjTiles = new int[] { 93 };
            TileID.Sets.BreakableWhenPlacing[Type] = true;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(1))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ItemID.Sandstone, Main.rand.Next(1, 2));
            }
            return false;
        }
    }
}