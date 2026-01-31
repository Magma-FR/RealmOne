/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Bullet;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.OldGold
{
    public class Oldy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antiquity Revolver");
            Tooltip.SetDefault("'An antique and pristine revolver!'");
            //   ItemGlowy.AddGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/OldGold/Oldy_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = rorAudio.WheelgunSound;
            Item.autoReuse = true;
            Item.shootSpeed = 40f;
            Item.noMelee = true;
            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<OldGoldBullet>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet)
                type = ProjectileType<OldGoldBullet>(); // or ProjectileID.FireArrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlintlockPistol, 1)
            .AddIngredient(ItemID.Torch, 20)
            .AddIngredient(Mod, "OldGoldBar", 6)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/OldGold/Oldy_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.LightCyan,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            Gore.NewGore(source, player.Center + muzzleOffset * 1, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("TommyGunPellets").Type, 1f);

            return true;
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
            Lighting.AddLight(Item.Center, Color.LightCyan.ToVector3() * 0.5f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.Enchanted_Gold, velocity);
                dust.scale = 0.6f;
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

            line = new TooltipLine(Mod, "Oldy", "'Shoots a loud and capshot gold bullet that splits into other bullets.")
            {
                OverrideColor = new Color(254, 226, 82)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Oldy", $"Converts bullets into ancient gold bullets [i:{ItemID.MusketBall}]")
            {
                OverrideColor = new Color(232, 218, 224)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Oldy", "'Pristinity!'")
            {
                OverrideColor = new Color(18, 240, 180)
            };
            tooltips.Add(line);
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-1, 0);
            return offset;
        }
    }
}*/