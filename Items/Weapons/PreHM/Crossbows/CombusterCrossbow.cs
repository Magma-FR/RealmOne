using Microsoft.Xna.Framework;
using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Crossbows
{
    public class CombusterCrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Combuster Crossbow");
            Tooltip.SetDefault("Hold out a burning and powerful crossbow that shoots flaming bolts"
                + "\nThe crossbow shoots out 2 crossbolts, having to let go of the button and reload"
                + "\nThe crossbow is equipped with a helpful scope to zoom out and scout further out");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 46;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 25;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = true;
            Item.shootSpeed = 50f;
            Item.shoot = ModContent.ProjectileType<CombusterCrossbowProj>();
            Item.scale = 1f;
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(gold: 2, silver: 3);
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_CrossbowLoad");
        }

        /*public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right Click function
                player.scope = true;

            return base.CanUseItem(player);
        }*/

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            for (int i = 0; i < 70; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Torch, speed * 6, Scale: 1.3f);
                ;
                d.noGravity = true;
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddIngredient(ItemID.Obsidian, 15);
            recipe.AddIngredient(ItemID.HellfireArrow, 25);

            recipe.AddIngredient(ItemID.Cobweb, 8);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}