using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RealmOne.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
        public static bool downedPiggy;
        public static bool downedRat;


        public static bool downedSquirmo;
        public static bool downedOutcropOutcast;

        public override void OnWorldLoad()
        {
            downedPiggy = false;
            downedRat = false;

            downedSquirmo = false;
            downedOutcropOutcast = false;
        }

        public override void OnWorldUnload()
        {
            downedPiggy = false;
            downedRat = false;

            downedSquirmo = false;
            downedOutcropOutcast = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {

            if (downedPiggy)
            {
                tag.Set("downedPiggy", true);
            }
            if (downedRat)
            {
                tag.Set("downedRat", true);
            }

            if (downedSquirmo)
            {
                tag.Set("downedSquirmo", true);
            }

            if (downedOutcropOutcast)
            {
                tag.Set("downedOutcropOutcast", true);
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedPiggy = tag.ContainsKey("downedPiggy");
            downedRat= tag.ContainsKey("downedRat");

            downedSquirmo = tag.ContainsKey("downedSquirmo");
            downedOutcropOutcast = tag.ContainsKey("downedOutcropOutcast");

        }
    }
}
