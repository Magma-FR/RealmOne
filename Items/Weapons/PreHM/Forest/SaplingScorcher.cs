using Microsoft.Xna.Framework;
using RealmOne.Items.Ammo;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class SaplingScorcher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling Scorcher"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Spits out fumes of fire"
            + "\nBurns up lumber to create combustion, when hot enough, it shoots it all out"
            + "\n20% chance of an explosive acorn to shoot out the flamethrower"
            + "\n38% chance to not consume wood"
            + $"\nUses Wood [i:{ItemID.Wood}]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.useAmmo = ItemID.Wood;
            Item.noMelee = true;
            Item.shootSpeed = 7f;
            Item.shoot = ProjectileID.Flames;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));

            if (Main.rand.NextBool(10))
                type = ProjectileID.SeedlerNut;

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.25f;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(4, 0);
            return offset;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 15);
            recipe.AddIngredient(ItemID.Torch, 20);
            recipe.AddIngredient(ModContent.ItemType<CrushedAcorns>(), 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}