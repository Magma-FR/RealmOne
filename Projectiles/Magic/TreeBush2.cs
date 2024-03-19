using Microsoft.Xna.Framework;
using RealmOne.Buffs.Debuffs;
using RealmOne.RealmPlayer;
using System.Net;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Magic
{
    public class TreeBush2 : ModProjectile
    {
        bool fadeIn;
        int cd = 30;
        int cd2 = 10;
        int heightMax = 26;
        int height = 0;

        bool splosion = false;
        bool oneHit = false;
        bool sound = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bush Whack");
        }


        public override void SetDefaults()
        {
            Projectile.width = 68;
            Projectile.height = 128;


            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.knockBack = 0f;
            Projectile.timeLeft = 200;
            AIType = ProjectileID.Bullet;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            if (oneHit == true && fadeIn == true && Projectile.timeLeft < 30)
            {
                for (int i = 0; i < 10; i++)
                    Projectile.alpha++;
            }

            if (fadeIn == false)
            {
                if (Projectile.alpha <= 0)
                {
                    fadeIn = true;
                }
                for (int i = 0; i < 20; i++)
                    Projectile.alpha--;
            }
            else
            {
                if (cd > 0)
                {
                    cd--;
                }

                if (cd == 0)
                {
                    if (height < heightMax)
                    {
                        height++;
                        for (int i = 0; i < 5; i++)
                        {
                            Projectile.position.Y--;
                        }
                        Projectile.position.X--;

                    }
                    else
                    {
                        if (sound == false)
                        {
                            sound = true;
                            SoundEngine.PlaySound(SoundID.Grass, Projectile.Center);
                        }
                        if (cd2 > 0)
                        {
                            cd2--;
                        }

                        if (splosion == false && cd2 == 0)
                        {
                            splosion = true;
                            for (int i = 0; i < 21; i++)
                            {
                                Vector2 spawnBox = new Vector2(Projectile.Center.X + Main.rand.Next(-30, 30), Projectile.Center.Y - 40 + Main.rand.Next(-30, 30));
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Gore.NewGorePerfect(Projectile.GetSource_Death(), spawnBox, speed * 5f, GoreID.TreeLeaf_Normal, 1f);
                            }
                        }
                        
                    }
                }
            }

        }

        public override bool? CanDamage()
        {
            return true;
        }


        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.Center);
            for (int i = 0; i < 45; i++)
            {
                Vector2 spawnBox = new Vector2(Projectile.Center.X + Main.rand.Next(-30, 30), Projectile.Center.Y - 40 + Main.rand.Next(-30, 30));
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Gore.NewGorePerfect(Projectile.GetSource_Death(), spawnBox, speed * 5f, GoreID.TreeLeaf_Normal, 1f);
            }
        }
    }
}