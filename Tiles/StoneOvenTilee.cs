using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace RealmOne.Tiles
{
    public class StoneOvenTilee : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileTable[Type] = true;
            Main.tileSolidTop[Type] = false;

            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

            DustType = DustID.Torch;
            AdjTiles = new int[] { TileID.Furnaces };

            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.newTile.StyleHorizontal = true;

            TileObjectData.addTile(Type);

            // Etc
            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Stone Oven");
            AddMapEntry(new Color(200, 200, 200), name);
            //   AnimationFrameHeight = 38;
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (Main.gamePaused || !Main.instance.IsActive)
            {
                return;
            }
            if (!Lighting.UpdateEveryFrame || new FastRandom(Main.TileFrameSeed).WithModifier(i, j).Next(4) == 0)
            {
                Tile tile = Main.tile[i, j];
                // Only emit dust from the top tiles, and only if toggled on. This logic limits dust spawning under different conditions.
                if (tile.TileFrameY == 0 && Main.rand.NextBool(3) && ((Main.drawToScreen && Main.rand.NextBool(8)) || !Main.drawToScreen))
                {
                    Dust dust = Dust.NewDustDirect(new Vector2(i * 16 + 2, j * 16 - 4), 4, 8, DustID.Smoke, 0f, 0f, 100);
                    if (tile.TileFrameX == 0)
                        dust.position.X += Main.rand.Next(8);

                    if (tile.TileFrameX == 36)
                        dust.position.X -= Main.rand.Next(8);

                    dust.alpha += Main.rand.Next(100);
                    dust.velocity *= 0.2f;
                    dust.velocity.Y -= 0.5f + Main.rand.Next(10) * 0.1f;
                    dust.fadeIn = 0.5f + Main.rand.Next(10) * 0.1f;

                }
                if (tile.TileFrameY == 0 && Main.rand.NextBool(30) && ((Main.drawToScreen && Main.rand.NextBool(30)) || !Main.drawToScreen))
                {

                    Dust dust1 = Dust.NewDustDirect(new Vector2(i * 16 + 2, j * 16 - 4), 4, 8, DustID.Torch, 0f, 0f, 30);
                    if (tile.TileFrameX == 0)
                        dust1.position.X += Main.rand.Next(8);

                    if (tile.TileFrameX == 36)
                        dust1.position.X -= Main.rand.Next(8);

                    dust1.alpha += Main.rand.Next(100);
                    dust1.velocity *= 0.05f;
                    dust1.velocity.Y -= 0.5f + Main.rand.Next(10) * 0.1f;
                    dust1.fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    dust1.scale = 0.5f;
                }
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 1;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/StoneOvenTilee_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), new Color(150, 150, 150, 100), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Tile t = Main.tile[i, j];
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Framing.GetTileSafely(i, j);

            if (tile.TileFrameX == 18 && tile.TileFrameY == 18 * 2)
            {
                float sin = 1.1f + (float)System.Math.Sin(Main.GameUpdateCount * 0.04f) * (float)System.Math.Cos(Main.GameUpdateCount * 0.065f) * 0.15f;
                (r, g, b) = (1f * sin, 0.65f * sin, 0.4f * sin);
            }
        }

        //  public override void AnimateTile(ref int frame, ref int frameCounter)
        //  {
        // We can change frames manually, but since we are just simulating a different tile, we can just use the same value
        //    frame = Main.tileFrame[TileID.GoldGrasshopperCage];
        //  }
    }
}