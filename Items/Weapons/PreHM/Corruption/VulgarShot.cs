using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Corruption
{
    public class VulgarShot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulgar Shot");
            Tooltip.SetDefault("'Absolutely Vile!'"
                + "\nCharges up Vile Eyes from the bow"
                + "\nReleasing the button shoots all Vile Eyes at once");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 0, 75, 0);
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<VulgarShotHeld>();
            Item.shootSpeed = 30f;
            Item.noMelee = true;
            Item.UseSound = rorAudio.SFX_CrossbowLoad;

            Item.channel = true;
            Item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            for (int i = 0; i < 80; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.CursedTorch, speed * 10, Scale: 1f); ;
                d.noGravity = true;
            }
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DemoniteBar, 10);

            recipe.AddIngredient(ModContent.ItemType<InfectedViscus>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(3, 0);
            return offset;
        }
    }
}