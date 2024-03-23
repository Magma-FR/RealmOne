using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;

namespace RealmOne.Buffs.Debuffs
{
    public class Copperized : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Copperized");
            Description.SetDefault("You're made out of copper?");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CopperizedGlobal>().Copperized = true;
            if (npc.buffTime[buffIndex] < 1)
            {
                npc.GetGlobalNPC<CopperizedGlobal>().dir = 0;
            }
        }
    }

    public class CopperizedGlobal : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Copperized;
        public int dir = 0;
        public int cd;
        

        public override void ResetEffects(NPC npc)
        {
            Copperized = false;
        }

        public override bool PreAI(NPC npc)
        {
            if (Copperized)
                if (dir == 0)
                    dir = npc.direction;

            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {
            

            if (Copperized)
            {
                if (cd > 0)
                {
                    cd--;
                }

                if (cd == 0)
                {
                    cd = 30;
                    SparkleParticle sparkle = new(Color.White, 1, new Vector2(npc.Center.X + Main.rand.Next(-50, 50), npc.Center.Y + Main.rand.Next(-50, 50)), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);
                    ParticleSystem.GenerateParticle(sparkle);
                }

                int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Copper, Scale: 1.5f);
                Main.dust[d].noGravity = true;

                if (npc.boss == false)
                {
                    npc.direction = dir;

                    npc.velocity /= 10f;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Copperized)
                drawColor = new Color(1f, 0.5f, 0f);

        }
    }
}