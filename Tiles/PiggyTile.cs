using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.BossSummons;
using RealmOne.NPCs.Enemies.MiniBoss;
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
    public class PiggyTile : ModTile
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
            HitSound = rorAudio.OldGoldTink;
            TileObjectData.newTile.Origin = new Point16(0, 1);

            DustType = DustID.DungeonPink;
            TileID.Sets.DisableSmartCursor[Type] = true;

            // Names

            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Piggy Vase");
            AddMapEntry(new Color(200, 200, 200), name);

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };

            TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
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
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 64, 32, ModContent.ItemType<MoneyVase>());
            return false;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            NPC.NewNPC(new EntitySource_TileBreak(x, y), x * 16, y * 16, ModContent.NPCType<PossessedPiggy>(), 32);

            Chest.DestroyChest(x, y);
            SoundEngine.PlaySound(SoundID.Shatter);
            if (Main.netMode != NetmodeID.Server)
            {
                int BGore1 = Mod.Find<ModGore>("MoneyVaseGore1").Type;
                int BGore2 = Mod.Find<ModGore>("MoneyVaseGore2").Type;
                int BGore3 = Mod.Find<ModGore>("MoneyVaseGore3").Type;

                var entitySource = new EntitySource_TileBreak(x, y);

                // We don't want Mod.Find<ModGore> to run on servers as it will crash because gores are not loaded on servers

                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(entitySource, new Vector2(x * 16, y * 16), new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BGore1);
                    Gore.NewGore(entitySource, new Vector2(x * 16, y * 16), new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), BGore2);
                    Gore.NewGore(entitySource, new Vector2(x * 16, y * 16), new Vector2(Main.rand.Next(-3, 7), Main.rand.Next(-3, 7)), BGore3);
                }
            }
        }
    }
}