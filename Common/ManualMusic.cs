using Terraria.ModLoader;

namespace RealmOne.Common
{
    public sealed class ManualMusic : ILoadable
    {
        public void Load(Mod mod)
        {
            MusicLoader.AddMusic(mod, "Assets/Music/squirmointro");
            MusicLoader.AddMusic(mod, "Assets/Music/InfestedSoil");

            MusicLoader.AddMusic(mod, "Assets/Music/Rlyeh");
            MusicLoader.AddMusic(mod, "Assets/Music/PiggyPatrol");
            MusicLoader.AddMusic(mod, "Assets/Music/CottageOrchestra");
            MusicLoader.AddMusic(mod, "Assets/Music/MORTICIDE");
            MusicLoader.AddMusic(mod, "Assets/Music/Moonwalker");
        }

        public void Unload()
        {
        }
    }
}