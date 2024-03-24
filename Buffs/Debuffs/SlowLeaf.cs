using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;

namespace RealmOne.Buffs.Debuffs
{
    public class SlowLeaf : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Leafed");
            Description.SetDefault("You're being suffocated by leaves!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GreenTorch, Scale: 2.5f);
            Main.dust[d].noGravity = true;

            npc.velocity /= 1.1f;
            npc.color = Color.LightGreen;

            if (npc.buffTime[buffIndex] < 1)
            {
                npc.color = Color.White;
            }
        }
    }


}