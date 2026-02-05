/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.BossBars;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.Piggy;
using RealmOne.RealmPlayer;
using RealmOne.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.MiniBoss
{
    public class MoneyVaseGlobalTile : GlobalTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return true;
        }

        private static int shakeTimer = 0;
        private static int shakeCooldown = 0;
        private static Vector2 currentOffset = Vector2.Zero;

        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            // Only your Money Vase tile
            if (type != ModContent.TileType<PiggyTile>())
                return;

            // Stop once boss is defeated
            if (DownedBossSystem.downedPiggy)
                return;

            Player player = Main.LocalPlayer;

            Tile tile = Main.tile[i, j];
            if (tile == null || !tile.HasTile)
                return;

            // World position
            Vector2 tileWorldPos = new Vector2(i * 16, j * 16);

            // Proximity check
            if (Vector2.Distance(player.Center, tileWorldPos) > 160f)
                return;

            // Rare twitch
            if (shakeCooldown > 0)
            {
                shakeCooldown--;
                return;
            }

            // Start a shake
            if (shakeTimer <= 0 && Main.rand.NextBool(90))
            {
                shakeTimer = 8; // frames of shaking
                shakeCooldown = 120; // delay before next shake
                currentOffset = new Vector2(Main.rand.Next(-1, 2), 0);
            }

            // Apply shake
            if (shakeTimer > 0)
            {
                shakeTimer--;
            }
            else
            {
                return;
            }

            // Micro shake
            Vector2 shakeOffset = new Vector2(Main.rand.Next(-1, 2), 0);

            // Correct frame data
            Rectangle frame = new Rectangle(
                tile.TileFrameX,
                tile.TileFrameY,
                16,
                16
            );

            Texture2D texture = TextureAssets.Tile[type].Value;

            spriteBatch.Draw(
            texture,
             tileWorldPos - Main.screenPosition + currentOffset,
            frame,
            new Color(255, 255, 255, 120),
            0f,
           Vector2.Zero,
             1f,
            SpriteEffects.None,
            0f
 );
        }
    }
}*/