using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne;
using RealmOne.Projectiles.Magic;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Sets.SunflowerSet
{
    public class SpinFlower: ModProjectile
    {
        private static Asset<Texture2D> Sun;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 19;

        }

        public override void SetDefaults()
        {
            Projectile.width = 45;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.penetrate = -2;
            Projectile.timeLeft = 160;
            Projectile.alpha = 0;
            Projectile.tileCollide = false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.MaxMana, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Sunflower);
                Main.dust[d].noGravity = true;
            }

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("SunflowerGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("SunflowerGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("SunflowerGore3").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("SunflowerGore4").Type, 1f);


            for (int i = 0; i < 130; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(2f, 2f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Sunflower, speed * 11, Scale: 2f);
                ;
                d.noGravity = true;
            }

            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, 6, -4, ModContent.ProjectileType<SunPetal>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, -6, -4, ModContent.ProjectileType<SunPetal>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, 6, 4, ModContent.ProjectileType<SunPetal>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, -6, 4, ModContent.ProjectileType<SunPetal>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, r: 0.35f, g: 0.2f, b: 0.05f);
           

            if (++Projectile.frameCounter >= 20f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

      
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.    
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            Sun = Request<Texture2D>("RealmOne/Assets/Effects/Sunny");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            Sun = null;
        }

        /*public override bool PreDraw(ref Color lightColor)
        {
            Color drawColor = Lighting.GetColor((int)Projectile.Center.X / 16, (int)(Projectile.Center.Y / 16));

            Main.EntitySpriteDraw(Sun.Value, Projectile.Center - Main.screenPosition,
                          Sun.Value.Bounds, Color.NavajoWhite, Projectile.rotation,
                          Sun.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            return true;
        }*/
    }
}
