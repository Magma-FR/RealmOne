using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.CodeAnalysis;
using RealmOne.Common.Core;

namespace RealmOne.Items.Sets.ForestRevengeSet
{
    public class SeekingOwlProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 550;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.damage = 16;
            Projectile.extraUpdates = 1;
        }

        private int maxTrackDistance = 150;
        private Player Player => Main.player[Projectile.owner];

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.Center = Player.Center;
            Projectile.rotation = Projectile.Center.DirectionTo(Main.MouseWorld).RotatedByRandom(MathHelper.Pi).ToRotation();
        }

        public override void AI()
        {
            Player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Player.Center.DirectionTo(Projectile.Center).ToRotation() - MathHelper.PiOver2);
            Player.direction = Projectile.Center.X > Player.Center.X ? 1 : -1;

            Vector2 targetpos = FindTarget(maxTrackDistance) == null ? Main.MouseWorld : FindTarget(maxTrackDistance).Center;

            float rot = Utils.SafeNormalize(targetpos - Projectile.Center, Vector2.One).RotatedByRandom(MathHelper.PiOver2).ToRotation();
            float speed = MathHelper.Clamp(Projectile.Center.Distance(targetpos) / 50, int.MinValue, 25);

            Projectile.velocity += rot.ToRotationVector2() * speed;
            Projectile.velocity *= 0.9f;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.Center, 10, 10, DustID.Blood, Main.rand.Next(10), Main.rand.Next(10));
            }
        }

        private NPC FindTarget(int maxdistance)
        {
            NPC target = null;
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && (npc.DistanceSQ(Projectile.Center) <= (maxdistance * maxdistance)))
                    target = npc;
            }
            return target;
        }

        private readonly int frametime = 6;
        public PrimitiveTrail trail = new();
        public List<Vector2> oldPositions = new List<Vector2>();

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.frameCounter++ >= frametime)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= Main.projFrames[Projectile.type])
                Projectile.frame = 0;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            lightColor = Color.White;

            Color color = Color.Red;

            Vector2 pos = (Projectile.Center).RotatedBy(Projectile.rotation, Projectile.Center);

            oldPositions.Add(pos);
            while (oldPositions.Count > 10)
                oldPositions.RemoveAt(0);

            trail.Draw(color, pos, oldPositions, 1.4f);
            trail.width = 2;

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1f);
                Color ProjColor = new Color(244, 0, 20) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;

                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }
    }
}