using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using RealmOne.Items.Misc.Plants;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Sets.OrchidSet
{
    public class OrchidBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 44;
            Item.damage = 7;
            Item.useAnimation = 24;
            Item.useTime = 8;
            Item.reuseDelay = 20;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.DamageType = DamageClass.Ranged;

            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Green;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.UseSound = SoundID.Item5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 20);

            Item.shoot = ProjectileType<OrchidArrow>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<OrchidArrow>(); // or ProjectileID.FireArrow;
            if (Main.rand.NextBool(8))
            {
                type = ModContent.ProjectileType<OrchidCross>();
            }
        }

        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<OrchidEssence>(), 10)
            .AddIngredient(ItemID.WoodenBow, 1)
            .AddIngredient(ItemID.Daybloom, 1)
            .AddTile(TileID.WorkBenches)

            .Register();
        }
    }

    public class OrchidArrow : ModProjectile
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
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.PinkFairy, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d.noGravity = true;

                var d1 = Dust.NewDustPerfect(Projectile.Center, DustID.GreenFairy, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d1.shader = GameShaders.Armor.GetSecondaryShader(100, Main.LocalPlayer);

                var d2 = Dust.NewDustPerfect(Projectile.Center, DustID.YellowTorch, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d2.shader = GameShaders.Armor.GetSecondaryShader(100, Main.LocalPlayer);
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
                Color color = new Color(30, 209, 39) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }

    public class OrchidCross : ModProjectile
    {
        private int cd = 100;

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
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 600;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0f);

            var d = Dust.NewDustPerfect(Projectile.Center, DustID.TreasureSparkle, new Vector2(0, 0), Scale: 1f);
            d.noGravity = true;

            if (cd > 0)
            {
                cd--;
            }

            if (cd == 0)
            {
                NPC closestNPC = FindClosestNPC(1000);
                if (closestNPC == null)
                    return;

                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 3f;
                Projectile.friendly = true;
            }
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

        public PrimitiveTrail trail = new();
        public List<Vector2> oldPositions = new List<Vector2>();

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            lightColor = Color.White;

            Color color = Color.Gold;

            Vector2 pos = (Projectile.Center).RotatedBy(Projectile.rotation, Projectile.Center);

            oldPositions.Add(pos);
            while (oldPositions.Count > 30)
                oldPositions.RemoveAt(0);

            trail.Draw(color, pos, oldPositions, 1.4f);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Effects/GlowLight").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 1.6f);
                Color ProjColor = new Color(244, 204, 39) * (1f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;

                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)

        {
            SoundEngine.PlaySound(SoundID.Item35, Projectile.position);
            Player p = Main.player[Projectile.owner];
            int healingAmount = damageDone / 5; //decrease the value 30 to increase heal, increase value to decrease. Or you can just replace damage/x with a set value to heal, instead of making it based on damage.
            p.statLife += healingAmount;
            p.HealEffect(healingAmount, true);

            // KillOldestJavelin will kill the oldest projectile stuck to the specified npc.
            // It only works if ai[0] is 1 when sticking and ai[1] is the target npc index, which is what IsStickingToTarget and TargetWhoAmI correspond to.
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 18; k++)
            {
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d.noGravity = true;

                var d1 = Dust.NewDustPerfect(Projectile.Center, DustID.TreasureSparkle, Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(5), 0, default, 0.75f);
                d1.shader = GameShaders.Armor.GetSecondaryShader(100, Main.LocalPlayer);
            }
            SoundEngine.PlaySound(SoundID.Item35, Projectile.position);

            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("CrossGore").Type, 1f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }
    }
}