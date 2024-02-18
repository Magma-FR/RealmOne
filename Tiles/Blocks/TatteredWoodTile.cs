using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Tiles.Blocks
{
    public class TatteredWoodTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.AllTiles[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            DustType = DustID.BorealWood;

            HitSound = SoundID.Dig;

            MineResist = 1f;
            MinPick = 30;
            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Tattered Wood");
            AddMapEntry(new Color(160, 80, 80), name);


        }



    }
}