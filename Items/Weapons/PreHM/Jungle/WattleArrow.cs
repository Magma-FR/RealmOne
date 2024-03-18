using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Jungle
{
    public class WattleArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wattle Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 10;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 320;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.arrow = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnKill(int timeleft)
        {
            for (var i = 0; i < 10; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Plantera_Green, 0f, 0f, 0, default, 1f);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.t_Lihzahrd, 0f, 0f, 0, default, 1f);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.NettleBurstLeft, Projectile.damage, Projectile.knockBack, Main.myPlayer);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.3f);
                Color color = new Color(40, 240, 10) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.rotation, frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }

        public override void AI()
        {
            int dust1 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Plantera_Green, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Main.dust[dust1].noLight = false;
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].scale = 0.8f;
            Main.dust[dust1].velocity *= 0.5f;

            Projectile.rotation = Projectile.velocity.ToRotation();

            int dust2 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.t_Lihzahrd, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Main.dust[dust2].noLight = false;
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].scale = 0.8f;
            Main.dust[dust2].velocity *= 0.5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = target.whoAmI;
            target.AddBuff(BuffID.Poisoned, Projectile.timeLeft);
            Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
            Projectile.damage = 10;
        }
    }
}