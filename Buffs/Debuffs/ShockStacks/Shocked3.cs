using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;
using Terraria.Audio;

namespace RealmOne.Buffs.Debuffs.ShockStacks
{
    public class Shocked3 : ModBuff
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
            int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric, Scale: 1.1f);
            Main.dust[d].noGravity = true;
            npc.lifeRegen = -4;
            if (npc.boss == false)
            {
                npc.velocity /= 1.1f;
            }

            npc.color = Color.Turquoise;
            if (npc.buffTime[buffIndex] < 1)
            {
                npc.color = Color.White;
            }
        }


        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.DelBuff(buffIndex);
            npc.AddBuff(ModContent.BuffType<Shocked4>(), time);

            return base.ReApply(npc, time, buffIndex);
        }
    }



}