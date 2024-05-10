using Microsoft.Xna.Framework;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.NPCs.Enemies.ForestRevenge;
using RealmOne.NPCs.Enemies.Lightbulb;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RealmOne.Common.Events.CursedForest
{
    public class CursedForestEvent : ModSystem
    {
        public static bool testedEvents;

        public static bool CursedForest = false;

        public static bool downedCursedForest = false;

        public static CursedForestEvent Instance { get; set; }

        public CursedForestEvent()
        {
            Instance = this;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downed = new List<string>();

            if (downedCursedForest)
                downed.Add("downedCursedForest");
            if (CursedForest)
                downed.Add("CursedForest");

            tag["downed"] = downed;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedCursedForest = downed.Contains("downedCursedForest");
            CursedForest = downed.Contains("CursedForest");
            testedEvents = true;
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = downedCursedForest;
            flags[1] = CursedForest;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedCursedForest = flags[0];
            CursedForest = flags[1];
        }

        public override void OnWorldLoad()
        {
            CursedForest = false;
            downedCursedForest = false;
        }

        public override void OnWorldUnload()
        {
            CursedForest = false;
            downedCursedForest = false;
        }

        public static int[] CursedForestEnemies => new[]
        {
            ModContent.NPCType<CursedBird>(),
            ModContent.NPCType<CursedOwl>(),
            ModContent.NPCType<CursedFirefly>(),
            ModContent.NPCType<CursedBunny>(),
            ModContent.NPCType<CursedSquirrel>(),
       };

        public override void PreUpdateWorld()
        {
            Player player = Main.LocalPlayer;

            if (!CursedForest && !testedEvents && !Main.bloodMoon && !Main.dayTime && WorldGen.spawnHardBoss == 0)
            {
                if ((Main.rand.NextBool(35) && !downedCursedForest) || (Main.rand.NextBool(40) && downedCursedForest))
                {
                    player.aggro += 200;
                    string status = "The forest has been cursed into a detrimental state";
                    if (Main.netMode == NetmodeID.Server)
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(status), new Color(200, 0, 50));
                    else if (Main.netMode == NetmodeID.SinglePlayer)
                        Main.NewText(Language.GetTextValue(status), new Color(200, 0, 50));

                    CursedForest = true;
                    downedCursedForest = true;

                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData);
                }
                testedEvents = true;
            }
            else if (CursedForest && Main.dayTime)
            {
                string status = "The deadly curse has been vanquished";
                if (Main.netMode == NetmodeID.Server)
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(status), new Color(200, 0, 255));
                else if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(Language.GetTextValue(status), new Color(200, 0, 255));

                CursedForest = false;
                testedEvents = false;
                int zoologistNpc = NPC.FindFirstNPC(NPCID.BestiaryGirl);

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
            }
            if (Main.dayTime)
                testedEvents = false;
        }

        public override void PostUpdateWorld()
        {
            if (CursedForest && !downedCursedForest)
                downedCursedForest = true;
        }
    }
}

/*   public class CursedForestballs : GlobalNPC
   {
       public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
       {
           int activePlayers = 0;
           for (int i = 0; i < Main.maxPlayers; i++)
               if (Main.player[i].active)
                   activePlayers++;

           if (CursedForestEvent.CursedForest && player.ZoneForest)
           {
               maxSpawns = (int)(5 + 1.5f * activePlayers);
               spawnRate = 10;
           }
       }

       public static List<IDictionary<int, float>> Spawnpool
       {
           get => new List<IDictionary<int, float>>
           { //list containing a dictionary of spawn pool information for each wave of the tide, key int is enemy type and value float is spawn rate
               new Dictionary<int, float>
               { //wave 1
                   { NPCID.Bird, 7.35f },
                   { NPCID.BirdBlue, 5.35f },
                   { NPCID.Bunny, 7.35f },
                   { NPCID.Squirrel, 1.73f },
                   { NPCID.Owl, .135f },
                   { NPCID.Firefly, 2f },
               }
           };
       }

       //Continue with EditSpawnPool code
       //And OnKill, like event points ig
   }
}
*/