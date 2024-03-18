using Terraria;
using Terraria.ModLoader;

namespace RealmOne.Common.Core.ParticleContent
{
    internal class ParticleCaller : ModSystem
    {
        public override void PostDrawTiles()
        {
            if (!Main.dedServ)
            {
                ParticleSystem.Draw();
                ParticleSystem.UpdateParticles();
            }
        }
    }
}