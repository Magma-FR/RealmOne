using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace RealmOne.Biomes.Farm
{
    public class FarmSurface : ModBiome
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Abandoned Farm");

        public override ModWaterStyle WaterStyle => ModContent.GetInstance<FarmWaterStyle>(); // Sets a water style for when inside this biome
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<FarmBiomeBackgroundStyle>();
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/CottageOrchestra");

        public override string BestiaryIcon => "RealmOne/Biomes/Farm/FarmBiome_Icon";
        public override string BackgroundPath => "RealmOne/Biomes/Farm/FarmBestiaryBG";

        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => BackgroundPath; // Re-uses Bestiary Background for Map Background

        public override bool IsBiomeActive(Player player)
        {
            // First, we will use the exampleBlockCount from our added ModSystem for our first custom condition
            bool b1 = ModContent.GetInstance<BiomeTileCount>().FarmCount >= 40;

            // Second, we will limit this biome to the inner horizontal third of the map as our second custom condition
            //     bool b2 = Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;

            // Finally, we will limit the height at which this biome can be active to above ground (ie sky and surface). Most (if not all) surface biomes will use this condition.
            bool b3 = player.ZoneSkyHeight || player.ZoneOverworldHeight;
            return b1 && b3;
        }

        // Declare biome priority. The default is BiomeLow so this is only necessary if it needs a higher priority.
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;
    }
}