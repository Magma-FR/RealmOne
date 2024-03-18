using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles
{
    public class TatteredBarrel : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileSpelunker[Type] = true;
            Main.tileContainer[Type] = true;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 1200;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;

            MineResist = 3f;
            MinPick = 20;

            DustType = DustID.WoodFurniture;
            AdjTiles = new int[] { TileID.Containers };
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.BasicChest[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;

            // Names

            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Tattered Barrel");
            AddMapEntry(new Color(200, 200, 200), name);

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };

            TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
        }

        public override ushort GetMapOption(int i, int j)
        {
            return (ushort)(Main.tile[i, j].TileFrameX / 36);
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }

        public override bool CanDrop(int i, int j)
        {
            if (Main.rand.NextBool(2))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<GoopyGrass>(), Main.rand.Next(2, 6));

            }

            if (Main.rand.NextBool(20))
            {
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<Wheat>(), Main.rand.Next(2, 6));

            }
            // Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<FarmKey>(), Main.rand.Next(1, 1), Main.rand.NextBool(60));
            // Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<Wheat>(), Main.rand.Next(3, 12), Main.rand.NextBool(2));
            //     Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<SuperWheat>(), Main.rand.Next(1, 1), Main.rand.NextBool(80));
            //    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<RustedBullets>(), Main.rand.Next(10, 15), Main.rand.NextBool(2));

            //     Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ItemID.Hay, Main.rand.Next(4, 7), Main.rand.NextBool(3));

            //        Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<TatteredBarrelItem>(), 0);

            return false;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            //       Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 48, 32, ModContent.ItemType<BrassNuggets>(), Main.rand.Next(10, 10));
            //        Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 48, 32, ItemID.Wood, Main.rand.Next(25, 30));
            //       Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 48, 32, ModContent.ItemType<GoopyGrass>(), Main.rand.Next(10, 30));

            //          Chest.DestroyChest(x, y);
            SoundEngine.PlaySound(rorAudio.BrokenBarrel);
            if (Main.netMode != NetmodeID.Server)
            {
                int BGore1 = Mod.Find<ModGore>("BarrelGore1").Type;
                int BGore2 = Mod.Find<ModGore>("BarrelGore2").Type;
                int BGore3 = Mod.Find<ModGore>("BarrelGore3").Type;

                var entitySource = new EntitySource_TileBreak(x, y);

                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers

                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(entitySource, new Vector2(x * 16, y * 16), new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BGore1);
                    Gore.NewGore(entitySource, new Vector2(x * 16, y * 16), new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BGore2);
                    Gore.NewGore(entitySource, new Vector2(x * 16, y * 16), new Vector2(Main.rand.Next(-3, 7), Main.rand.Next(-3, 7)), BGore3);
                }
            }

            if (Main.netMode != NetmodeID.Server)
            {
                CombatText.NewText(new Rectangle(Main.tile.Width + 30, Main.tile.Height - 20, Main.tile.Height - 10, Main.tile.Height + 10), new Color(234, 129, 178, 190), "It won't budge!!", false, false);
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;

            if (Main.netMode != NetmodeID.Server)

                CombatText.NewText(new Rectangle(Main.tile.Width + 30, Main.tile.Height - 20, Main.tile.Height - 20, Main.tile.Height + 30), new Color(234, 129, 178, 190), "It won't budge!!", false, false);
            if (tile.TileFrameX % 36 != 0)
            {
                left--;
            }

            if (tile.TileFrameY != 0)
            {
                top--;
            }

            int chest = Chest.FindChest(left, top);
            player.cursorItemIconID = -1;
            if (chest < 0)
            {
                player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
            }
            else
            {
                player.cursorItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Tattered Barrel";
                if (player.cursorItemIconText == "Fr")
                {
                    player.cursorItemIconID = CursorOverrideID.FavoriteStar;
                    player.cursorItemIconText = "What's This?";
                }
            }

            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.cursorItemIconText == "What's this?")
            {
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = 0;
            }
        }
    }
}