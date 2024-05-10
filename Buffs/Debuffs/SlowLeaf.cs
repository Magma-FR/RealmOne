using RealmOne.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using RealmOne.RealmPlayer;
using Terraria.ID;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;

namespace RealmOne.Buffs.Debuffs
{
    public class SlowLeaf : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            DisplayName.SetDefault("Leafed");
            Description.SetDefault("You're being suffocated by leaves!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!npc.townNPC && !npc.friendly && !npc.CountsAsACritter)
            {
                //   int d = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GreenTorch, Scale: 0.5f);
                //  Main.dust[d].noGravity = true;
                GenericGlowParticle particle = new(new Vector2(npc.Center.X + Main.rand.Next(-2, 3), npc.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.2f, 3f)), Color.LightGreen, 0.15f, 50);
                ParticleSystem.GenerateParticle(particle);

                npc.velocity /= 1.2f;
                npc.color = Color.LightGreen;

                if (npc.buffTime[buffIndex] < 1)
                {
                    npc.color = Color.White;
                }
                { }
            }
        }
    }
}