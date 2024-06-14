using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
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
    public class BlueButterfly : ModProjectile
    {
        Vector2 IdlePos; //Everything here is explained in AI

        int StopAi = 0;
        bool Manastealed = false;
        int Manamount = 0;
        bool Returning = true;
        int IdlePosCD = 0;

        bool YellowDead = false;
        bool PinkDead = false;
        bool GreenDead = false;

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
            //Projectile.minionSlots = 0f;
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }


        public override void OnKill(int timeLeft)
        {

            //since the 3 extra butterflies do not consume minion slots (or else if they did all take 0.5 each, they could be replaceable one by one, which would cause you to be able to have ex. 1 pink and 1 green and a desert tiger.
            //we check this here since these minions do not consume slots, so they will be the first ones to get replaced. So make sure the whole bunch disappears if one does.
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].type == ModContent.ProjectileType<GreenButterfly>())
                {
                    Main.projectile[i].Kill();
                }
            }
        }



        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player p = Main.player[Projectile.owner]; //self explanatory
            float overlapVelocity = 0.04f; //flying minions need this

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

            float speed = 4f; //set the speed of the minion
            float inertia = 60f; //kinda works like tilting speed or how precise the minions movement is, the less the value the more precise it is. The higher the value, the higher the chance to overshoot its targets (if correctly used can make the minion smooth!)

            if (Manastealed)
            {
                if (IdlePosCD == 0)
                {
                    IdlePosCD = 5; //extremely fast location setter bc our butterfly is now alot faster to make it look like a special effect
                    IdlePos = new Vector2(p.Center.X + Main.rand.Next(-40, 40), p.Center.Y - Main.rand.Next(-30, 30));                                                                                                  //remember to always set the min to the same value but negative so it can go both directions, setting the X velocity to Main.rand.Next(0, 40) would make it randomize between player and a random amount to the right
                    IdlePos = IdlePos - Projectile.Center;
                }
                //custom dust while boosting

                IdlePos.Normalize();
                if (Vector2.Distance(p.Center, Projectile.Center) < 90)
                {
                    SoundEngine.PlaySound(SoundID.Item29, Projectile.position);
                    Manastealed = false;
                    if (p.statMana + Manamount < p.statManaMax2)
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                            Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.BlueFairy, speedd * 9, Scale: 1f);
                            dust1.noGravity = true;
                        }
                        p.statMana += Manamount;
                    }
                    else
                    {
                        p.statMana = p.statManaMax2;

                    }
                    CombatText.NewText(p.getRect(), new Color(0, 52, 231), $"+{Manamount}", false, false);

                }
                else
                {
                    Dust.NewDustPerfect(Projectile.Center, DustID.BlueFairy, new Vector2(0, 0), Alpha: 120, default, Scale: 0.45f);
                    IdlePos *= speed;
                }
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + IdlePos) / inertia;
            }
            else
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.BlueFairy, new Vector2(0, 0), Alpha: 120, default, Scale: 0.45f);
            }

            //neat visuals
            Dust.NewDustPerfect(Projectile.Center, DustID.BlueFairy, new Vector2(0, 0), Alpha: 120, default, Scale: 0.45f);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);



            //smooth movement
            NPC npc = FindClosestNPC(600);
            if (npc == null)
            {
                ButterflyFlutter(p, speed, inertia); //if npc wasnt found, continue normal ai
                return;
            }
            else
            {
                if (npc.friendly == false)
                {
                    if (Collision.CanHitLine(Projectile.Center, 14, 14, npc.Center, npc.width, npc.height) && !Returning && !Manastealed) // if the path to the npc has some tile in the way
                    {
                        Attack(npc, 15f, inertia); //hostile npc found, attack
                    }
                    else //if butterfly is returning (in ButterflyFlutter) or it is returning to the player to give the mana, do not let it start an attack
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
            if (!Manastealed) //Avoid idling when returning to the player for mana
            {
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
            Player p = Main.player[Projectile.owner];
            if (Main.rand.Next(100) < 25 && p.statMana < p.statManaMax2)
            {
                Manastealed = true;
                Manamount = hit.Damage / 2;
            }

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
