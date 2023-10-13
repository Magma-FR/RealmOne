using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.BloodMoon
{
    public class ArteryCarrion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 8;

        }

		public override void SetDefaults()
		{
			NPC.width = 16;
			NPC.height = 21;
			NPC.damage = 14;
			NPC.defense = 3;
			NPC.lifeMax = 80;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f; 
			NPC.HitSound = SoundID.NPCHit19;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.noGravity = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer && Main.bloodMoon ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

		private double Timer;
		private double SinThing;
		private bool CanShoot;
        public override void OnSpawn(IEntitySource source)
        {
			Timer = 0;
        }
        public override void AI()
        {

			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Blood, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
			}

			Timer++;
			if (Timer % 15 == 0)
				SinThing++;
			Player player = Main.player[NPC.target];
			Vector2 TargetLocation = new Vector2(player.position.X + ((float)Math.Sin(SinThing)*100), player.position.Y - 150 - ((float)Math.Sin(SinThing) *10));

			float speed = 11f;
			float inertia = 30f;
			Vector2 direction = TargetLocation - NPC.Center;
			direction.Normalize();
			direction *= speed;
			NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;

			NPC.rotation = NPC.velocity.X * 0.3f;
			var entitySource = NPC.GetSource_FromThis();
			if (Vector2.Distance(TargetLocation, NPC.Center) <= 130)
			{
                if (Timer % 60 == 0)
                {
					var projectile = Projectile.NewProjectile(entitySource, NPC.Center, new Vector2(0f, 2f), ModContent.ProjectileType<ArteryCarrionProjectile>(), 10, 0f);
				}
					

			} 
				
			FindFrame(44);
		}
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ArteryGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ArteryGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ArteryGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ArteryGore4").Type, 1f);


            }
            for (int k = 0; k < 18; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }


        private int AnimFrameCount;
		private int AnimTimer;
		public override void FindFrame(int frameHeight)
		{
			AnimTimer++;
			
			if(AnimTimer%10==0)
				AnimFrameCount++;
			if (AnimFrameCount == 8)
			{
				AnimFrameCount = 1;
			}
			NPC.frame.Y = NPC.frame.Height * AnimFrameCount;
		}
	}

	public class ArteryCarrionProjectile :ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
		{
			Projectile.width = 12; // The width of projectile hitbox
			Projectile.height = 12; // The height of projectile hitbox
			
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = true; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 3; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 340; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.damage = 10;
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true; // Can the projectile collide with tiles?
			Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;

		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			}
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--; //Make sure it doesnt penetrate anymore
            if (Projectile.penetrate <= 0)
                Projectile.Kill();
            else
            {
                Projectile.velocity *= 0.6f;

                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }

            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("ArteGore1").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("ArteGore2").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("ArteGore3").Type, 1f);

        }
    }
}
