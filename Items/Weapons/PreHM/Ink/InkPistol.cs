/*using Microsoft.Xna.Framework;
using RealmOne.Items.Misc;
using RealmOne.Projectiles.Other;
using RealmOne.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Ink
{
    public class InkPistol : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ink Pistol"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Filled to the brim with eidolic ink"
            + "\nShoots out splats of eidolic ink that slide on the ground and inflict Shadowflame"
                        + "\n20% chance to not consume Eidolic Ink"

            + $"\nUses Eidolic Ink [i:{ModContent.ItemType<EidolicInk>()}]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.value = 10000;
            Item.UseSound = SoundID.Item11;

            Item.rare = ModContent.RarityType<InkRarity>();
            Item.autoReuse = true;
            Item.useAmmo = ModContent.ItemType<EidolicInk>();
            Item.noMelee = true;
            Item.shootSpeed = 25f;
            Item.shoot = ModContent.ProjectileType<InkGlob>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 35f;

            int proj = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, 0, 0.0f, 0.0f);
            Main.projectile[proj].friendly = true;
            Main.projectile[proj].hostile = false;
            Main.projectile[proj].velocity.Y += 1f;
            Main.projectile[proj].velocity.X *= 1.2f;

            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.7f, 0.7f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Obsidian, speed * 4, Scale: 1f);
                ;
                d.noGravity = true;
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EidolicInk>(), 8);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ModContent.ItemType<EidolicInk>(), 8);

            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.20f;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-1, 0);
            return offset;
        }
    }
}*/