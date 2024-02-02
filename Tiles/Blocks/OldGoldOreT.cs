using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Tiles.Blocks
{
    internal class OldGoldOreT : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 350;

            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Old Gold Ore");
            AddMapEntry(new Color(243, 255, 5), name);

            DustType = DustID.Gold;

            HitSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/OldGoldTink");
            MineResist = 1.5f;
            MinPick = 60;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
            => NPC.downedBoss1;

        public override bool CanExplode(int i, int j)
            => NPC.downedBoss1;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.13f;
            g = 0.11f;
            b = 0.08f;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.Slope == 0 && !tile.IsHalfBlock)
            {
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }
                Main.spriteBatch.Draw(Mod.Assets.Request<Texture2D>("Tiles/Blocks/OldGoldOreT_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), new Color(255, 240, 148, 200), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}