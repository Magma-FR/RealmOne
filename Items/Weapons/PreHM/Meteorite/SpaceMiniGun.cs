using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Meteorite
{
    public class SpaceMiniGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Space Mini Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Shoots rapid fire space lasers"
            + "\nSpace Gun's big brother"
            + "\nUses Mana"
              + $"[i:{ItemID.ManaCrystal}]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item157;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.GreenLaser;
            Item.mana = 2;
            Item.shootSpeed = 30f;
            Item.noMelee = true;
            Item.value = Item.buyPrice(gold: 3, silver: 75);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            position += Vector2.Normalize(velocity) * 35f;
            const int NumProjectiles = 2;

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
                newVelocity *= 1f - Main.rand.NextFloat(0.2f);

                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(12));
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MeteoriteBar, 15)
            .AddIngredient(ItemID.SpaceGun, 1)
            .AddIngredient(Mod, "GizmoScrap", 2)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-3, 0);
            return offset;
        }
    }
}