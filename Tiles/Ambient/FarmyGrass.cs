using Microsoft.Xna.Framework;
using RealmOne.NPCs.Critters.Farm;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles.Ambient
{
    public class FarmyGrass : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileMergeDirt[Type] = true;

            DustType = DustID.Plantera_Green;
            HitSound = SoundID.Grass;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 142, 68));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = 2;

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
            => offsetY = 2;

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tileBelow = Framing.GetTileSafely(i, j + 2);
            if (!tileBelow.HasTile || tileBelow.IsHalfBlock || tileBelow.TopSlope)
            {
                WorldGen.KillTile(i, j);
            }

            return true;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            if (Main.rand.NextBool(14))
            {
                NPC.NewNPC(new EntitySource_TileBreak(i, j), i * 16, j * 16, ModContent.NPCType<OldSnail>(), 32);
            }
            if (Main.rand.NextBool(12))
            {
                NPC.NewNPC(new EntitySource_TileBreak(i, j), (int)i * 16 + 8, (int)j * 16 + 16, ModContent.NPCType<NPCs.Critters.Squirm>(), 0, 2, 1, 0, 0, Main.myPlayer);
            }
        }
    }
}