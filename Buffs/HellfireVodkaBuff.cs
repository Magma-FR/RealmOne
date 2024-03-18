using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class HellfireVodkaBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Hellfire Vodka");
            Description.SetDefault("Prefered choice of alcohol from the one and only Satan");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
            player.maxRunSpeed += 0.15f;
            player.runAcceleration += 0.15f;
            player.moveSpeed += 0.15f;
            player.onFire = true;
            player.statDefense -= 4;
            player.GetKnockback(DamageClass.Generic) += 0.10f;
        }
    }
}