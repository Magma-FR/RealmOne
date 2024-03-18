using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class IchorGlob : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Glob");
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 9;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.timeLeft = 120;
            Projectile.scale = 1f;
            Projectile.penetrate = 1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.NavajoWhite;
        }

        public override void OnKill(int timeLeft)
        {
            int radius = 150;

            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC target = Main.npc[i];
                if (target.active && !target.friendly && Vector2.Distance(Projectile.Center, target.Center) < radius)
                {
                    target.SimpleStrikeNPC(damage: 25, 0);
                }
            }

            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            for (int i = 0; i < 3; i++)
            {
                _ = Projectile.velocity.X * Main.rand.NextFloat(.46f, .8f) + Main.rand.NextFloat(-7f, 8f);
                _ = Projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;

                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, -4, ModContent.ProjectileType<IchorGlobSmall>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 4, 0, ModContent.ProjectileType<IchorGlobSmall>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, -4, 0, ModContent.ProjectileType<IchorGlobSmall>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 4, ModContent.ProjectileType<IchorGlobSmall>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }

            for (int i = 0; i < 100; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Ichor, speed * 10, Scale: 3f);
                ;
                d.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 400);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !target.friendly;
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f;
            Projectile.velocity *= 0.95f;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Scale: 1.2f);
        }
    }
}