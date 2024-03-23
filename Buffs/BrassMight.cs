using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class BrassMight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Brass Encasement");
            Description.SetDefault("'The power of rust!!!'\n+4 life regen\n+20 max health\n+8 defense\n-50% movement speed");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 8;
            player.lifeRegen += 4;
            player.moveSpeed -= 0.50f;


            if (player.buffTime[buffIndex] < 1)
            {
                player.AddBuff(ModContent.BuffType<BrassMightCD>(), 1200);
            }
        }
    }
}