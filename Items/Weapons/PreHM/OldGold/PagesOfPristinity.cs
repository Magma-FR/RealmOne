using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Misc;
using RealmOne.Items.Misc.Bars;
using RealmOne.Projectiles.Magic;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.OldGold
{
    public class PagesOfPristinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Conjure theurgy and divine sandnadoes from the sand-collected pages");
            DisplayName.SetDefault("Pages of Pristinity");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {   
            Item.width = 30;
            Item.height = 42;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 14;
            Item.damage = 12;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Orange;
            Item.shootSpeed = 3f;
            Item.useAnimation = 42;
            Item.useTime = 42;
            Item.UseSound = SoundID.DD2_PhantomPhoenixShot;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(gold: 4, silver: 11);

            Item.shoot = ModContent.ProjectileType<OldGoldNado>();
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item90);
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int a = 0; a < Main.rand.Next(1, 5); a++)
            {
                float angle = Main.rand.NextFloat(MathHelper.PiOver4, -MathHelper.Pi - MathHelper.PiOver2);

                Vector2 PositionArea = Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))) * 35f;
                if (Collision.CanHit(position, 0, 0, position + PositionArea, 0, 0))
                    position += PositionArea;
            }

            for (int i = 0; i < 130; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Sandnado, speed * 8, Scale: 1f);
                ;
                d.noGravity = true;
            }

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
            Lighting.AddLight(Item.Center, Color.SkyBlue.ToVector3() * 1f);

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

            line = new TooltipLine(Mod, "PagesOfPristinity", "'The Sandnadoes have been spinning before the dawn of time!'")
            {
                OverrideColor = new Color(254, 226, 82)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "PagesOfPristinity", "'Pristinity!'")
            {
                OverrideColor = new Color(18, 240, 180)
            };
            tooltips.Add(line);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<OldGoldBar>(), 6)
            .AddIngredient(ModContent.ItemType<Parchment>(), 5)
            .AddIngredient(ItemID.SandstoneBrick, 15)
            .AddRecipeGroup("Sand", 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}