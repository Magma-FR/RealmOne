using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Summons;
using RealmOne.Common.Global;
using RealmOne.Common.Systems;
using RealmOne.Items.Others;
using RealmOne.Items.Weapons.Summoner;
using RealmOne.Projectiles.Other;
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
    public class OrchidBloom : ModProjectile
    {
        //
        // Orchid Petal (the proj) also has documentation!
        //

        int Bobbing = 0;
        bool BobbingDown = false;
        bool Attack = false;
        float acc = 0f;
        bool Decreasation = false;
        int AtkCD = 0;

        NPC npcc; //Our target, used in another code part so we have to save it in the projectile
        Item theItem; //Same with this, an item parameter used later in the code

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;

            Projectile.friendly = false;
            Projectile.minion = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.hide = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player p = Main.player[Projectile.owner];
            for (int i = 0; i < 58; i++)
            {
                Item item = p.inventory[i];
                if (item.type == ModContent.ItemType<OrchidBloomStave>()) //Find our stave from our inventory
                {
                    theItem = item; //Here we set our stave to a variable for later use
                    break;
                }
            }

            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 0) 
            {
                if (p.ownedProjectileCounts[ModContent.ProjectileType<OrchidBloom>()] > 0 && p.slotsMinions + 1f < p.maxMinions) //Check if there exists a flower already and if there is minion slots for an upgrade
                {
                    if (theItem.GetGlobalItem<GlobalItemList>().BloomStack < 3) //This is how the minion stacking works. Since there can be only one active Orchid Flower at a time, reusing it will empower it. Note that this only works on minion classes that dont spawn more of it upon reuse.
                    {
                        for (int c = 0; c < Main.maxProjectiles; c++) //Find the active flower
                        {
                            if (Main.projectile[c].type == ModContent.ProjectileType<OrchidBloom>() && Main.projectile[c].whoAmI != Projectile.whoAmI && Main.projectile[c].owner == p.whoAmI) //First, from all the projs it checks all orchid flowers. Then it checks the one that isnt the just spawned one, which means that is our flower. Then we make sure to check that the owner is us so in multiplayer this wont break
                            {
                                Main.projectile[c].minionSlots = 2f; //Increase the slot weight of the main flower depending on upgrade
                                if (theItem.GetGlobalItem<GlobalItemList>().BloomStack < 2) //SFX change depending on phase
                                {
                                    SoundEngine.PlaySound(SoundID.Item29, Main.projectile[c].Center);
                                    for (int i = 0; i < 50; i++) //Some VFX
                                    {
                                        Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                                        Dust dust1 = Dust.NewDustPerfect(Main.projectile[c].Center, DustID.PinkCrystalShard, speedd * 8, Scale: 1f);
                                        dust1.noGravity = true;
                                    }
                                }
                                else
                                {
                                    SoundEngine.PlaySound(SoundID.Item15, Main.projectile[c].Center);
                                    for (int i = 0; i < 50; i++) //Some VFX
                                    {
                                        Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                                        Dust dust1 = Dust.NewDustPerfect(Main.projectile[c].Center, DustID.YellowStarDust, speedd * 8, Scale: 1f);
                                        dust1.noGravity = true;
                                    }
                                }
                                theItem.GetGlobalItem<GlobalItemList>().BloomStack += 1; //Empowered stage 1 (max stage 3)
                            }
                        }

                    }
                    else if (theItem.GetGlobalItem<GlobalItemList>().BloomStack >= 3)
                    {
                        theItem.GetGlobalItem<GlobalItemList>().BloomStack = 0; //If Empowered past stage 3 (even if no minion slot is empty) reset the projectile and the variable
                        for (int ii = 0; ii < Main.maxProjectiles; ii++)
                        {
                            if (Main.projectile[ii].type == ModContent.ProjectileType<OrchidBloom>() && Main.projectile[ii].whoAmI != Projectile.whoAmI && Main.projectile[ii].owner == p.whoAmI)
                            {
                                Main.projectile[ii].Kill();
                            }
                        }
                    }
                    Projectile.Kill();
                }
                else if (p.ownedProjectileCounts[ModContent.ProjectileType<OrchidBloom>()] == 0)
                {
                    for (int c = 0; c < Main.maxProjectiles; c++)
                    {
                        if (Main.projectile[c].type == ModContent.ProjectileType<OrchidBloom>() && Main.projectile[c].whoAmI != Projectile.whoAmI && Main.projectile[c].owner == p.whoAmI)
                        {
                            Main.projectile[c].Kill();
                        }
                    }
                        
                    theItem.GetGlobalItem<GlobalItemList>().BloomStack = 0;
                }
                else if (p.slotsMinions + 1f + (float)theItem.GetGlobalItem<GlobalItemList>().BloomStack >= p.maxMinions)
                {
                    Projectile.Kill();
                    theItem.GetGlobalItem<GlobalItemList>().BloomStack = 0;

                }
            }
            else if (theItem.GetGlobalItem<GlobalItemList>().BloomStack > 0)
            {
                if (p.ownedProjectileCounts[ModContent.ProjectileType<OrchidBloom>()] > 0 && p.slotsMinions + 1f + (float)theItem.GetGlobalItem<GlobalItemList>().BloomStack < p.maxMinions) //Here we do the same thing, but now we also check for additional minion slots depending on upgrade
                {
                    
                    if (theItem.GetGlobalItem<GlobalItemList>().BloomStack < 3)
                    {
                        for (int c = 0; c < Main.maxProjectiles; c++)
                        {
                            if (Main.projectile[c].type == ModContent.ProjectileType<OrchidBloom>() && Main.projectile[c].whoAmI != Projectile.whoAmI && Main.projectile[c].owner == p.whoAmI)
                            {
                                if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 2)
                                {
                                    Main.projectile[c].minionSlots = 3f; //Increase the slot weight of the main flower depending on upgrade
                                }
                                if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 3)
                                {
                                    Main.projectile[c].minionSlots = 4f;
                                }
                                for (int i = 0; i < 50; i++)
                                {
                                    Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                                    Dust dust1 = Dust.NewDustPerfect(Main.projectile[c].Center, DustID.YellowStarDust, speedd * 8, Scale: 1f);
                                    dust1.noGravity = true;
                                }
                                if (theItem.GetGlobalItem<GlobalItemList>().BloomStack < 2)
                                {
                                    SoundEngine.PlaySound(SoundID.Item29, Main.projectile[c].Center);
                                }
                                else
                                {
                                    SoundEngine.PlaySound(SoundID.Item15, Main.projectile[c].Center);
                                }
                                theItem.GetGlobalItem<GlobalItemList>().BloomStack += 1;
                            }
                        }

                    }
                    else if (theItem.GetGlobalItem<GlobalItemList>().BloomStack >= 3)
                    {
                        theItem.GetGlobalItem<GlobalItemList>().BloomStack = 0;
                        for (int ii = 0; ii < Main.maxProjectiles; ii++)
                        {
                            if (Main.projectile[ii].type == ModContent.ProjectileType<OrchidBloom>() && Main.projectile[ii].whoAmI != Projectile.whoAmI && Main.projectile[ii].owner == p.whoAmI)
                            {
                                Main.projectile[ii].Kill();
                            }
                        }
                    }
                    Projectile.Kill();
                }
                else if (p.ownedProjectileCounts[ModContent.ProjectileType<OrchidBloom>()] == 0)
                {
                    for (int c = 0; c < Main.maxProjectiles; c++)
                    {
                        if (Main.projectile[c].type == ModContent.ProjectileType<OrchidBloom>() && Main.projectile[c].whoAmI != Projectile.whoAmI && Main.projectile[c].owner == p.whoAmI)
                        {
                            Main.projectile[c].Kill();
                        }
                    }
                    theItem.GetGlobalItem<GlobalItemList>().BloomStack = 0;
                }
                else if (p.slotsMinions + 1f + (float)theItem.GetGlobalItem<GlobalItemList>().BloomStack >= p.maxMinions)
                {
                    Projectile.Kill();
                    theItem.GetGlobalItem<GlobalItemList>().BloomStack = 0;

                }
            }

        }

        public override Color? GetAlpha(Color lightColor) // Give it an unique effect based empowered stages
        {
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 3)
            {
                return new Color(161, 0, 164, 255);
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 2)
            {
                return new Color(204, 0, 207, 255);
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 1)
            {
                return new Color(251, 0, 255, 255);
            }

            return base.GetAlpha(lightColor);
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
                p.ClearBuff(ModContent.BuffType<OrchidFlower>());
            }
            if (p.HasBuff(ModContent.BuffType<OrchidFlower>()))
            {
                Projectile.timeLeft = 2;
            }

            if (AtkCD > 0)
            {
                AtkCD--;
            }

            float speed = 7f;
            float inertia = 30f;


            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);



            Vector2 direction;
            if (Bobbing < 8 && !BobbingDown)
            {
                Bobbing++;
            }
            else if (Bobbing == 8)
            {
                BobbingDown = true;
            }

            if (BobbingDown)
            {
                if (Bobbing > -8)
                {
                    Bobbing--;
                }
                if (Bobbing == -8)
                {
                    BobbingDown = false;
                }
            }

            if (Vector2.Distance(Projectile.Center, p.Center) > 200)
            {
                speed *= 2;
            }
            if (Vector2.Distance(Projectile.Center, p.Center) > 600)
            {
                speed *= 3;
            }
            if (Vector2.Distance(Projectile.Center, p.Center) > 900)
            {
                Projectile.Center = p.Center;
            }

            if (p.direction == 1)
            {
                direction = new Vector2(p.Center.X - 30, p.Center.Y - 40 + Bobbing) - Projectile.Center; //Keep moving behind the player, but slightly above
            }
            else
            {
                direction = new Vector2(p.Center.X + 30, p.Center.Y - 40 + Bobbing) - Projectile.Center; //If player is looking left
            }
            if (Vector2.Distance(direction, Projectile.Center) < 10)
            {
                inertia = 5f;
            }
            direction.Normalize();
            direction *= speed;

            Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia; //A little movement so it feels more alive, and not stiff like by setting its position.

            if (!Attack && !Decreasation)
            {
                if (theItem.GetGlobalItem<GlobalItemList>().BloomStack > 0) //Make its idle spin faster if empowered
                {
                    Projectile.rotation += 0.05f * theItem.GetGlobalItem<GlobalItemList>().BloomStack;
                }
                else
                {
                    Projectile.rotation += 0.05f;
                }
            }
            else if (Attack || Decreasation)
            {
                Projectile.rotation += 0.05f + acc; //if there is, add acceleration to the spin
            }

            if (Decreasation)
            {
                if (acc > 0f) //Gives in a fun effect where it slows down to its original speeds.
                {
                    acc -= 0.003f; //0.18f per 1s
                }
                if (acc <= 0f)
                {
                    acc = 0f;
                    Decreasation = false; //Turn slowing the projectile mode off
                }
            }

            if (Attack)
            {
                AtkCD = 145; //cooldown
                Decreasation = false; //while starting to accelerate, make sure to not start decreasing acceleration
                if (!Decreasation && acc < 0.3f) //Start increasing acceleration
                {
                    if (theItem.GetGlobalItem<GlobalItemList>().BloomStack > 0) 
                    {
                        acc += 0.0015f * theItem.GetGlobalItem<GlobalItemList>().BloomStack; //0.06f per 1s, empowered stages boost acceleration
                    }
                    else
                    {
                        acc += 0.0015f;
                    }
                }
                
                if (acc >= 0.3f)
                {
                    //SpawnPetals is a function at the end of the class that spawns all the projectiles. Below you can edit the speed, targets, and the integers.
                    //Two out of 6 will always be a healing petal and defensive petal, which overrides normal AI to instead go for the player
                    //Instead of always spawning the defensive and healing proj on same positions, we add randomness by selecting two of the six projectiles so its randomized each time. We do this by GlobalProjectiles, since randomization otherwise would be messy, but possible in code. OrchidPetal class is also documented
                    int healerProj = Main.rand.Next(1, 7 + theItem.GetGlobalItem<GlobalItemList>().BloomStack); //1-6, wont result 7. Lets add an extra petal per an empowered stage
                    int DefProj = Main.rand.Next(1, 7 + theItem.GetGlobalItem<GlobalItemList>().BloomStack);

                    for (int i = 0; i < 20; i++) //Some VFX
                    {
                        Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                        Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.PinkCrystalShard, speedd * 8, Scale: 1f);
                        dust1.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
                    SpawnPetals(npcc, p, Projectile.damage, healerProj, DefProj, 8f);
                    Decreasation = true; //Start slowing down
                    Attack = false; //activate idle
                }
            }

            //smooth movement
            NPC npc = FindClosestNPC(900);
            if (npc == null)
            {
                return;
            }
            else
            {
                if (npc.CanBeChasedBy())
                {
                    if (AtkCD == 0)
                    {
                        Attack = true; //Activate attack code (above)
                        npcc = npc; //Set the class's target to this npc
                    }
                }

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

        public void SpawnPetals(NPC npc, Player p, int dmg, int healerProj, int DefProj, float speed)
        {
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 1) //Boost dmg based on empowered stages
            {
                dmg = (int)Math.Ceiling(dmg * 1.1);
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 2)
            {
                dmg = (int)Math.Ceiling(dmg * 1.2);
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 3)
            {
                dmg = (int)Math.Ceiling(dmg * 1.3);
            }
            Vector2 dir = (npc.Center - p.Center).SafeNormalize(Vector2.UnitX); //Set the line with dots (A, B) so it has a velocity
            Vector2 vel1 = dir.RotatedBy(MathHelper.ToRadians(0)); //Adjust velocity with radians to get perfectly angled 6 projectiles. The randomness of the healing and defensive make it look different each time
            Vector2 vel2 = dir.RotatedBy(MathHelper.ToRadians(60));
            Vector2 vel3 = dir.RotatedBy(MathHelper.ToRadians(120));
            Vector2 vel4 = dir.RotatedBy(MathHelper.ToRadians(180));
            Vector2 vel5 = dir.RotatedBy(MathHelper.ToRadians(240));
            Vector2 vel6 = dir.RotatedBy(MathHelper.ToRadians(300));

            Vector2 vel7 = dir.RotatedBy(MathHelper.ToRadians(30)); //Extra petals for each empowered stage
            Vector2 vel8 = dir.RotatedBy(MathHelper.ToRadians(90));
            Vector2 vel9 = dir.RotatedBy(MathHelper.ToRadians(270));
            if (healerProj == 1)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel1 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true; //As you can see, projectiles bools are set globally here. Notice that in GlobalProjectiles, its instanced per entity so it wont set it for each active projectile.
            }
            else if (DefProj == 1 && healerProj != 1)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel1 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel1 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
            }
            if (healerProj == 2)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel2 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
            }
            else if (DefProj == 2 && healerProj != 2)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel2 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel2 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
            }
            if (healerProj == 3)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel3 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
            }
            else if (DefProj == 3 && healerProj != 3)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel3 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel3 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
            }
            if (healerProj == 4)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel4 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
            }
            else if (DefProj == 4 && healerProj != 4)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel4 * speed,  ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel4 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
            }
            if (healerProj == 5)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel5 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
            }
            else if (DefProj == 5 && healerProj != 5)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel5 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel5 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
            }
            if (healerProj == 6)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel6 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
            }
            else if (DefProj == 6 && healerProj != 6)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel6 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
            }
            else
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel6 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack >= 1)
            {
                if (healerProj == 7)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel7 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                    Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
                }
                else if (DefProj == 7 && healerProj != 7)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel7 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                    Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
                }
                else
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel7 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                }
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack >= 2)
            {
                if (healerProj == 8)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel8 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                    Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
                }
                else if (DefProj == 8 && healerProj != 8)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel8 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                    Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
                }
                else
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel8 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                }
            }
            if (theItem.GetGlobalItem<GlobalItemList>().BloomStack == 3)
            {
                if (healerProj == 9)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel9 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                    Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().HealingPetal = true;
                }
                else if (DefProj == 9 && healerProj != 9)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel9 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                    Main.projectile[proj].GetGlobalProjectile<GlobalProjectiles>().DefensivePetal = true;
                }
                else
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel9 * speed, ModContent.ProjectileType<OrchidPetal>(), dmg, 5f, Main.myPlayer);
                }
            }
                
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

    }
}
