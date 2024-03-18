using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Bullet
{
    public class ConeFlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Muzzle Flash");
        }

        private Vector2 flashoffset = Vector2.Zero;

        public override string Texture => Helper.Empty;

        private Player Owner => Main.player[Projectile.owner];

        private bool FullyUsed = false;

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.damage = 0;
            Projectile.height = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 4;
            Projectile.friendly = false;
            Projectile.aiStyle = 0;
            Projectile.alpha = 255;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.8f);
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
            Color glowColor = Color.NavajoWhite;
            glowColor.A = 0;
            Main.spriteBatch.Draw(glowTex, Projectile.Center - Main.screenPosition, null, glowColor, Projectile.rotation, new Vector2(8, glowTex.Height / 2), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}