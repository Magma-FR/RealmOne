using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.ForestRevengeSet
{
    public class FireflyJar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 0, 0, 45);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.shoot = ModContent.ProjectileType<FireflyJarProj>();
            Item.shootSpeed = 11f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LightYellow.ToVector3() * 1f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.DesertTorch, velocity);
                dust.scale = 1f;
                dust.fadeIn = 0.4f;
                dust.noGravity = true;
                dust.noLight = false;
                dust.alpha = 0;
            }
        }
    }

    public class FireflyJarProj : ModProjectile
    {
        public override string Texture => "RealmOne/Items/Sets/ForestRevengeSet/FireflyJar";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;

            Projectile.aiStyle = 2;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.light = 1f;

            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 2;
            Projectile.CloneDefaults(ProjectileID.Shuriken);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, r: 0.2f, g: 0.8f, b: 1.5f);

            Lighting.Brightness(1, 1);

            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.5f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeleft)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileID.SolarWhipSwordExplosion, Damage: 0, Projectile.knockBack, Projectile.owner);

            for (int i = 0; i < 17; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 50, default, 2f);

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(rorAudio.BulbShatter, Projectile.position);

            if (Main.rand.Next(0, 10) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                    ModContent.ItemType<FireflyJar>(), 1, false, 0, false, false);
            if (Main.rand.Next(0, 7) == 0)
                Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height,
                    ItemID.Firefly, 1, false, 0, false, false);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, 6, -4, ModContent.ProjectileType<FireflyProj>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, -6, -4, ModContent.ProjectileType<FireflyProj>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, 6, 4, ModContent.ProjectileType<FireflyProj>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position.X, Projectile.position.Y, -6, 4, ModContent.ProjectileType<FireflyProj>(), Projectile.damage / 2, Projectile.knockBack, Main.myPlayer);

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore3").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore4").Type, 1f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            target.AddBuff(BuffID.OnFire, 600);

            Projectile.Kill();
        }
    }
}