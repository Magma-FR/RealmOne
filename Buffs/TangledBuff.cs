using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class TangledBuff : ModBuff
    {
        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Tangled");
            Description.SetDefault("'Wrath of the vines grows in you, Greatly slows enemies and inflict poison'");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed -= 0.3f;
            player.buffImmune[BuffID.Poisoned] = true;
            player.AddBuff(BuffID.Poisoned, 300);

            if (Main.rand.NextBool(3))
            {
                int d = Dust.NewDust(player.position, player.width, player.height, DustID.Plantera_Green);
                Main.dust[d].velocity.X *= 0f;
                Main.dust[d].velocity.Y *= 0.5f;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (Main.rand.NextBool(3))
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.JungleGrass);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 3f;

                int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.JungleSpore);
                Main.dust[dust2].scale = 3f;
            }
            npc.defense -= 5;
            npc.velocity.X *= 0.90f;
            npc.velocity.Y *= 0.90f;
        }
    }
}