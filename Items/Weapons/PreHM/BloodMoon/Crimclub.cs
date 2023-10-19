using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using RealmOne.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.BloodMoon
{
    public class Crimclub : ModItem
    {
     
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 66;
            Item.crit = 45;
            Item.damage = 32;
            Item.knockBack = 6f;

            Item.useAnimation = 46;
            Item.useTime = 46;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.noMelee = true;


            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<CrimClubSwing>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.GetModPlayer<CrimClubPlayer>().SwingCount++;
            return true;
        }
    }

    public class CrimClubSwing : ModProjectile
    {
        public override string Texture => "RealmOne/Items/Weapons/PreHM/BloodMoon/Crimclub";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
        }
        int i = 0;
        List<Vector2> keypoints = new();
        Vector2 mouse;
        Vector2 vectorToMouse;
        Vector2 mousew;
        ProjKeyFrameHandler keyFrameHandler;
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];

            if (player.GetModPlayer<CrimClubPlayer>().SwingCount % 2 != 0)
                upswing = true;
            else
                upswing = false;

            keyFrameHandler = new(KeyFrameInterpolationCurve.Slerp, "RealmOne/Common/Core/HeldProjPoints/SwingPoints", 25);

            mousew = Main.MouseWorld;
            vectorToMouse = player.Center.DirectionTo(mousew);

            keyFrameHandler.SetAiDefaults(Projectile, player, mousew);
            keypoints = keyFrameHandler.GetPoints(40);

            if ((player.direction == -1 && !upswing) || (player.direction == 1 && upswing))
                i = keypoints.Count - 1;
        }
        bool upswing = false;
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            mouse = player.Center + vectorToMouse;
            keyFrameHandler.SetAiDefaults(Projectile, player, mouse);
            Projectile.Center = keyFrameHandler.CalculateSwordSwingPointsAndApplyRotation(Projectile, mouse, player, keypoints, ref i, new Vector2(-5, -5), upswing);

            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation + MathHelper.Pi + MathHelper.PiOver4 * player.direction);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Utils.RandomVector2(Main.rand, -1f, 1f);
                var d = Dust.NewDustPerfect(Projectile.position, DustID.t_Flesh, speed * 5, Scale: 1f);

                d.noGravity = true;
            }
            if (Main.rand.NextBool(2))
            {
                Vector2 vel = new(Main.rand.NextFloat(-5, 5), -10);
              
                for (int i = 0; i < 3; i++)
                {
                   
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<BloodToothProj>(), Projectile.damage / 2, 0);
                }
            }
        }
    }
    public class CrimClubPlayer : ModPlayer
    {
        public int SwingCount = 0;
    }
    public class BloodToothProj : ModProjectile
    {
        public override string Texture => "RealmOne/Items/Weapons/PreHM/BloodMoon/BloodToothProj";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
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
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.4f);
                Color color = new Color(255, 70, 129) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
        public override void AI()
        {
            Projectile.velocity += new Vector2(0, 0.5f);
            if (Projectile.velocity.Y >= 13)
            {
                Projectile.velocity.Y = 13;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}
