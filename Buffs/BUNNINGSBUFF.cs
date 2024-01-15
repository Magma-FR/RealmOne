using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class BUNNINGSBUFF : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Bunnings Buff");
            Description.SetDefault("GET THAT SHI DOWN IN YOUR MOUTH! NOTHIN BETTER THAN GOOD OLD BUNNINGS SNAG");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.10f;
            player.maxRunSpeed += 0.10f;
            player.AddBuff(BuffID.WellFed, 150000);
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Confused] = true;
        }
    }
}