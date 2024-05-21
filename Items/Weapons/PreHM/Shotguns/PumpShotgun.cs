/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Bullet;
using RealmOne.RealmPlayer;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class PumpShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pump-action Shotgun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'A classic, brawny and reliable pump action shotgun'"
                + "\nShoots a spry and powerful round of 12 gauge Buckshot shells"
                + "\n'The bottom rear 'handguard' must be 'slid' back and forward to load the rounds back into the gun to fire again'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 34;
            Item.height = 25;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shootSpeed = 87f;
            Item.shoot = ModContent.ProjectileType<VintageBulletProjectile>();
            Item.noMelee = false; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(gold: 8, silver: 3);
            //   Item.noUseGraphic = true;
            // Item.channel = true;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_PumpShotgun");
            Item.reuseDelay = 20;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            float numberProjectiles = 3 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(4);
            Gore.NewGore(source, player.Center + muzzleOffset * 1, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("TommyGunPellets").Type, 1f);

            position += Vector2.Normalize(velocity) * 28f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<VintageBulletProjectile>(), damage, knockback, player.whoAmI);
            }

            for (int d = 0; d < 40; d++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.Smoke, 0f, 0f, 150, default, 1.5f);

                Dust.NewDust(player.position, player.width, player.height, DustID.Torch, 0f, 0f, 150, default, 1.5f);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddRecipeGroup("IronBar", 12);

            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "PumpShotgun", "'Can't Double Pump with this!'")
            {
                OverrideColor = new Color(0, 255, 255)
            };
            tooltips.Add(line);
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-10, -5);
            return offset;
        }
    }
}*/