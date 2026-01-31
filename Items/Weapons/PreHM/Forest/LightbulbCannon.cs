/*using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.Bullet;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class LightbulbCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbulb Cannon"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Shoots a massive lightbulb that release a large aura of light'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item61;
            Item.autoReuse = true;
            Item.shootSpeed = 25f;
            Item.shoot = ModContent.ProjectileType<LightbulbBullet>();
            // Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Scroll");

            Item.value = Item.buyPrice(gold: 1, silver: 3);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.YellowStarDust, speed * 4, Scale: 1.3f);
                ;
                d.noGravity = true;
                d.noLight = false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "BrassIngot", 6);
            recipe.AddIngredient(ItemID.Glass, 15);

            recipe.AddIngredient(ModContent.ItemType<LiteBulb>(), 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(3, 0);
            return offset;
        }
    }
}*/