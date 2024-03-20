using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Debuffs.ShockStacks;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Magic;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
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
            Item.width = 34;
            Item.height = 34;
            Item.crit = 20;
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

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<ImpactBoomerangProj>()] < 1;
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
        bool goingToLoc = false;
        int hits = 0;
        bool hitGround = false;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            hits = 0;
            goingToLoc = false;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
        }
        /*public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
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
        }*/
        public override void AI()
        {

            Main.player[Projectile.owner].itemTime = 12;
            Main.player[Projectile.owner].itemAnimation = 12;
            Projectile.timeLeft = 20;

            if (hitGround == false)
            {
                if (hits < 3)
                {
                    Projectile.aiStyle = 3;
                    NPC npc = FindClosestNPC(600);
                    if (npc == null)
                    {
                        return;
                    }
                    else
                    {
                        Projectile.aiStyle = 2;
                        if (goingToLoc == false)
                        {
                            Projectile.friendly = false;
                        }

                        //Projectile.rotation += 0.4f;
                        Vector2 OnTop = new Vector2(npc.Center.X, npc.Center.Y - 300);
                        Vector2 direction = (Projectile.Center - OnTop).SafeNormalize(Vector2.UnitX);
                        Projectile.velocity = (OnTop - Projectile.Center).SafeNormalize(Vector2.Zero) * 14f;
                        if (Vector2.Distance(OnTop, Projectile.Center) < 10 && goingToLoc == false)
                        {
                            goingToLoc = true;
                        }

                        if (goingToLoc == true)
                        {
                            Projectile.friendly = true;
                            Projectile.velocity = (npc.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 14f;
                        }
                    }



                    
                }
                else if (hits >= 3)
                {
                    //Projectile.aiStyle = 3;
                    Projectile.velocity = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 14f;
                    if (Projectile.Colliding(Projectile.getRect(), Main.player[Projectile.owner].getRect()))
                    {
                        Projectile.Kill();
                        hits = 0;
                    }

                }
            }
            else
            {
                Projectile.tileCollide = false;
                Projectile.velocity = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 14f;
                if (Projectile.Colliding(Projectile.getRect(), Main.player[Projectile.owner].getRect()))
                {
                    Projectile.Kill();
                    hits = 0;
                }
            }


        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            hitGround = true;
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(rorAudio.SawbladeRev, Projectile.position);
            if (goingToLoc == true)
            {
                hits++;
            }
            goingToLoc = false;
            if (target.HasBuff<Shocked5>() && !target.HasBuff<Shocked6>())
            {
                target.AddBuff(ModContent.BuffType<Shocked5>(), 150);
            }
            else if (target.HasBuff<Shocked4>() && !target.HasBuff<Shocked6>())
            {
                target.AddBuff(ModContent.BuffType<Shocked4>(), 150);
            }
            else if (target.HasBuff<Shocked3>() && !target.HasBuff<Shocked6>())
            {
                target.AddBuff(ModContent.BuffType<Shocked3>(), 150);
            }
            else if (target.HasBuff<Shocked2>() && !target.HasBuff<Shocked6>())
            {
                target.AddBuff(ModContent.BuffType<Shocked2>(), 150);
            }
            else if (target.HasBuff<Shocked>() && !target.HasBuff<Shocked6>() || !target.HasBuff<Shocked>() && !target.HasBuff<Shocked6>())
            {
                target.AddBuff(ModContent.BuffType<Shocked>(), 150);
            }
            for (int i = 0; i < 90; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(2.5f, 2.5f);
                var dus = Dust.NewDustPerfect(Projectile.Center, DustID.GemSapphire, speed * 5f, Scale: 2.5f);
                ;
                dus.noGravity = true;
            }
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Vector2.Distance(target.Center, Main.npc[i].Center) < 225 && Main.npc[i] != target)
                {
                    if (Main.npc[i].HasBuff<Shocked5>() && !Main.npc[i].HasBuff<Shocked6>())
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<Shocked5>(), 90);
                    }
                    else if (Main.npc[i].HasBuff<Shocked4>() && !Main.npc[i].HasBuff<Shocked6>())
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<Shocked4>(), 90);
                    }
                    else if (Main.npc[i].HasBuff<Shocked3>() && !Main.npc[i].HasBuff<Shocked6>())
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<Shocked3>(), 90);
                    }
                    else if (Main.npc[i].HasBuff<Shocked2>() && !Main.npc[i].HasBuff<Shocked6>())
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<Shocked2>(), 90);
                    }
                    else if (Main.npc[i].HasBuff<Shocked>() && !Main.npc[i].HasBuff<Shocked6>() || !Main.npc[i].HasBuff<Shocked>() && !Main.npc[i].HasBuff<Shocked6>())
                    {
                        Main.npc[i].AddBuff(ModContent.BuffType<Shocked>(), 90);
                    }
                }
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
    }
}

