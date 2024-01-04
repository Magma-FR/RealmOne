
/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops
{

    public class SquirmStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons worms from beneath the ground to chase after enemies");
            DisplayName.SetDefault("Squirm Staff");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 9;
            Item.damage = 9;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 2f;
            Item.useAnimation = 34;
            Item.useTime = 34;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 11);

            Item.shoot = ModContent.ProjectileType<SquirmStaffProjectile>();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-2, 0);
            return offset;
        }
        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath11);
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y + 100f)
                ceilingLimit = player.Center.Y + 100f;

            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, -600f);
                position.Y += 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                    heading.Y *= 1f;

                if (heading.Y < -20f)
                    heading.Y = -20f;

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.MudBlock, 14)
            .AddIngredient(ItemID.Worm, 3)
            .AddRecipeGroup("Wood", 18)
            .AddTile(TileID.WorkBenches)
            .Register();

        }
    }
}
*/