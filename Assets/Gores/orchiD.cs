using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RealmOne.Assets.Gores
{
    public class orchiD : ModGore
    {
        public override void OnSpawn(Gore gore, IEntitySource source)
        {
            base.OnSpawn(gore, source);
        }

        public override bool Update(Gore gore)
        {
            return base.Update(gore);
        }

        public override Color? GetAlpha(Gore gore, Color lightColor)
        {
            return Color.White;
        }
    }
}