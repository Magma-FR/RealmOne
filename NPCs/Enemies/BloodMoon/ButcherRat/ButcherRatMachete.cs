using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat
{
    public class ButcherRatMachete : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public ref float Timer => ref Projectile.ai[0];

        public bool StickingToTile
        {
            get => Projectile.ai[1] == 1f;
            set => Projectile.ai[1] = value ? 1f : 0f;
        }

        public override void SetDefaults()
        {
            Projectile.ignoreWater = true;
            Projectile.hostile = true;

            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            AIType = -1;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.1f);
                Color color = new Color(255, 32, 99) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
        public override void AI()
        {
            const float MinimumTime = 10f;

            if (Projectile.timeLeft < 255 / 25)
            {
                Projectile.alpha += 25;
            }

            UpdateTileStick();

            if (StickingToTile)
            {
                return;
            }

            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += Projectile.velocity.X * 0.05f;

            Dust.NewDust(Projectile.Center, 0, 0, DustID.Blood);

            if (Timer++ < MinimumTime)
            {
                return;
            }

            Projectile.velocity.Y += 0.2f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (StickingToTile)
            {
                return false;
            }

            SoundEngine.PlaySound(in SoundID.Dig, Projectile.Center);
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

            StickingToTile = true;

            NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, Projectile.whoAmI);

            return false;
        }

        private void UpdateTileStick()
        {
            if (!StickingToTile)
            {
                return;
            }

            Projectile.velocity *= 0.5f;
        }

    }
}