using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
namespace RealmOne.Projectiles.Bullet
{

    public class BlueFlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Muzzle Flash");
        }
        private Vector2 flashoffset = Vector2.Zero;



        private Player Owner => Main.player[Projectile.owner];

        private bool FullyUsed = false;

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.damage = 25;
            Projectile.height = 80;
            Projectile.timeLeft = 4;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.tileCollide = false;

        }

        public override void AI()
        {

            Player player = Main.player[Projectile.owner];

            Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3() * 1f);
            Projectile.rotation = Projectile.ai[0];
            if (!FullyUsed)
            {
                FullyUsed = true;
                flashoffset = Projectile.Center - Owner.Center;
            }
            //     Projectile.rotation = player.DirectionTo(Main.MouseWorld).ToRotation;
            Projectile.rotation = player.DirectionTo(Main.MouseWorld).ToRotation();

            Projectile.Center = Owner.Center + flashoffset;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D mainTex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(mainTex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(8, mainTex.Height / 2), Projectile.scale, SpriteEffects.None, 0f);

            Texture2D glowTex = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/Cone").Value;
            Color glowColor = Color.LightSkyBlue;
            glowColor.A = 0;
            Main.spriteBatch.Draw(glowTex, Projectile.Center - Main.screenPosition, null, glowColor, Projectile.rotation, new Vector2(8, glowTex.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}

