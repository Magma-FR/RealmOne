using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RealmOne.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
        public static bool downedPiggy;
        public static bool downedRat;
        public static bool downedSquirmo;

        public override void SaveWorldData(TagCompound tag)
        {
            if (downedPiggy)
            {
                tag["downedPiggy"] = true;
            }
            if (downedRat)
            {
                tag["downedRat"] = true;
            }

            if (downedSquirmo)
            {
                tag["downedSquirmo"] = true;
            }
        }

        public override void ClearWorld()
        {
            downedPiggy = false;
            downedRat = false;
            downedSquirmo = false;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedPiggy = tag.ContainsKey("downedPiggy");

            downedRat = tag.ContainsKey("downedRat");

            downedSquirmo = tag.ContainsKey("downedSquirmo");
        }

        public override void NetSend(BinaryWriter writer)
        {
            // Order of operations is important and has to match that of NetReceive
            var flags = new BitsByte();
            flags[0] = downedPiggy;
            flags[1] = downedRat;
            flags[2] = downedSquirmo;

            writer.Write(flags);
        }

        public override void OnWorldLoad()
        {
            downedPiggy = false;

            downedRat = false;

            downedSquirmo = false;
        }

        public override void OnWorldUnload()
        {
            downedPiggy = false;
            downedRat = false;

            downedSquirmo = false;
        }

        public override void NetReceive(BinaryReader reader)
        {
            // Order of operations is important and has to match that of NetSend
            BitsByte flags = reader.ReadByte();
            downedPiggy = flags[0];
            downedRat = flags[1];
            downedSquirmo = flags[2];
        }
    }
}