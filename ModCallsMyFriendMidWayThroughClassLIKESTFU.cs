using Terraria.ModLoader;

namespace RealmOne
{
    internal class ModCallsMyFriendMidWayThroughClassLIKESTFU : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModLoader.TryGetMod("MusicDisplay", out Mod display))
                return;

            void AddMusic(string path, string name, string author) => display.Call("AddMusic", (short)MusicLoader.GetMusicSlot(Mod, path), name, "by " + author, "Realm of R'lyeh");

            AddMusic("Assets/Music/CottageOrchestra", "Cottage Orchestra", "The Possum");
            AddMusic("Assets/Music/PiggyPatrol", "Piggy Patrol", "Mr Liquid");

            AddMusic("Assets/Music/squirmointro", "Digging Disasters", "Elosir");
            AddMusic("Assets/Music/InfestedSoil", "Infested Soil", "Elosir");
        }
    }
}