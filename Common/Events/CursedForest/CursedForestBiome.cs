using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Common.Events.CursedForest
{
    public class CursedForestBiome : ModBiome
    {
        public static CursedForestBiome Instance { get; set; }
        public CursedForestBiome()
        {
            Instance = this;
        }
        public override void SetStaticDefaults()
        {
        }
        public override string BestiaryIcon
        {
            get
            {
                return base.BestiaryIcon;
            }
        }
        public override string BackgroundPath
        {
            get
            {
                return base.BackgroundPath;
            }
        }
        public override string MapBackground
        {
            get
            {
                return BackgroundPath;
            }
        }

        public override int Music
        {
            get
            {
                return (Main.LocalPlayer.ZoneOverworldHeight || Main.LocalPlayer.ZoneSkyHeight) ? MusicLoader.GetMusicSlot(Mod, "Assets/Music/squirmotheme") : base.Music;
            }
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;

        public override bool IsBiomeActive(Player player)
        {
            return CursedForestEvent.CursedForest;
        }

        public override void OnInBiome(Player player)
        {
            if (Main.netMode != NetmodeID.Server)
            {

            }
        }
    }

}
