using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles.Furniture
{
    public class ChickenCoopTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLavaDeath[Type] = true;
            //   AnimationFrameHeight = 50;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16 };

            DustType = DustID.WoodFurniture;
            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Chicken Coop");
            AddMapEntry(new Color(50, 30, 150), name);
            TileObjectData.addTile(Type);
        }
    }
}