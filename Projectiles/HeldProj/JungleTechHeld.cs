using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.HeldProj
{
    public class JungleTechHeld : ModProjectile
    {
        private float AimResponsiveness = 0.67f;
        private bool timerUp = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;//23
            Projectile.height = 26;//23
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 4;
            Projectile.ownerHitCheck = true;//so it cant attack through walls
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            if (++Projectile.frameCounter >= 15f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            Player player = Main.player[Projectile.owner];
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 60f)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Projectile.ai[0] = -1f;
                timerUp = true;
                Projectile.netUpdate = true;
                Projectile.netImportant = true;
                Projectile.netUpdate = true;
                //dust
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.Plantera_Green, speed * 5, Scale: 1.5f);
                    dust1.noGravity = true;

                    int dust2 = Dust.NewDust(Projectile.Center, 1, 1, DustID.Smoke, 0f, 0f, 200, default, 0.8f);
                    Main.dust[dust2].noGravity = false;
                }
            }

            if (timerUp == true)
            {
                Projectile.ai[0] -= 1;
            }

            //small delay before despawning
            if (player.channel == false)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 15)
                {
                    Projectile.ai[1] = 0;
                    Projectile.Kill();
                }
            }
            //bullet shooting
            if (timerUp == true && player.channel == false && Projectile.ai[1] == 1)
            {
                ShootBullets();
            }

            bool stillInUse = player.channel && !player.noItems && !player.CCed;
            if (Projectile.owner == Main.myPlayer)
            {
                UpdatePlayerVisuals(player, player.Center);

                UpdateAim(player.Center, player.HeldItem.shootSpeed);
            }
            else if (!stillInUse)
            {
                Projectile.Kill();
            }

            Projectile.timeLeft = 2;
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
            float offsetX = 20f;
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

        private void ShootBullets()
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(SoundID.Item38, Projectile.position);
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;

            if (Main.myPlayer == Projectile.owner)
            {
                float numberProjectiles = 5 + Main.rand.Next(4, 5);
                Projectile.position += Vector2.Normalize(Projectile.velocity) * 16f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(MathHelper.Lerp(-MathHelper.ToRadians(Main.rand.Next(2, 16)), MathHelper.ToRadians(Main.rand.Next(4, 16)), i / (numberProjectiles - 1))) * 1f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(player.GetSource_ItemUse_WithPotentialAmmo(player.HeldItem, AmmoID.Bullet), Projectile.position, perturbedSpeed, ProjectileID.Bullet, Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
        }
    }
}