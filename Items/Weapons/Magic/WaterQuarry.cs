/*using Microsoft.Xna.Framework;
using RealmOne.Dusts;
using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Magic
{
    public class WaterQuarry : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water's Quarry"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Hold out a mystical and beautiful water scepter"
                + "\nShoots out a reckless and powerful swishing ball of water"
                + "\nThe water swish explodes in a large radius");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 25;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shootSpeed = 50f;
            Item.shoot = ModContent.ProjectileType<WaterQuarryHeld>();
            Item.scale = 1f;
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(gold: 2, silver: 3);
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.staff[Item.type] = true;
            Item.mana = 6;
            Item.useAmmo = AmmoID.None;

            Item.UseSound = SoundID.Item66;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            Lighting.AddLight(player.position, r: 0.5f, g: 1f, b: 1.7f);
            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, ModContent.DustType<BubbleDust>(), speed * 6, Scale: 0.9f);
                ;
                d.noGravity = true;

                d.velocity *= 0.3f;
            }

            return true;
        }
    }
}*/