using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class BreatheBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Breathe Buff!");
            Description.SetDefault("'Ability to breathe in space and gain various buffs that increase if your in space'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.08f;
            player.accRunSpeed += 0.08f;
            player.jumpSpeedBoost += 0.5f;
            player.buffImmune[BuffID.Suffocation] = true;

            //If the player is now in space
            if (player.ZoneSkyHeight)
            {
                player.moveSpeed += 0.16f;
                player.accRunSpeed += 0.16f;
                player.jumpSpeedBoost += 0.1f;
                player.buffImmune[BuffID.Suffocation] = true;
            }
        }
    }
}