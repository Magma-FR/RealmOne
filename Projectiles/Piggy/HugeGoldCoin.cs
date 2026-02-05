using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Piggy
{
    public class HugeGoldCoin : ModProjectile
    {
        private static Asset<Texture2D> coin;

        public override string Texture => Helper.Empty;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(232, 222, 0, 0) * Projectile.Opacity;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.light = 0.2f;
            Projectile.timeLeft = 100;
            Projectile.penetrate = 1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.scale *= 4f;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.position, 2, 2, DustID.GoldCoin);
            Main.dust[dust].scale = 0.1f;

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Main.LocalPlayer.GetModPlayer<Screenshake>().ScreenShake = 45;
            for (int i = 0; i < 300; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.GoldCoin, speed * 50, Scale: 1.5f);
                dust1.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.Item119, Projectile.position);
        }

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            coin = Request<Texture2D>("RealmOne/Assets/Effects/Sunny");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            coin = null;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(coin.Value, Projectile.Center - Main.screenPosition,
                          coin.Value.Bounds, Color.LightYellow, Projectile.rotation,
                          coin.Size() * 0.5f, 0.5f, SpriteEffects.None, 0);
            return true;
        }
    }
}