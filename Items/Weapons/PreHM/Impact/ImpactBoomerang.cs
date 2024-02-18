using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Systems;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Impact
{
    public class ImpactBoomerang : ModItem
    {
        public override void SetStaticDefaults()
        {

            Item.ResearchUnlockCount = 1;
            ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Impact/ImpactBoomerang_Glow");

        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.buyPrice(0, 0, 12, 0);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 1;
            Item.shoot = ModContent.ProjectileType<ImpactBoomerangProj>();
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 14f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(Mod, "ImpactTech", 10);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Impact/ImpactBoomerang_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.LightCyan,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }


    }
    internal class ImpactBoomerangProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.scale = 1f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(rorAudio.SawbladeRev, Projectile.position);

            for (int i = 0; i < 5; i++)
            {
                SparkleParticle sparkle = new(Color.LightSkyBlue, 1, new Vector2(Projectile.Center.X + Main.rand.Next(-30, 30), Projectile.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

                ParticleSystem.GenerateParticle(sparkle);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            return false;
        }
        public override void AI()
        {


            float maxDetectRadius = 300;
            float projSpeed = 16;

            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;

            Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        private Player player => Main.player[Projectile.owner];

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
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 0.5f);
                Color color = new Color(30, 183, 237) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
        public override void OnKill(int timeLeft)
        {

        }
    }
}

