using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs;
using RealmOne.Common.Global;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Other
{
    public class OrchidPetal : ModProjectile
    {
        int rand;
        int immunity = 0; //immunity frames to save our projectile from tile collision (explained at TileCollide)
        int cd = 20; //20 frame StopAI at the start to let the projectiles shoot out from the flower in a 6-sectioned circle, otherwise they would be coming from the flowers core.

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.friendly = false;
            Projectile.tileCollide = true;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 300;
        }

        
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            Main.EntitySpriteDraw(texture, Projectile.position, new Rectangle(0, 0, 14, 20), Color.White, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return true;
        }

        public override void AI()
        {

            if (Projectile.GetGlobalProjectile<GlobalProjectiles>().HealingPetal == true) //Depending on version, change dusts
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.RedStarfish, Scale: 0.45f, Alpha: 120);
                Lighting.AddLight(Projectile.Center, Color.Red.ToVector3() * 0.78f);
            }
            else if (Projectile.GetGlobalProjectile<GlobalProjectiles>().DefensivePetal == true)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust, Scale: 0.45f, Alpha: 120);
                Lighting.AddLight(Projectile.Center, Color.Yellow.ToVector3() * 0.78f);
            }
            else
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PinkFairy, Scale: 0.45f, Alpha: 120);
                Lighting.AddLight(Projectile.Center, Color.Pink.ToVector3() * 0.78f);
            }

            if (immunity > 0)
            {
                immunity--;
            }
            if (cd > 0)
            {
                cd--;
            }

            if (cd == 0) //Once StopAi start has finished, start the actual code
            {
                Projectile.friendly = true;

                NPC npc = FindClosestNPC(1200, Projectile);
                if (npc == null)
                {
                    return;
                }
                else
                {
                    if (npc.CanBeChasedBy())
                    {
                        if (Projectile.GetGlobalProjectile<GlobalProjectiles>().HealingPetal != true && Projectile.GetGlobalProjectile<GlobalProjectiles>().DefensivePetal != true)
                        {
                            Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(npc.Center) * 8f, 0.1f); //Home into the nearest npc if normal version. 10f is the speed of the projectile, while 0.1f is the turning rate. Changing these will affect the projectile!
                        }

                    }

                }

                if (Projectile.GetGlobalProjectile<GlobalProjectiles>().HealingPetal == true) //Healing version instead homes for the player
                {
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * 5f, 0.1f); //Slower than a normal petal

                    if (Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center) < 10) //If within 10 pixels of our projectiles owner, heal the player with some effects and destroy the projectile. Note that it is important to check current health when healing the player. I like to do custom healing rather than terraria healing because it is more immersive
                    {
                        Projectile.Kill();

                        int r = Main.rand.Next(17, 23);

                        if (Main.player[Projectile.owner].statLife + r < Main.player[Projectile.owner].statLifeMax2)
                        {
                            for (int i = 0; i < 40; i++)
                            {
                                Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.PinkFairy, speedd * 9, Scale: 1f);
                                dust1.noGravity = true;
                            }
                            Main.player[Projectile.owner].statLife += r;

                        }
                        else
                        {
                            Main.player[Projectile.owner].statLife = Main.player[Projectile.owner].statLifeMax2;
                        }
                        CombatText.NewText(Main.player[Projectile.owner].getRect(), new Color(58, 255, 0), $"+{r}", false, false);
                    }
                }
                if (Projectile.GetGlobalProjectile<GlobalProjectiles>().DefensivePetal == true) //Same as the above, but apply buff upon getting close
                {
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.DirectionTo(Main.player[Projectile.owner].Center) * 5f, 0.1f);

                    if (Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center) < 10)
                    {
                        Projectile.Kill();
                        for (int i = 0; i < 40; i++)
                        {
                            Vector2 speedd = Main.rand.NextVector2CircularEdge(2f, 2f);
                            Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust, speedd * 9, Scale: 1f);
                            dust1.noGravity = true;
                        }
                        Main.player[Projectile.owner].AddBuff(ModContent.BuffType<PetalDefensiveBuff>(), 180);
                    }
                }
            }
            else
            {
                Projectile.friendly = false;
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2; //Flip the projectile 90 degrees since our sprite faces upwards
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (Projectile.GetGlobalProjectile<GlobalProjectiles>().HealingPetal == true) //Vfx based on version
            {
                return new Color(255, 0, 255, 0);
            }
            else if (Projectile.GetGlobalProjectile<GlobalProjectiles>().DefensivePetal == true)
            {
                return new Color(255, 236, 0, 0);
            }

            return base.GetAlpha(lightColor);
        }

        public NPC FindClosestNPC(float maxDetectDistance, Projectile proj) //self explanatory
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];

                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, proj.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (cd > 0)
            {
                immunity = 12; //Cannot die from tile collision for 20 frames. Allows our projectile to necessarily save itself if shot towards a tile in the starting StopAI
                cd = 0;
                return false;
            }
            else if (immunity == 0)
            {
                return true;
            }

            return default;
        }


        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true; //Passthrough platforms
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 20; i++) //Death VFX
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust dust1 = Dust.NewDustPerfect(Projectile.Center, DustID.PinkCrystalShard, speed * 8, Scale: 1f);
                dust1.noGravity = true;
            }
        }
    }
}