using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.HeldProj
{
    public class InkSpearProj : ModProjectile
    {
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 95f;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Trident);
        }

        private int t = 0;

        public override bool PreAI()
        {
            t++;

            if (t == 1)
            {
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity * 1.3f, ModContent.ProjectileType<InkGlob>(), 10, 0, Main.myPlayer);
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position, Projectile.velocity, ModContent.ProjectileType<InkGlob>(), 10, 0, Main.myPlayer);
            }

            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;

            player.heldProj = Projectile.whoAmI;

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float halfDuration = duration * 0.5f;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians(135f);
            }

            // Avoid spawning dusts on dedicated servers
            if (!Main.dedServ)
            {
                // These dusts are added later, for the 'ExampleMod' effect
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Obsidian, Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 128, Scale: 1.2f);
                }

                if (Main.rand.NextBool(4))
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Titanium, Alpha: 128, Scale: 0.3f);
                }
            }

            return false; // Don't execute vanilla AI.
        }
    }
}