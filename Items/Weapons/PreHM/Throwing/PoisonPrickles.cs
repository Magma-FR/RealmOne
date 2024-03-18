using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Throwing
{
    public class PoisonPrickles : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poison Prickles");

            Tooltip.SetDefault("Throw a bunch of prickles that stick to enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.knockBack = 0.5f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.buyPrice(silver: 11);
            Item.scale = 0.9f;
            Item.consumable = true;
            Item.maxStack = 99;
            Item.shoot = ModContent.ProjectileType<PoisonPricklesProj>();
            Item.shootSpeed = 23f;
        }

        public override bool RangedPrefix()
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3 + Main.rand.Next(1); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(18);
            position += Vector2.Normalize(velocity) * 30f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<PoisonPricklesProj>(), damage, knockback, player.whoAmI);
            }
            return false;
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
                dust.scale = 0.2f;
                dust.fadeIn = 1.1f;
                dust.noGravity = false;
                dust.noLight = false;
                dust.alpha = 60;
            }
        }
    }
}