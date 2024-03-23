using RealmOne.Common.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs.Debuffs
{
    public class TreePoisoning : ModBuff
    {
        int cd;
        int cd2 = 60;
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Stuck");
            Description.SetDefault("Being prickled by trees!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BranchedGlobal>().Branched = true;
            if (npc.buffTime[buffIndex] < 1)
            {
                npc.GetGlobalNPC<BranchedGlobal>().dir = 0;
            }
        }
    }

    public class BranchedGlobal : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Branched;
        public int dir = 0;

        public override void ResetEffects(NPC npc)
        {
            Branched = false;
        }

        public override bool PreAI(NPC npc)
        {
            if (Branched)
                if (dir == 0)
                    dir = npc.direction;

            return base.PreAI(npc);
        }

        public override void PostAI(NPC npc)
        {

            if (Branched)
            {

                npc.direction = dir;
                npc.velocity /= 20f;
            }
        }
    }
}