using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class CigaretteBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Cigarette Buff");
            Description.SetDefault("'You're really feelin it right now'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.20f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.20f;
            player.lifeRegen = 0;
            player.lifeMagnet = false;
            player.lifeSteal = 0f;
        }
    }
}