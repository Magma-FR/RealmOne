using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Bullet
{
    public class Heller : ModProjectile
    {
        private static Asset<Texture2D> Flamer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Spray");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.hostile = false;

            Projectile.width = 40;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -2;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = true;
            Projectile.damage = 24;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 10;
            Projectile.scale = 3f;
            Projectile.light = 2f;
        }

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            Flamer = Request<Texture2D>("RealmOne/Assets/Effects/FlameEffect");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            Flamer = null;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(Flamer.Value, Projectile.Center - Main.screenPosition,
                         Flamer.Value.Bounds, Color.DarkOrange, Projectile.rotation,
                          Flamer.Size() * 0.25f, 1f, SpriteEffects.None, 0);
            return true;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 3f)//the amount of ticks the game spends on each frame
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.2f);
            Lighting.Brightness(1, 1);
        }
    }
}