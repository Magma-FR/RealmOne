using Microsoft.Xna.Framework;
using RealmOne.Items.Ammo;
using RealmOne.Projectiles.Bullet;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Ranged
{
    public class CrackshotWheelgun : ModItem
    {
        private int cooldown = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crackshot Wheelgun");
            Tooltip.SetDefault("Right click to throw out a Wagon Wheel that rolls on the ground"
                + "\nLeft click to shoot out a fast bullet, shoot the bullet at the wheel to explode into fire shrapnel");
        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 56;
            Item.useAnimation = 56;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 2, 25, 0);
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.autoReuse = true;
            Item.useAmmo = ModContent.ItemType<RustedBullets>();
            Item.noMelee = true;
            Item.shootSpeed = 70f;
            Item.shoot = ModContent.ProjectileType<WagonWheel>();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.UseSound = SoundID.Item1;
                Item.autoReuse = false;
                if (cooldown > 0)
                    return false;
            }
            else
            {
                Item.shootSpeed = 88f;
                Item.useTime = 56;
                Item.useAnimation = 56;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noUseGraphic = false;
                Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/WheelgunSound");
            }

            return base.CanUseItem(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 direction = Vector2.Normalize(velocity) * 11;
                velocity = direction;
                type = ModContent.ProjectileType<WagonWheel>();
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
            }
            else
            {
                player.GetModPlayer<Screenshake>().SmallScreenshake = true;

                var proj = Projectile.NewProjectileDirect(player.GetSource_ItemUse(Item), position, velocity * 2, type, damage, knockback, player.whoAmI);
                proj.GetGlobalProjectile<CrackShotGlobalProj>().hasbeenShot = true;

                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                    position += muzzleOffset;
                Gore.NewGore(source, player.Center + muzzleOffset * 1, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("TommyGunPellets").Type, 1f);

                for (int i = 0; i < 19; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Smoke, speed * 4, Scale: 1f);
                    d.noGravity = false;
                }

                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position + muzzleOffset, Vector2.Zero, ModContent.ProjectileType<TommyGunBarrelFlash>(), 0, 0, player.whoAmI);
            }

            return true;
        }
    }

    public class WagonWheel : ModProjectile
    {

        private Player Owner => Main.player[Projectile.owner];

        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wagon Wheel");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SpikyBall);
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 400;
            Projectile.aiStyle = 14;
            Projectile.friendly = false;
            Projectile.scale = 1.2f;
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.WoodFurniture, Projectile.velocity.X * 0.7f, Projectile.velocity.Y * 0.7f, Scale: 0.6f, Alpha: 90);

            var Hitbox = new Rectangle((int)Projectile.Center.X - 40, (int)Projectile.Center.Y - 40, 80, 80);
            IEnumerable<Projectile> list = Main.projectile.Where(x => x.Hitbox.Intersects(Hitbox));
            foreach (Projectile proj in list)
                if (proj.GetGlobalProjectile<CrackShotGlobalProj>().hasbeenShot && Projectile.timeLeft > 20 && proj.active)
                {
                    Projectile.timeLeft = 5;

                    proj.active = false;

                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 velocity = Vector2.Normalize(proj.velocity).RotatedBy(Main.rand.NextFloat(-0.6f, 0.6f)) * Main.rand.NextFloat(1.3f, 3);
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ProjectileID.MolotovFire3, Projectile.damage, 0, Owner.whoAmI).scale = Main.rand.NextFloat(0.85f, 1.15f);
                    }
                }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity, Mod.Find<ModGore>("WheelGore1").Type, 1f);
            for (int i = 0; i < 2; i++)
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity, Mod.Find<ModGore>("WheelGore2").Type, 1f);
            for (int i = 0; i < 2; i++)
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Projectile.velocity, Mod.Find<ModGore>("WheelGore3").Type, 1f);
        }
    }

    public class CrackShotGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool hasbeenShot = false;

        public override void SetDefaults(Projectile entity)
        {
            entity.damage = 0;
        }
    }
}