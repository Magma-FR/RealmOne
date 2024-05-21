using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Sets.OrchidSet;
using System;
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
            Main.tileAxe[Type] = true;

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
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<OrchidEssence>(), Main.rand.Next(6, 10));
            }
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            Color colour = Color.White;

            Texture2D mask = ModContent.Request<Texture2D>("RealmOne/Tiles/PlantTiles/OrchidTree_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

            spriteBatch.Draw(mask, new Vector2(i * 16, j * 16) - Main.screenPosition + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), colour);
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