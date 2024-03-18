/*using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.BossDrops.OutcastDrops
{
    public class Overgrowth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overgrowth");
            Tooltip.SetDefault("Conjure a large swarm of foliage to damage enemies"
                + "\n'This is what happens when you dont take care of your backyard'"
                + "\n'This branch is so old, Jesus used it in his science experiment in Year 9 :skull:'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 7;
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.shootSpeed = 19f;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.Item8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 11);

            Item.shoot = ModContent.ProjectileType<ShrubbyLeaf>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
                ceilingLimit = player.Center.Y - 200f;
            // Loop these functions 3 times.
            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                    heading.Y *= -1f;

                if (heading.Y < 20f)
                    heading.Y = 20f;
                Vector2 speed = Main.rand.NextVector2CircularEdge(3f, 3f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Grass, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<ShrubbyLeaf>(), damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Grass);
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(2, 0);
            return offset;
        }
    }
}*/