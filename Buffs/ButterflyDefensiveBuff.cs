using Microsoft.Xna.Framework;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Buffs
{
    public class PetalDefensiveBuff : ModBuff
    {
        int cd = 0;

        public override void SetStaticDefaults()

        {
            DisplayName.SetDefault("Petal's Blessing");
            Description.SetDefault("Increased defense");
        }

        public override void Update(Player p, ref int buffIndex)
        {
            if (cd > 0)
            {
                cd--;
            }
            if (cd == 0)
            {
                cd = 45;
                SparkleParticle sparkle = new(Color.White, 1, new Vector2(p.Center.X + Main.rand.Next(-50, 50), p.Center.Y + Main.rand.Next(-50, 50)), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);
                ParticleSystem.GenerateParticle(sparkle);
            }

            p.statDefense += 8;
        }
    }
}