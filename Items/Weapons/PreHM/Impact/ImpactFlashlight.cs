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

        private float AimResponsiveness = 0.67f;
        private bool timerUp = false;
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
            Item.shootSpeed = 1f;
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

        private float AimResponsiveness = 0.67f;
        private bool timerUp = false;
        public override string Texture => "RealmOne/Items/Weapons/PreHM/Impact/ImpactFlashLights";

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // SpriteEffects helps to flip texture horizontally and vertically
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // Getting texture of projectile
            var texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            // If texture without animation frameHeight is always texture.Height and startY is always 0
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            var sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

            // Alternatively, you can skip defining frameHeight and startY and use this:
            // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

            Vector2 origin = sourceRectangle.Size() / 2f;

            // If image isn't centered or symmetrical you can specify origin of the sprite
            // (0,0) for the upper-left corner
            float offsetX = 10f;
            origin.X = Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX;

            // If sprite is vertical
            // float offsetY = 20f;
            // origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);

            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return false;
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
                            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center + new Vector2(0, 10f).RotatedBy(Projectile.rotation), Vector2.Zero, ModContent.ProjectileType<BlueFlash>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);

             
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
                        Player player = Main.player[Projectile.owner];

            bool stillInUse = player.channel && !player.noItems && !player.CCed;
            if (Projectile.owner == Main.myPlayer)
            {
                UpdatePlayerVisuals(player, player.Center);

                UpdateAim(player.Center, player.HeldItem.shootSpeed);

            }

        }
        private void UpdateAim(Vector2 source, float speed)
        {
            Player player = Main.player[Projectile.owner];
            // Get the player's current aiming direction as a normalized vector.
            var aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }

            Vector2 DirAndVel = new(Projectile.velocity.X * player.direction, Projectile.velocity.Y * player.direction);
            Projectile.rotation = DirAndVel.ToRotation();
            // Change a portion of the Prism's current velocity so that it points to the mouse. This gives smooth movement over time.
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, AimResponsiveness));
            aim *= speed;

            if (aim != Projectile.velocity)
            {
                Projectile.netUpdate = true;
                Projectile.netImportant = true;
                Projectile.netUpdate = true;
            }


            Projectile.velocity = aim;
        }

        private void UpdatePlayerVisuals(Player player, Vector2 playerhandpos)
        {
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.Center = playerhandpos;
            Projectile.spriteDirection = Projectile.direction;

            // Constantly resetting player.itemTime and player.itemAnimation prevents the player from switching items or doing anything else.
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            // If you do not multiply by projectile.direction, the player's hand will point the wrong direction while facing left.
            //player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            float piover2 = MathHelper.PiOver2;
            if (player.direction == 1)
            {
                piover2 -= MathHelper.Pi;
            }

            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + piover2);
        }
    }
}