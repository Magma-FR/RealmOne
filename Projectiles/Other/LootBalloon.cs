using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Other
{
    public class LootBalloon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Loot Balloon");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 60;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.aiStyle = 0;
        }

        public override void AI()
        {
            Projectile.rotation += 0.00f;
            Projectile.velocity.Y += 0.00001f;

            Vector2 center = Projectile.Center;

            Lighting.AddLight(Projectile.position, 0.2f, 0.2f, 0.4f);
            Lighting.Brightness(1, 1);
        }

        public override void OnKill(int timeleft)
        {
            if (Main.rand.Next(0, 0) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                    ItemID.FloatingIslandFishingCrate, 1, false, 0, false, false);
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Cloud, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LootGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LootGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("LootGore3").Type, 1f);

            SoundEngine.PlaySound(SoundID.MaxMana);
        }
    }
}