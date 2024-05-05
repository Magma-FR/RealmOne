using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Jungle
{
    public class RainforestBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            // ItemGlowy.AddGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Jungle/RainforestBow_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 13;
            Item.DamageType = DamageClass.Ranged;

            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 11);
            Item.scale = 1f;
            Item.shoot = ModContent.ProjectileType<WattleArrowBase>();
            Item.shootSpeed = 35f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<WattleArrowBase>(); // or ProjectileID.FireArrow;
            if (Main.rand.NextBool(4))
            {
                type = ModContent.ProjectileType<WattleArrow>();
            }
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.DarkGreen.ToVector3() * 1f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.2f - 1.5f);

                Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.Plantera_Green, velocity);
                dust.scale = 0.5f;
                dust.fadeIn = 1.1f;
                dust.noGravity = false;
                dust.noLight = false;
                dust.alpha = 60;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 13f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            if (Main.rand.NextBool(8))
            {
                for (int i = 0; i < 2; i++)
                {
                    int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), position + muzzleOffset, velocity, ProjectileID.SporeTrap, 15, 0, player.whoAmI);
                    Main.projectile[p].scale = 1f;
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = false;
                }
            }
            return true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>(Texture + ("_Glow"), AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.LightYellow,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RichMahogany, 18)
                 .AddIngredient(ItemID.JungleSpores, 10)
                                  .AddIngredient(ItemID.Vine, 3)

                 .AddTile(TileID.Anvils)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(-1, 0);
            return offset;
        }
    }
}