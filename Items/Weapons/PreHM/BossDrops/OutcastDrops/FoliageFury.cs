/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Bullet;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.BossDrops.OutcastDrops
{
    public class FoliageFury : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Foliage Fury"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Shoots rapid fire leaves in an inaccurate spread"
            + "\nDoes not use ammo");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Grass;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.None;
            Item.noMelee = true;
            Item.shootSpeed = 40f;

            Item.shoot = ModContent.ProjectileType<GreenLeaf>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));

            if (Main.rand.NextBool(2))
                type = ModContent.ProjectileType<PinkLeaf>();

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(0, 0);
            return offset;
        }
    }
}*/