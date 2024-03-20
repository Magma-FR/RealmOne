using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;
using Terraria.Audio;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;

namespace RealmOne.Buffs.Debuffs.ShockStacks
{
    public class Shocked6 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Electrified");
            Description.SetDefault("Shocked!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            SparkleParticle sparkle = new(Color.LightSkyBlue, 1, new Vector2(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y + Main.rand.Next(-20, 20)), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

            ParticleSystem.GenerateParticle(sparkle);
            if (npc.boss == false)
            {
                npc.velocity /= 1.2f;
            }
            npc.color = Color.Turquoise;


            if (npc.buffTime[buffIndex] < 1)
            {
                npc.color = Color.White;
                if (!npc.HasBuff<Shocked7>())
                    npc.AddBuff(ModContent.BuffType<Shocked7>(), 10);
            }

        }


        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }
    }



}