using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using ReLogic.Content;
using Terraria.GameContent;
using RealmOne.Common.Core;
using Terraria.Audio;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Bullet;
using RealmOne.Projectiles.Other;

namespace RealmOne.Projectiles.Magic
{ 
    internal class LightBulbRing1 : ModProjectile
    {
        static Texture2D tex;
        public override void Load()
        {
            tex = (Texture2D)ModContent.Request<Texture2D>("RealmOne/Assets/Effects/LightbulbShine", AssetRequestMode.ImmediateLoad);
        }
        public override void Unload()
        {
            tex = null;
        }
        public override string Texture => Helper.Empty;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
 
            Projectile.penetrate = -1;
            Projectile.damage = 1;
        }
        ref float Timer => ref Projectile.ai[0];
        ref float NumOfImages => ref Projectile.ai[1];
        Vector2 scale;
        bool channel = false;   
        public override void OnSpawn(IEntitySource source)
        {
            NumOfImages = 1;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(rorAudio.BulbShatter, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<Starbang>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LightbulbBulletGore3").Type, 1f);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int maxNumImages = 8;

            player.itemAnimation = 2;
            player.itemTime = 2;

            Timer++;

            if (Timer % 60 == 0 && !(NumOfImages >= maxNumImages))
            {
                Projectile.damage += 1;
                NumOfImages++;
            }

            if (player.statMana <= 0)
            {
                Projectile.Kill();
            }

            if (NumOfImages >= maxNumImages)
            {
                NumOfImages = maxNumImages;
            }

            if (!player.channel && channel == false)
            {
                channel = true;
                Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * 2 * NumOfImages;
                
                Projectile.timeLeft = 30 * (int)NumOfImages;
            }

            if(channel == false)
            {
                Projectile.Center = player.Center;
            }
            else
            {
                Projectile.velocity *= 0.98f;
                if (Timer % 20 == 0)
                {
                    NumOfImages--;
                }
            }

            Lighting.AddLight(Projectile.Center, Color.LightGoldenrodYellow.ToVector3() * 1f * NumOfImages / maxNumImages);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            for (int i = 1; i <= NumOfImages; i++)
            {
                float scaleFloat = 0.65f - 0.25f / i * (float)Math.Sin(Timer / 20) / 2;
                scale = new(scaleFloat, scaleFloat);

                float colorMultiplier = 0.75f - scale.X;
                Vector2 offset = new Vector2(tex.Width / 2, tex.Height / 2) * scale;

                Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) - offset,
                null, Color.LightGoldenrodYellow * colorMultiplier, Projectile.rotation, Projectile.Hitbox.Size() / 2, scale, SpriteEffects.None);
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Vector2.Distance(Projectile.Center, targetHitbox.Center.ToVector2()) <= tex.Width * scale.X / 2;
        }
    }
}
