/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using RealmOne.Common.Core;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class IgniteAK : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 0, 30, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = rorAudio.GoreGun;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.shootSpeed = 21f;

            Item.consumeAmmoOnLastShotOnly = true;

            Item.shoot = ModContent.ProjectileType<IgniteProj>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));

            if (type == ProjectileID.Bullet)
                type = ModContent.ProjectileType<IgniteProj>();
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(rorAudio.IgniteRifle with { Volume = 0.5f, PitchVariance = 0.5f }, player.Center);

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = new(0, -5);

            Gore.NewGore(source, player.Center + muzzleOffset, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("chasing").Type, 1f);

            Projectile.NewProjectile(source, position + muzzleOffset, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-2, 0);
            return offset;
        }
    }

    public class IgniteProj : ModProjectile
    {
        public override string Texture
          => Helper.Empty;

        private const int timeLeftMax = 20;
        private Vector2 origin;

        private static Texture2D trail1;
        private static Texture2D glowTrail;
        private static Texture2D trailing;

        public override void Load()
        {
            trail1 = Mod.Assets.Request<Texture2D>("Assets/Effects/TrailTextureFlames", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            glowTrail = Mod.Assets.Request<Texture2D>("Assets/Effects/GlowTrail", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            trailing = Mod.Assets.Request<Texture2D>("Assets/Effects/GradientCirc", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

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
            if (Projectile.timeLeft % 2 == 0)
            {
                Projectile.damage--;
            }
        }

        private Color color;

        public override bool PreDraw(ref Color lightColor)
        {
            float Descaling = (float)
            Projectile.timeLeft / timeLeftMax;
            float ScaleY = 18;
            color = Color.OrangeRed;

            for (int i = 0; i < 3; i++)
            {
                float shotLength = origin.Distance(Projectile.Center);

                Texture2D texture = (i > 1) ?
                trail1 : glowTrail;
                Vector2 scale = new Vector2(shotLength, MathHelper.Lerp(ScaleY, 5, 1f - Descaling)) / texture.Size();

                //descales by colour
                color = (color with { A = 0 }) * Descaling;
                Main.EntitySpriteDraw(texture, origin - Main.screenPosition, null, color, Projectile.velocity.ToRotation(), new Vector2(0, texture.Height / 2), scale, SpriteEffects.None, 0);

                Vector2 Endscale = origin + (Vector2.UnitX * shotLength).RotatedBy(Projectile.velocity.ToRotation());

                Main.EntitySpriteDraw(trailing, Endscale - Main.screenPosition, null, color, 0, trailing.Size() / 2, (0.13f - (i * 0.1f)) * Descaling, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}*/