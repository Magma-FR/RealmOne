using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Projectiles.Bullet;
using RealmOne.Projectiles.Returning;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Impact
{
    public class ImpactFlashlight : ModItem
    {
        public override void SetStaticDefaults()
        {
            
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 30;
            Item.height = 30;
            Item.useAnimation = 4;
            Item.useTime = 4;
            Item.knockBack = 0f;
            Item.damage = 7;
            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.shootSpeed = 3f;
            Item.shoot = ModContent.ProjectileType<ImpactFlashLights>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "ImpactTech", 10 );

            recipe.AddTile(TileID.Anvils);
            recipe.Register();

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
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(20, 170, 250, 70), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(70, 120, 190, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.AliceBlue.ToVector3() * 0.4f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.IceGolem, velocity);
                dust.scale = 0.9f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }
    }

    public class ImpactFlashLights: ModProjectile
    {
        public override string Texture => "RealmOne/Items/Weapons/PreHM/Impact/ImpactFlashLights";

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

     
        public override void AI()
        {
            Player Player = Main.player[Projectile.owner];
            if (Projectile.ai[0] >= 26f)
            {
                if (Projectile.ai[0] == 26f) SoundEngine.PlaySound(SoundID.Item25, Player.Center);
                Projectile.ai[0] = 27f;
                bool released = false;
                if (!Player.channel || Player.noItems || Player.CCed) released = true;
                if (released)
                {
                    Projectile.ai[1]++;
                    if (Projectile.ai[1] <= 5)
                    {
                        if (Projectile.ai[1] == 3f)
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + new Vector2(0, 20f).RotatedBy(Projectile.rotation), Vector2.Zero, ModContent.ProjectileType<ConeFlash>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);

             
                    }
                    else
                    {
                        if (Projectile.ai[1] == 10) Projectile.Kill();
                      
                    }
                }
            }
            else
            {
                if (Projectile.ai[0] % 5 == 0)
                if (!Player.channel || Player.noItems || Player.CCed) Projectile.Kill();
            }
            Projectile.ai[0]++;
                if (Main.MouseWorld.X > Player.Center.X) Player.ChangeDir(1);
            else if (Main.MouseWorld.X < Player.Center.X) Player.ChangeDir(-1);
            Projectile.rotation = (Player.Center - Main.MouseWorld).ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = Player.direction;
            Projectile.Center = Player.MountedCenter;
            Projectile.position = Projectile.Center;
            Player.itemAnimation = 2;
            Player.itemTime = 2;
            Player.heldProj = Projectile.whoAmI;
        }
    }
}