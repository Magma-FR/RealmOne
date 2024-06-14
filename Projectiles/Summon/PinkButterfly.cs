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
    public class PinkButterfly : ModProjectile
    {
        Vector2 IdlePos; //Everything here is explained in AI

        int StopAi = 0;
        bool Returning = true;
        int IdlePosCD = 0;

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

        public override bool MinionContactDamage()
        {
            return false; //Healers stay at the player, increasing passive regeneration by 1 per butterfly.
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
            //timer
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
            Dust.NewDustPerfect(Projectile.Center, DustID.PinkFairy, new Vector2(0, 0), Alpha: 120, default, Scale: 0.45f);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

            float speed = 4f; //set the speed of the minion
            float inertia = 60f; //kinda works like tilting speed or how precise the minions movement is, the less the value the more precise it is. The higher the value, the higher the chance to overshoot its targets (if correctly used can make the minion smooth!)

            //smooth movement
            ButterflyFlutter(p, speed, inertia); //This butterfly cannot attack. It chills around the player, passively increasing regeneration by 1.
        }

        public void ButterflyFlutter(Player p, float speed, float inertia)
        {
            //idle!
            if (IdlePosCD == 0)
            {
                IdlePosCD = 10;
                IdlePos = new Vector2(p.Center.X + Main.rand.Next(-20, 20), p.Center.Y - Main.rand.Next(-20, 20)); //set to a random location around the player, 1 = 1 pixel, 16 pixels = 1 tile aka our range rn is 2 tiles to left, right, up, and down
                //we also first go 3 tiles above the player, so it doesnt flutter around the center of the player but rather on its own center
                //remember to always set the min to the same value but negative so it can go both directions, setting the X velocity to Main.rand.Next(0, 40) would make it randomize between player and a random amount to the right
                IdlePos = IdlePos - Projectile.Center; // making another line with dots for later use
            }

            if (Vector2.Distance(p.Center, Projectile.Center) < 600 && !Returning) //if the npc is within 15~ tiles of us, make it slow down and have alot of overshootage for smooth
            {
                speed = 6f;
                inertia = 100f;
            }
            else if (Vector2.Distance(p.Center, Projectile.Center) > 600 && !Returning)//if further than 15~ tiles, make it return to the player at a rapid rate while ignoring tiles and npcs
            {
                Returning = true; //this bool causes this event to have priority, so we check the attack function with this bool to prevent the butterfly from starting an attack
                Projectile.friendly = false;
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

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

    }
}
