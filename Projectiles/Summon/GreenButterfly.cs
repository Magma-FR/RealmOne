using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Summons;
using RealmOne.Common.Systems;
using RealmOne.Items.Others;
using RealmOne.RealmPlayer;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Summon
{
    public class GreenButterfly : ModProjectile //This is the main butterfly out of the 4. It controls spawning the other ones and terminating them on certain criteria.
    {
        Vector2 IdlePos; //Everything here is explained in AI

        int StopAi = 0;
        bool Returning = true;
        int IdlePosCD = 0;

        int SpawnCD = 5;


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 2f;
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }





        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner]; //self explanatory
            float overlapVelocity = 0.04f; //flying minions need this

            if (SpawnCD > 0)
            {
                SpawnCD--;
            }
            if (SpawnCD == 1) //here we set the damage and summon the other butterflies after a 5 frame delay. Why we summon after a delay is because you can still spawn a minion for a few frames even if you dont have enough slots. This would cause the green butterfly to not appear, but these three to appear. Delay fixes this
            {
                int BlueDMG = (int)Math.Ceiling(Projectile.damage * 1.2);
                int YellowDMG = (int)Math.Ceiling(Projectile.damage * 1.5);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, new Vector2(0f, -1f), ModContent.ProjectileType<YellowButterfly>(), YellowDMG, Projectile.knockBack, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, new Vector2(0f, -1f), ModContent.ProjectileType<PinkButterfly>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.player[Projectile.owner].Center, new Vector2(0f, -1f), ModContent.ProjectileType<BlueButterfly>(), BlueDMG, Projectile.knockBack, Main.myPlayer, (int)Projectile.whoAmI);
            }

            //VV check if the buff exists, if the player died or is inactive. If so, remove the minions.
            if (p.dead || !p.active)
            {
                p.ClearBuff(ModContent.BuffType<Butterflies>());
            }
            if (p.HasBuff(ModContent.BuffType<Butterflies>()))
            {
                Projectile.timeLeft = 2;
            }

            //examplemod makes minions look difficult.. nah imma do my own thing

            //flutter!
            //some timers for certain events
            if (StopAi > 1) // if the stopai is more than zero, make it not do dmg and ascend before continuing with the normal ai
            {
                Projectile.friendly = false;
                Projectile.velocity.Y = -5f;
                StopAi--;

            }
            if (StopAi == 1)
            {
                StopAi--;
                Projectile.friendly = true;
            }
            if (IdlePosCD > 0)
            {
                IdlePosCD--;
            }


            Projectile.frameCounter++;
            if (Projectile.frameCounter > 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            //neat visuals
            Dust.NewDustPerfect(Projectile.Center, DustID.GreenFairy, new Vector2(0, 0), Alpha: 120, default, Scale: 0.45f); 
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2; //Our butterfly is turned 90 degrees because our sprite faces upwards, it needs to be sideways.
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

            float speed = 4f; //set the speed of the minion
            float inertia = 60f; //kinda works like tilting speed or how precise the minions movement is, the less the value the more precise it is. The higher the value, the higher the chance to overshoot its targets (if correctly used can make the minion smooth!)

            //smooth movement
            NPC npc = FindClosestNPC(600);
            if (npc == null)
            {
                ButterflyFlutter(p, speed, inertia); //if npc wasnt found, continue normal ai
                return;
            }
            else
            {
                if (npc.CanBeChasedBy())
                {
                    if (Collision.CanHitLine(Projectile.Center, 14, 14, npc.Center, npc.width, npc.height) && !Returning) // if the path to the npc has some tile in the way
                    {
                        Attack(npc, 15f, inertia); //hostile npc found, attack
                    }
                    else //if butterfly is returning (in ButterflyFlutter) do not let it start an attack
                    {
                        ButterflyFlutter(p, speed, inertia); // if npc was found but its obstructed, continue normal ai
                    }
                }
                else
                {
                    ButterflyFlutter(p, speed, inertia); // if npc was found but its a friendly one, continue normal ai
                }

            }
        }

        public void Attack(NPC npc, float speed, float inertia)
        {
            Vector2 direction = npc.Center - Projectile.Center; //here we set a line which has 2 dots at the start and the end. Dot 1 is our target, and dot 2 is our butterfly. The line inbetween is its path to the target 
            direction.Normalize(); //math stuff to make it more precise
            direction *= speed; //self explanatory, set the speed

            Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia; //here we push the butterfly towards the npc with our direction so it goes the correct direction and with inertia to make it slightly overshoot so it looks better ingame
            //very useful in minion making since minions tend to do this to add more 'life' into the game, rather than locking onto locations and velocity that makes it look inhumane
        }

        public void ButterflyFlutter(Player p, float speed, float inertia)
        {
            //idle!
            if (IdlePosCD == 0)
            {
                IdlePosCD = 10;
                IdlePos = new Vector2(p.Center.X + Main.rand.Next(-40, 40), p.Center.Y - 60 - Main.rand.Next(-30, 30)); //set to a random location around the player, 1 = 1 pixel, 16 pixels = 1 tile aka our range rn is 2 tiles to left, right, up, and down
                //we also first go 3 tiles above the player, so it doesnt flutter around the center of the player but rather on its own center
                //remember to always set the min to the same value but negative so it can go both directions, setting the X velocity to Main.rand.Next(0, 40) would make it randomize between player and a random amount to the right
                IdlePos = IdlePos - Projectile.Center; // making another line with dots for later use
            }

            if (Vector2.Distance(p.Center, Projectile.Center) < 600) //if the npc is within 50~ tiles of us, make it slow down and have alot of overshootage for smooth
            {
                speed = 6f;
                inertia = 100f;
            }
            else if (Vector2.Distance(p.Center, Projectile.Center) > 600 && !Returning)//if further than 50~ tiles, make it return to the player at a rapid rate while ignoring tiles and npcs
            {
                Returning = true; //this bool causes this event to have priority, so we check the attack function with this bool to prevent the butterfly from starting an attack
                Projectile.tileCollide = false;
                speed = 12f;
                inertia = 100f;
            }

            if (Vector2.Distance(p.Center, Projectile.Center) < 10) //when back close, allow it to deal dmg to npcs, get blocked by tiles and start an attack
            {
                Returning = false;
                Projectile.friendly = true;
                Projectile.tileCollide = true;
            }



            if (Vector2.Distance(IdlePos, Projectile.Center) > 20) //if the butterfly is more than 1.5 tiles away from us, make it focus on returning
            {
                IdlePos.Normalize();
                IdlePos *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + IdlePos) / inertia;
            }
            /*else if (Projectile.velocity == Vector2.Zero) this is a test line from examplemod, sometimes the npc would stop in the air. That cant happen with our minion but if you want to you can take the precaution 
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }*/
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
            if (StopAi == 0)
            {
                StopAi = 20; // upon hitting a npc, set the stopai to exactly 1 second (60 terraria frames) this causes it to focus on fluttering away for a bit before resuming its AI tasks
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

    }
}
