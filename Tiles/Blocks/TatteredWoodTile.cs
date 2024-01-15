using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Tiles.Blocks
{
    internal class TatteredWoodTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.AllTiles[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;

            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Tattered Wood");
            AddMapEntry(new Color(160, 80, 80), name);

            DustType = DustID.BorealWood;

            HitSound = SoundID.Dig;

            MineResist = 1f;
            MinPick = 30;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            SoundEngine.PlaySound(rorAudio.BrokenBarrel);

            int BGore1 = Mod.Find<ModGore>("TWoodGore1").Type;
            int BGore2 = Mod.Find<ModGore>("TWoodGore2").Type;
            int BGore3 = Mod.Find<ModGore>("TWoodGore3").Type;

            var entitySource = new EntitySource_TileBreak(i, j);

            // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers

            for (int k = 0; k < 1; i++)
            {
                Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), BGore1);
                Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), BGore2);
                Gore.NewGore(entitySource, new Vector2(i * 16, j * 16), new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), BGore3);
            }
        }
    }
}