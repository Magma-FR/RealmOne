using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class SaltySwig : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Salty Swig");
            Description.SetDefault("Strong flavour of absolute rum, pineapple, vanilla and spices, washed up!");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
            player.moveSpeed += 0.08f;
            player.statDefense -= 5;
        }
    }
}