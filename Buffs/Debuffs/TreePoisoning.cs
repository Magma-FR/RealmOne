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
            npc.velocity.X = 0;
            npc.velocity.Y = 0;

            if (cd2 > 0)
                cd2--;
            if (cd > 0)
                cd--;

            if (cd == 0)
            {
                cd = 10;
                npc.HitEffect(0, 1);
            }

            if (cd2 == 0)
            {
                cd2 = 60;
                if (npc.direction == -1)
                {
                    npc.direction = 1;
                }
                else
                {
                    npc.direction = -1;
                }
            }

        }
    }
}