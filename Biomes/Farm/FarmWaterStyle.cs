using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Biomes.Farm
{
    public class FarmWaterStyle : ModWaterStyle
    {
        public override int ChooseWaterfallStyle()
        {
            return ModContent.GetInstance<FarmWaterfallStyle>().Slot;
        }

        public override int GetSplashDust() => DustID.Water_Desert;

        public override int GetDropletGore() => GoreID.WaterDrip;

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }

        public override Color BiomeHairColor() => Color.LightYellow;
    }
}