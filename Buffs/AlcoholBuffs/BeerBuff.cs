using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs.AlcoholBuffs
{
    public class BeerBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Limetwist Rum Buff");
            Description.SetDefault("A slightly tangy but frothy rum");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) -= 0.15f;
            player.endurance += 0.10f;

            player.GetArmorPenetration(DamageClass.Generic) += 3f;
        }
    }
}