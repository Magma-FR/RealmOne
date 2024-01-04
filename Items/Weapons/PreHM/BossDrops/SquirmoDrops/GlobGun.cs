
/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Bullet;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops
{
    public class GlobGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glob Gun"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'A bit too squelchy for my liking'"
            + "\nShoots a forced and sludgy explosive mud glob"
            + "\nStick the glob on a tile and let it charge up its power, dealing 2x the aoe and penetrate up to 3 enemies"
             + $"\nUses Mud Blocks [i:{ItemID.MudBlock}]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 62;
            Item.useAnimation = 62;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            Item.autoReuse = true;
            Item.useAmmo = ItemID.MudBlock;
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.Bullet;

        }
        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath11);
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ModContent.ProjectileType<GlobGunProjectile>(); // or ProjectileID.FireArrow;
        }

        /*public override void AddRecipes()
          {
              CreateRecipe()
              .AddIngredient(ItemID.MudBlock, 30)
             .AddIngredient(Mod, "GoopyGrass", 7)
              .AddIngredient(ItemID.IllegalGunParts, 1)

              .AddIngredient(ItemID.Worm, 3)
              .AddTile(TileID.Anvils)
              .Register();
        }
   

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}
*/