using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Arrow;
using RealmOne.Projectiles.Bullet;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.HeldProj
{
    public class VulgarShotHeld : ModProjectile
    {
        private float AimResponsiveness = 1f;
        private bool timerUp = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;//number of frames the animation has
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 3;
            Projectile.ownerHitCheck = true;//so it cant attack through walls
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        private bool recoilFX;

        public override void AI()
        {
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            if (++Projectile.frameCounter >= 13f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0] += 1f;

            if (Projectile.ai[0] >= 90f)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath38, Projectile.position);
                Projectile.ai[0] = -1f;
                timerUp = true;
                Projectile.netUpdate = true;
                Projectile.netImportant = true;
                Projectile.netUpdate = true;
                //dust
                for (int i = 0; i < 20; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.CursedTorch, speed * 7, Scale: 1.2f);
                    dust1.noGravity = true;
                }
            }
            if (timerUp == true)
                Projectile.ai[0] -= 1;

            if (player.channel == false)
            {
                Projectile.Kill();
            }

            //calling shootbullets and recoil
            if (Projectile.ai[0] == 79)
                ShootBullets();
            if (Projectile.ai[0] == 87)
                ShootBullets();
            if (Projectile.ai[0] > 78 && Projectile.ai[0] < 86)
                recoilFX = true;
            else
                recoilFX = false;

            Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);

            bool stillInUse = player.channel && !player.noItems && !player.CCed;
            if (Projectile.owner == Main.myPlayer)
            {
                UpdatePlayerVisuals(player, rrp);

                UpdateAim(rrp, player.HeldItem.shootSpeed);
            }
            else if (!stillInUse)
                Projectile.timeLeft = 2;
        }

        private void UpdatePlayerVisuals(Player player, Vector2 playerhandpos)
        {
            Projectile.Center = playerhandpos;
            Projectile.spriteDirection = Projectile.direction;

            // Constantly resetting player.itemTime and player.itemAnimation prevents the player from switching items or doing anything else.
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;

            float piover2 = MathHelper.PiOver2;
            if (player.direction == 1)
            {
                piover2 -= MathHelper.Pi;
            }
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + piover2);
        }

        private void UpdateAim(Vector2 source, float speed)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
                aim = -Vector2.UnitY;
            Vector2 DirAndVel = new(Projectile.velocity.X * player.direction, Projectile.velocity.Y * player.direction);
            Projectile.rotation = DirAndVel.ToRotation();
            //lerp = to, from, speed to go
            aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(Projectile.velocity), aim, AimResponsiveness));
            aim *= speed;

            if (aim != Projectile.velocity)
                Projectile.netUpdate = true;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.velocity = aim;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
                spriteEffects = SpriteEffects.FlipHorizontally;

            // get texture
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            // Calculating frameHeight and current Y pos dependence of frame
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int startY = frameHeight * Projectile.frame;

            // Get this frame on texture
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;

            //recoil
            float offsetX = -5f;
            if (recoilFX == true)
            {
                Main.instance.LoadProjectile(Projectile.type);
                Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
                for (int k = 0; k < Projectile.oldPos.Length; k++) // old pos
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
                }

                offsetX -= 8;
                if (offsetX == 32)
                    offsetX += 4;
            }

            origin.X = Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX;

            // Applying lighting and draw current frame
            Color drawColor = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
                sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

            //return false to not draw original texture
            return false;
        }

        private void ShootBullets()
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(rorAudio.SFX_CrossbowShot, Projectile.position);

            if (Main.myPlayer == Projectile.owner)
            {
                float numberProjectiles = 2 + Main.rand.Next(0, 0);
                float numberProjectiles1 = 1 + Main.rand.Next(0, 0);

                Projectile.position += Vector2.Normalize(Projectile.velocity) * 16f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedBy(MathHelper.Lerp(-MathHelper.ToRadians(Main.rand.Next(2, 16)), MathHelper.ToRadians(Main.rand.Next(4, 15)), i / (numberProjectiles - 1))) * 1f; // Watch out for dividing by 0 if there is only 1 projectile.

                    Projectile.NewProjectile(player.GetSource_ItemUse_WithPotentialAmmo(player.HeldItem, AmmoID.Arrow), Projectile.position, perturbedSpeed, ModContent.ProjectileType<VileArrow>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }

                for (int i = 0; i < numberProjectiles1; i++)
                {
                    Vector2 perturbedSpeed1 = Projectile.velocity.RotatedBy(MathHelper.Lerp(-MathHelper.ToRadians(Main.rand.Next(2, 16)), MathHelper.ToRadians(Main.rand.Next(4, 15)), i / (numberProjectiles - 1))) * 0.4f; // Watch out for dividing by 0 if there is only 1 projectile.

                    Projectile.NewProjectile(player.GetSource_ItemUse_WithPotentialAmmo(player.HeldItem, AmmoID.Arrow), Projectile.position, perturbedSpeed1, ModContent.ProjectileType<CorruptEye>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
        }
    }
}