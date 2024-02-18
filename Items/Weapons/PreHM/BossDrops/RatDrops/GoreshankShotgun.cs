using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.BossDrops.RatDrops
{
    public class GoreshankShotgun : ModItem
    {
        //    private int shotCount;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 34;
            Item.height = 25;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shootSpeed = 70f;
            Item.shoot = ModContent.ProjectileType<GoreshankShot>();
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(gold: 5, silver: 3);

            Item.useAmmo = AmmoID.Bullet;
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item38 with { Volume = 0.5f, PitchVariance = 0.5f, MaxInstances = 3 }, player.Center);
            SoundEngine.PlaySound(SoundID.Item149 with { Volume = 0.7f, PitchVariance = 0.5f, MaxInstances = 3 }, player.Center);

            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            float numberProjectiles = 3 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(10);
            Gore.NewGore(source, player.Center + muzzleOffset * 1, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("TommyGunPellets").Type, 1f);

            position += Vector2.Normalize(velocity) * 28f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<GoreshankShot>(), damage, knockback, player.whoAmI);
            }

            return false;
        }


    }

    public class GoreshankShot : ModProjectile
    {
        public override string Texture
            => Helper.Empty;

        private const int timeLeftMax = 18;
        private Vector2 origin;

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(12);

            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = timeLeftMax;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Projectile.timeLeft == timeLeftMax)
            {
                origin = Projectile.Center;
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                return;
            }

            for (int i = 0; i < 18; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, Main.rand.NextBool(2) ? DustID.Blood
              : DustID.t_Flesh, Vector2.Zero, 0, Color.White, Main.rand.NextFloat(0.7f, 1.0f));
                dust.velocity = -(Projectile.velocity * Main.rand.NextFloat(0.2f, 0.5f)).RotatedByRandom(0.8f);
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            float Descaling = (float)
            Projectile.timeLeft / timeLeftMax;
            float ScaleY = 20;
            Color color = Color.PaleVioletRed;

            for (int i = 0; i < 3; i++)
            {
                float shotLength = origin.Distance(Projectile.Center);

                Texture2D texture = (i > 1) ?
                Mod.Assets.Request<Texture2D>("Assets/Effects/Trail_1").Value : Mod.Assets.Request<Texture2D>("Assets/Effects/GlowTrail").Value;
                Vector2 scale = new Vector2(shotLength, MathHelper.Lerp(ScaleY, 5, 1f - Descaling)) / texture.Size();

                //descales by colour
                color = (color with { A = 0 }) * Descaling;
                Main.EntitySpriteDraw(texture, origin - Main.screenPosition, null, color, Projectile.velocity.ToRotation(), new Vector2(0, texture.Height / 2), scale, SpriteEffects.None, 0);
            }

            return false;
        }
    }

    public class GoreshankShotgunS : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GladiusStab);
        }
    }
}