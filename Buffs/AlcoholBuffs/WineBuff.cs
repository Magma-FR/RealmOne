using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs.AlcoholBuffs
{
    public class WineBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Spicegrass Wine");
            Description.SetDefault("A spicy but almost sweet liquor");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
            player.runAcceleration += 0.08f;
            player.maxRunSpeed += 0.08f;
            player.statDefense -= 3;
        }
    }
}