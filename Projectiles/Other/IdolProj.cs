using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Other
{
    public class IdolProj : ModProjectile
    {
        private static Asset<Texture2D> Idol;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cthulhu Medallion");
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;

            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.light = 3.5f;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 380;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.penetrate = 5;
            Projectile.stepSpeed = 1f;
            Projectile.alpha = 255;
            Projectile.scale = 1f;
        }

        public override void AI()
        {
            Projectile.rotation += 0.05f;
            Projectile.velocity.X *= 0.0f;
            Projectile.velocity.Y *= 0.09f;
        }

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            Idol = Request<Texture2D>("RealmOne/Assets/Effects/Idol");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            Idol = null;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(Idol.Value, Projectile.Center - Main.screenPosition,
            Idol.Value.Bounds, Color.NavajoWhite, Projectile.rotation,
                          Idol.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            return true;
        }
    }
}