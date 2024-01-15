using Microsoft.Xna.Framework;
using RealmOne.Items.Ammo;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class AcornBlunderbuss : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acorn Blunderbuss"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Not very sturdy for a starter gun, but it sure makes a crunch!"
            + "\nShoots fast velocity acorn fragments that explode at falling speed"
            + "\n'The acorns explode into sharp acorn shrapnel'"
            + "\nUses Crushed Acorns as ammo");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 32;
            Item.useTime = 78;
            Item.useAnimation = 78;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            Item.shoot = ProjectileID.SeedlerNut;
            Item.shootSpeed = 88f;
            Item.value = Item.buyPrice(gold: 1, silver: 75);
            Item.noMelee = true;
            Item.useAmmo = ModContent.ItemType<CrushedAcorns>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(2); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(velocity) * 36f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.SeedlerNut, damage, knockback, player.whoAmI);
            }

            for (int d = 0; d < 20; d++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.WoodFurniture, 0f, 0f, 150, default, 2f);
                Dust.NewDust(player.position, player.width, player.height, DustID.Grass, 0f, 0f, 150, default, 2f);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Wood, 30)
            .AddIngredient(ItemID.Acorn, 14)
                        .AddIngredient(ModContent.ItemType<CrushedAcorns>(), 14)

            .AddTile(TileID.WorkBenches)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-2, 2);
            return offset;
        }
    }
}