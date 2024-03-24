
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class LightningBolt : ModProjectile
    {
        Vector2 loc1 = new Vector2(0, 0);
        Vector2 loc2 = new Vector2(0, 0);

        private const float moving = 60f;
        bool once = false;
        public float setting
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void OnSpawn(IEntitySource source)
        {
            loc1 = Projectile.Center;
            loc2 = new Vector2(loc1.X, loc1.Y + 800);
        }


        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 10;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hide = false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var texture = (Texture2D)ModContent.Request<Texture2D>(Texture);

            var spriteBatch = Main.spriteBatch;

            laser(spriteBatch, texture, new Vector2(loc1.X, loc1.Y),
                    Projectile.velocity, 15, Projectile.damage, -1.57f, 1f, 2000f, Color.White, (int)moving);
            return false;
        }



        public void laser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 t, float state, int dmg, float rotation = 0f, float scale = 1f, float maximun = 1800f, Color col = default(Color), int transitional = 50)
        {
            float rotati = t.ToRotation() + rotation;

            for (float i = transitional; i <= setting; i += state)
            {
                Color c = Color.White;
                var origin = start + i * t;
                
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 24, 26, 16), i < transitional ? Color.Transparent : c, rotati,
                    new Vector2(26 * .5f, 16 * .5f), scale, 0, 0);
            }

            spriteBatch.Draw(texture, start + (setting + state) * t - Main.screenPosition,
                new Rectangle(0, 52, 26, 26), Color.White, rotati, new Vector2(26 * .5f, 26 * .5f), scale, 0, 0);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            //Player player = Main.player[Projectile.owner];
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), loc1,
                loc1 + unit * setting, 22, ref point);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 20;
            int rand = Main.rand.Next(1, 3);
            if (target.boss == false && target.type != NPCID.TargetDummy)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (rand == 1)
                    {
                        target.velocity.X = 10f;
                    }
                    if (rand == 2)
                    {
                        target.velocity.X = -10f;
                    }
                    target.position.Y -= 8;
                }
            }
            
            

        }

        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Update(player);
            laserloc();
            dusts();

            Projectile.position = loc1 + Projectile.velocity * moving;

            //dusts(player);
            lights();
        }

        private void dusts()
        {
            Vector2 t = Projectile.velocity * -1;
            Vector2 position = loc1 + Projectile.velocity * setting;

            if (once == false)
            {
                once = true;
                for (int i = 0; i < 90; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1.5f, 1.5f);
                    var dus = Dust.NewDustPerfect(position, DustID.GemSapphire, speed * 18f, Scale: 2.5f);
                    ;
                    dus.noGravity = true;
                }
            }

        }

        private void laserloc()
        {
            for (setting = moving; setting <= 2200f; setting += 5f)
            {
                var start = loc1 + Projectile.velocity * setting;

                if (!Collision.CanHit(loc1, 1, 1, start, 1, 1))
                {
                    setting -= 5f;
                    break;
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

        private void Update(Player player)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Vector2 difference = loc2 - loc1;
                difference.Normalize();
                Projectile.velocity = difference;
                Projectile.direction = loc2.X > loc1.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            player.heldProj = Projectile.whoAmI;


        }

        private void lights()
        {
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (setting - moving), 26, DelegateMethods.CastLight);
        }

        public override bool ShouldUpdatePosition() => false;
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 t = Projectile.velocity;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + t * setting, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
        }
    }
}