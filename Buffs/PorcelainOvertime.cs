using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    internal class PorcelainOvertime : ModBuff
    {
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<PorcelainDMG>().PorceDeb = true;
        }
    }

    public class PorcelainDMG : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool PorceDeb;

        public override void ResetEffects(NPC npc)
        {
            PorceDeb = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (PorceDeb)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                if (npc.velocity.X != 0f || npc.velocity.Y != 0f)
                {
                    npc.lifeRegen -= 8;
                }
            }
        }
    }
}