using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.Sets.ForestRevengeSet;
using RealmOne.Projectiles.Other;
using RealmOne.Projectiles.Throwing;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Classless
{
    public class CartonBeer : ModItem
    {
        public override void SetStaticDefaults()
        {

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Generic;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 0, 0, 25);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.shoot = ModContent.ProjectileType<CartonBeerProj>();
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

                Item.useStyle = ItemUseStyleID.DrinkLiquid;
                Item.useTime = 38;
                Item.useAnimation = 38;

                Item.width = 20;
                Item.height = 20;
                Item.maxStack = 999;
                Item.shoot = ProjectileID.None;
                Item.value = 500;
                Item.rare = ItemRarityID.Blue;
                Item.consumable = true;

                Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/LightbulbShine");

                if (Main.rand.NextBool(1))
                    player.AddBuff(BuffID.Tipsy, 600);


            }

            else
            {
                Item.damage = 13;
                Item.DamageType = DamageClass.Generic;
                Item.width = 24;
                Item.height = 24;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.knockBack = 1f;
                Item.rare = ItemRarityID.Blue;
                Item.UseSound = SoundID.Item1;
                Item.autoReuse = true;
                Item.maxStack = 999;
                Item.shoot = ModContent.ProjectileType<CartonBeerProj>();
                Item.shootSpeed = 10f;
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.consumable = true;
            }

            return base.CanUseItem(player);
        }


    }
    public class CartonBeerProj : ModProjectile
    {
        public override string Texture => "RealmOne/Items/Weapons/PreHM/Classless/CartonBeerProj";

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


            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 55, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 0.5f);




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

            for (int i = 0; i < 17; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 55, 0f, 0f, 50, default, 2f);

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);


            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore3").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("JarGore4").Type, 1f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {

            Projectile.Kill();
        }
    }
}