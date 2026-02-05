using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Items.BossSummons;
using RealmOne.NPCs.Enemies.MiniBoss;
using System;
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

            DustType = DustID.DungeonPink;
            TileID.Sets.DisableSmartCursor[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            //   TileObjectData.newTile.Origin = new Point16(0, 1);
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

        private Vector2 EldritchWobble(int i, int j)
        {
            if (DownedBossSystem.downedPiggy)
                return Vector2.Zero;

            Player player = Main.LocalPlayer;
            Vector2 tileWorldPos = new Vector2(i * 16, j * 16);

            float distance = Vector2.Distance(player.Center, tileWorldPos);
            if (distance > 160f)
                return Vector2.Zero;
            float time = Main.GlobalTimeWrappedHourly;

            // base agitation
            float baseX = (float)Math.Sin(time * 12.3f) * 1.2f;
            float baseY = (float)Math.Sin(time * 15.7f) * 1.2f;

            // slam trigger
            float slamTrigger = (float)Math.Sin(time * 3.1f);

            // choose direction using DIFFERENT phases
            float dirX = Math.Sign(Math.Sin(time * 5.37f));
            float dirY = Math.Sign(Math.Sin(time * 7.91f));

            if (dirX == 0) dirX = 1;
            if (dirY == 0) dirY = 1;

            float slamStrength = slamTrigger > 0.96f ? 10f : 0f;

            Vector2 slam = new Vector2(
                slamStrength * dirX,
                slamStrength * dirY
            );

            return new Vector2(baseX, baseY) + slam;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);

            Texture2D texture = ModContent.Request<Texture2D>(
                Texture,
                ReLogic.Content.AssetRequestMode.ImmediateLoad
            ).Value;

            Vector2 offScreen = Main.drawToScreen
                ? Vector2.Zero
                : new Vector2(Main.offScreenRange);

            Vector2 drawPos =
                new Vector2(i * 16, j * 16)
                - Main.screenPosition
                + offScreen
                + EldritchWobble(i, j);

            spriteBatch.Draw(
                texture,
                drawPos,
                new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16),
                Lighting.GetColor(i, j)
            );

            Tile tile1 = Framing.GetTileSafely(i, j);

            Rectangle frame = new Rectangle(
                tile1.TileFrameX,
                tile1.TileFrameY,
                16,
                16
            );

            float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 6f) * 0.5f + 0.5f;
            Color auraColor = new Color(180, 160, 130, 60) * pulse;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;

                    spriteBatch.Draw(
                        texture,
                        drawPos + new Vector2(x, y),
                        frame,
                        auraColor
                    );
                }
            }

            // 🔑 prevent vanilla draw
            return false;
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