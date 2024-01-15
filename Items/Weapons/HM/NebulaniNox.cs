/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.HM
{
    public class NebulaniNox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebulani Nox"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Nebulous!!'"
            + "\n'Shoot a powerful shock of shadowbeam and Nebula Powers'"
            + "\n'Uses mana and ammo but shoots a extra Nebula Arcanum if used with bullets");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 116;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 32;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item158;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.NebulaBlaze1;
            Item.shootSpeed = 20f;
            Item.value = Item.buyPrice(gold: 2, silver: 75);
            Item.crit = 4;
            Item.mana = 10;
            Item.noMelee = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "NebulaniNox", "'Feel the power of a new born star!'")
            {
                OverrideColor = new Color(167, 68, 236)
            };
            tooltips.Add(line);
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(2, 0);
            return offset;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileID.NebulaArcanum; // or ProjectileID.FireArrow;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 5 + Main.rand.Next(1); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(5);
            position += Vector2.Normalize(velocity) * 18f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.NebulaBlaze1, damage, knockback, player.whoAmI);
            }

            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.UnusedWhiteBluePurple, 0f, 0f, 150, default, 2f);
                Dust.NewDust(player.position, player.width, player.height, DustID.Shadowflame, 0f, 0f, 150, default, 2f);
            }

            for (int i = 0; i < 20; i++)
            {
                Vector2 speed = Utils.RandomVector2(Main.rand, -10f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.ShadowbeamStaff, speed * 10, Scale: 2.5f);
                ;
                ;

                d.noGravity = true;
            }

            Projectile.NewProjectile(source, position, velocity, ProjectileID.ShadowBeamFriendly, damage, knockback, player.whoAmI);
            float numberProjectiles1 = 5 + Main.rand.Next(1);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles1 - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.ShadowBeamFriendly, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FragmentNebula, 16)
            .AddIngredient(ItemID.ManaCrystal, 5)
            .AddIngredient(ItemID.FallenStar, 20)
            .AddIngredient(ItemID.IllegalGunParts, 2)
            .AddIngredient(ItemID.SpaceGun, 1)

            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }s
    }
}*/