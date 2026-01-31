/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Hell
{
    public class TorchedTextbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Conjure a wisp of hell born spells. Mixture of homing lashes, bats and firebeams");
            DisplayName.SetDefault("Torched Textbook");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            //   ItemGlowy.AddGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Hell/TorchedTextbook_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 42;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 6;
            Item.damage = 22;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 38f;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.UseSound = SoundID.DD2_PhantomPhoenixShot;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(gold: 4, silver: 11);

            Item.shoot = ProjectileID.Hellwing;
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item73);
            return true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Hell/TorchedTextbook_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            else
                frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            var offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
                time = 2f - time;

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(118, 240, 209, 70), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(196, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Orange.ToVector3() * 1f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.GoldFlame, velocity);
                dust.scale = 1f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "TorchedTextbook", "Read with heat")
            {
                OverrideColor = new Color(255, 87, 51)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "TorchedTextbook", "'Turn to page -666 for instant human combustion'")
            {
                OverrideColor = new Color(223, 159, 103)
            };

            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ItemID.HellstoneBar, 8)
            .AddIngredient(ItemType<Parchment>(), 5)
            .AddIngredient(ItemID.Book, 1)
            .AddIngredient(ItemID.LavaBucket, 3)

            .AddTile(TileID.Anvils)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-1, 0);
            return offset;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            float numberProjectiles = 1 + Main.rand.Next(1); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(3);
            position += Vector2.Normalize(velocity) * 35f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .1f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            for (int i = 0; i < 57; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Torch, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }

            Projectile.NewProjectile(source, position, velocity, ProjectileID.Flames, damage, knockback, player.whoAmI);
            float numberProjectiles1 = 5 + Main.rand.Next(1);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles1 - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, ProjectileID.Flamelash, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Every projectile shot from this gun has a 1/3 chance of being an ExampleInstancedProjectile
            if (Main.rand.NextBool(2))
                type = ProjectileID.ImpFireball;
        }
    }
}*/