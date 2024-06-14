
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Summons;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Summon
{
    public class BabyVulture : ModProjectile
    {
        public Vector2 TheLoc;
        public Vector2 randomLoc;
        public Vector2 DiveLoc;
        public Vector2 loc;
        public int move = 0;

        public bool Returning = false;
        public int StopAi = 0;
        public bool Sitting = false;
        public bool Dive = false;
        public bool AiSwitch = false;

        public int frameY = 20; //0, 20, 72, 130, 188, 250, 300

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 43;

            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.scale *= 0.85f;
            Projectile.frame = 1;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            SpriteEffects spriteEffects = SpriteEffects.None;
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Main.EntitySpriteDraw(texture, Projectile.position, texture.Frame(1, 6), Color.White, Projectile.rotation, texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);

            // It's important to return false, otherwise we also draw the original texture.
            return true;
        }


        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner];
            float overlapVelocity = 0.04f;

            if (p.dead || !p.active)
            {
                p.ClearBuff(ModContent.BuffType<BabyVultures>());
            }
            if (p.HasBuff(ModContent.BuffType<BabyVultures>()))
            {
                Projectile.timeLeft = 2;
            }

            if (move > 0)
            {
                move--;
            }
            if (StopAi > 1)
            {
                Projectile.friendly = false;
                Projectile.velocity.Y = -7f;
                StopAi--;

            }
            if (StopAi == 1)
            {
                StopAi--;
                Projectile.friendly = true;
            }

            if (Dive)
            {
                Projectile NPC = Projectile;
                Vector2 vec = new Vector2(NPC.Center.X - 15, NPC.position.Y) + Vector2.Normalize(NPC.velocity) * 10f;
                Dust dusted = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud)];
                dusted.scale = 0.3f;
                dusted.position = vec;
                dusted.velocity = NPC.velocity.RotatedBy(1.5707963705062866) * 0.33f + NPC.velocity / 4f;
                dusted.position += NPC.velocity.RotatedBy(1.5707963705062866);
                dusted.fadeIn = 0.5f;
                dusted.noGravity = true;
                Vector2 vec1 = new Vector2(NPC.Center.X + 15, NPC.position.Y) + Vector2.Normalize(NPC.velocity) * 10f;
                Dust ddusted = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Cloud)];
                ddusted.scale = 0.3f;
                ddusted.position = vec1;
                ddusted.velocity = NPC.velocity.RotatedBy(-1.5707963705062866) * 0.33f + NPC.velocity / 4f;
                ddusted.position += NPC.velocity.RotatedBy(-1.5707963705062866);
                ddusted.fadeIn = 0.5f;
                ddusted.noGravity = true;
            }

            if (Projectile.frame != 0)
            {
                if (++Projectile.frameCounter >= 4)
                {
                    Projectile.frameCounter = 0;
                    // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                    if (++Projectile.frame >= Main.projFrames[Projectile.type])
                        Projectile.frame = 1;
                }
            }

           

            Projectile.rotation = Projectile.velocity.X * 0.05f;
            float speed = 10f;
            float inertia = 30f;


            NPC npc = FindClosestNPC(600);
            if (npc == null)
            {
                Idle(p, speed, inertia);
                return;
            }
            else
            {
                if (npc.CanBeChasedBy())
                {
                    Attack(npc, 10f, inertia);
                }
                else
                {
                    Idle(p, speed, inertia);
                }

            }
        }

        public void Attack(NPC npc, float speed, float inertia)
        {
            if (AiSwitch)
            {
                AiSwitch = false;
                Projectile.frame = 1;
            }

            if (Projectile.position.X < npc.position.X)
            {
                Projectile.spriteDirection = -1;
            }
            else if (Projectile.position.X >= npc.position.X)
            {
                Projectile.spriteDirection = 1;
            }



            if (!Dive)
            {
                DiveLoc = new Vector2(npc.Center.X, npc.Center.Y - 200) - Projectile.Center;
                DiveLoc.Normalize();
                DiveLoc *= speed;
                if (Vector2.Distance(new Vector2(npc.Center.X, npc.Center.Y - 200), Projectile.Center) < 45)
                {
                    Dive = true;
                }

            }
            else
            {
                DiveLoc = npc.Center - Projectile.Center;
                DiveLoc.Normalize();
                DiveLoc *= speed * 2;
            }



            Projectile.velocity = (Projectile.velocity * (inertia - 1) + DiveLoc) / inertia;
        }

        public void Idle(Player p, float speed, float inertia)
        {
           
            //idle!

            if (Main.rand.Next(600) < 1)
            {
                SoundEngine.PlaySound(SoundID.NPCHit28 with { Pitch = 0.25f, PitchVariance = 0.5f }, Projectile.Center);
            }

            if (Sitting)
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
                if (Projectile.position.X < p.position.X)
                {
                    Projectile.spriteDirection = 1;
                }
                else if (Projectile.position.X >= p.position.X)
                {
                    Projectile.spriteDirection = -1;
                }
            }
            else
            {
                if (Projectile.position.X < p.position.X)
                {
                Projectile.spriteDirection = -1;
                }
                else if (Projectile.position.X >= p.position.X)
                {
                Projectile.spriteDirection = 1;
                }
            }

            if (Vector2.Distance(p.Center, Projectile.Center) > 200 && Sitting)
            {
                if (AiSwitch)
                {
                    AiSwitch = false;
                    Projectile.frame = 1;
                }
                Sitting = false;
                StopAi = 1;
            }

            if (Vector2.Distance(p.Center, Projectile.Center) < 600) // Idle Chill Speed
            {
                speed = 6f;
                inertia = 50f;
            }
            else if (Vector2.Distance(p.Center, Projectile.Center) > 600) // Catch up (prioritzed)
            {
                Returning = true;
                speed = 14f;
                inertia = 50f;
            }


            if (Vector2.Distance(p.Center, Projectile.Center) < 200) //Remove Catching up state
            {
                if (Returning)
                {
                    Returning = false;
                    Projectile.friendly = true;
                    Projectile.tileCollide = true;
                }
            }

            if (Vector2.Distance(p.Center, Projectile.Center) < 150)
            {
                if (move == 0)
                {
                    move = 30;
                    loc = new Vector2(p.Center.X + Main.rand.Next(-150, 150), p.Center.Y);
                }
                TheLoc = loc - Projectile.Center;
                if (Collision.CanHitLine(Projectile.Center, Projectile.width, Projectile.height, new Vector2(Projectile.Center.X, Projectile.Center.Y + 16), Projectile.width, Projectile.height))
                {
                    loc.Y += 16;
                }
                else
                {
                    if (!Sitting)
                    {
                        Sitting = true;
                        Projectile.position.Y += 8;
                        Projectile.frame = 0;
                        AiSwitch = true;
                    }
                }
            }
            else
            {
                TheLoc = new Vector2(p.Center.X, p.Center.Y - 80) - Projectile.Center;
            }

            /*if (move == 0)
            {
                move = 45;
                randomLoc = new Vector2(p.Center.X + Main.rand.Next(-50, 50), p.Center.Y - 80 + Main.rand.Next(-50, 50));
            }*/

            if (!Sitting)
            {
                TheLoc.Normalize();
                TheLoc *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + TheLoc) / inertia;
            }
        }


        public NPC FindClosestNPC(float maxDetectDistance) //self explanatory
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.NPCHit28 with { Pitch = 0.25f, PitchVariance = 0.5f }, Projectile.Center);
            if (Dive)
            {
                Dive = false;
            }
            if (StopAi == 0)
            {
                Projectile.friendly = false;
                StopAi = 30;
            }
        }
        
    }
}
