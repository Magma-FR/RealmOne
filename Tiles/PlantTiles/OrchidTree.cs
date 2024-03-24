using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Sets.OrchidSet;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles.PlantTiles
{
    public class OrchidTree : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileCut[Type] = false;
            Main.tileLavaDeath[Type] = true;
            HitSound = rorAudio.TreeHurt;
            DustType = DustID.Grass;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 9;
            TileObjectData.newTile.Width = 7;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(80, 230, 150), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(1))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<OrchidEssence>(), Main.rand.Next(2, 6));
            }
            return false;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Tile tile = Framing.GetTileSafely(i, j);

            if (Main.netMode != NetmodeID.Server)
            {
                SoundEngine.PlaySound(rorAudio.TreeKill);
            }
        }
    }
}