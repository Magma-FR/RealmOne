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
using RealmOne.Items.Weapons.PreHM.Jungle;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.Graphics.Shaders;

namespace RealmOne.Items.Sets.SunflowerSet
{
    public class SuperSunflowerBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 44;
            Item.damage = 11;
            Item.useTime = 16;
            Item.useAnimation = 16;
         

            Item.autoReuse = true;
            Item.useTurn = true;
           
            Item.DamageType = DamageClass.Ranged;

            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
          
            Item.UseSound = SoundID.Item5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 20);

            Item.shoot = ProjectileType<SunflowerArrow>();
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Arrow;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<SunflowerArrow>(); // or ProjectileID.FireArrow;
            if (Main.rand.NextBool(2))
            {
               type = ModContent.ProjectileType<SunPetal>();
            }
        }

    }
    public class SunflowerArrow : ModProjectile
    {
     
		public override void SetStaticDefaults()
		{
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 13;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 600;
            Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
        }
        public override void OnKill(int timLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit3, Projectile.Center);

            for (int k = 0; k < 18; k++)
            {
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d.noGravity = true;

                var d1 = Dust.NewDustPerfect(Projectile.Center, DustID.TreasureSparkle, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d1.shader = GameShaders.Armor.GetSecondaryShader(100, Main.LocalPlayer);
            }
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
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.2f);
                Color color = new Color(244, 204, 39) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}
