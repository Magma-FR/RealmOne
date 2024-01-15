using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Projectiles.Other
{
    public class Starbang : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pristine Starbang");
        }

        public override string Texture => Helper.Empty;

        //  private Vector2 flashoffset = Vector2.Zero;

        // private Player Owner => Main.player[Projectile.owner];

        //  private bool FullyUsed = false;

        private static Asset<Texture2D> Star;

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 5;
            Projectile.friendly = false;
            Projectile.aiStyle = 0;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Lighting.AddLight(Projectile.Center, r: 0.3f, g: 0.6f, 0.1f);
            //  Projectile.rotation = Projectile.ai[0];
            //   if (!FullyUsed)
            //  {
            //      FullyUsed = true;
            //   flashoffset = Projectile.Center - Owner.Center;
            //}
            //     Projectile.rotation = player.DirectionTo(Main.MouseWorld).ToRotation;
            //   Projectile.rotation = player.DirectionTo(Main.MouseWorld).ToRotation();

            //  Projectile.Center = Owner.Center + flashoffset;

            int radius = 50;

            // Damage enemies within the splash radius
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC target = Main.npc[i];
                if (target.active && !target.friendly && Vector2.Distance(Projectile.Center, target.Center) < radius)
                {
                    target.SimpleStrikeNPC(0, 0);
                }
            }
        }

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            Star = Request<Texture2D>("RealmOne/Assets/Effects/LightBang");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            Star = null;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(Star.Value, Projectile.Center - Main.screenPosition,
                          Star.Value.Bounds, Color.LightYellow, Projectile.rotation,
                          Star.Size() * 0.5f, 0.5f, SpriteEffects.None, 0);
            return true;
        }
    }
}