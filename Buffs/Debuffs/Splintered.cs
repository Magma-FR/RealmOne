using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs.Debuffs
{
    public class Splintered : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Splinted");
            Description.SetDefault("'Ouch!'");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;

            Main.pvpBuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.life -= 2;
            if (npc.life <= 0)
            {
                npc.life = 1; // Make sure the NPC's life is never less than 1
                npc.checkDead(); // Kill the NPC if its life is less than or equal to 0
            }
        }
    }
}