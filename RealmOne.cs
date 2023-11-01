using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using tModPorter;
using RealmOne.Common.Core;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using Terraria.DataStructures;
using System.Collections.Generic;
using RealmOne.Common;

namespace RealmOne
{
    public class RealmOne : Mod
    {
        public static RealmOne Instance = new();
        public const string AssetPath = $"{nameof(RealmOne)}/Assets/";
        public static float ModTime { get; internal set; }
        public static object MessageType { get; internal set; }

        public override void Unload()
        {
            
        }
        public override void Load()
        {
            // Your other initialization code

            // Add a new instance of your sky system
       

               if (!Main.dedServ)
                {
                Terraria.Graphics.Effects.Filters.Scene["RealmOne:BloodSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryHigh);
                SkyManager.Instance["RealmOne:BloodSky"] = new BloodSky();
             }
              
            
        }
        internal static object GetLegacySoundSlot(object custom, string v)
        {
            throw new NotImplementedException();
        }

        internal static object GetLegacySoundSlot(SoundType soundType)
        {
            throw new NotImplementedException();
        }

        

    }
    internal class BloodMoonOSTSwitch : ModSceneEffect
    {



        public override int Music => MusicLoader.GetMusicSlot(Mod,"Assets/Music/MORTICIDE");
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public override bool IsSceneEffectActive(Player player)
        {
            return Main.bloodMoon;
        }
    }
}

    
     

           /*   public override void PostSetupContent()
               {
                   ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist);
                   if (bossChecklist != null)
                   {
                       bossChecklist.Call(new object[11]
                       {s

                                 "AddBoss",
                                 this,
                                 "Squirmo",
                                 ModContent.NPCType<SquirmoHead>(),
                                 0.7f,
                                 () => DownedBossSystem.downedSquirmo,
                                 () => true,
                                 new List<int>
                                 {
                                     ModContent.ItemType<GlobGun>(),
                                     ModContent.ItemType<SquirmStaff>(),
                                     ModContent.ItemType<SquirYo>(),
                                     ModContent.ItemType<SquirmoLorePageOne>(),
                                     ModContent.ItemType<TwinklingTwig>(),
                                 },
                                 ModContent.ItemType<SquirmoSummon>(),
                                 "Even from the past dread of worm adaptation, they havent really caused global damage. But for Squirmo, ever seeking revenge on human inhabitants is still a current warning for people.  Adhere the relief of the soil by defeating Squirmo!!",
                                 ""

                       });

                       bossChecklist.Call(new object[11]
                       {
                                 "AddBoss",
                                 this,
                                 "The Outcrop Outcast",
                                 ModContent.NPCType<MossyMarauder>(),
                                 3f,
                                 () => DownedBossSystem.downedOutcropOutcast,
                                 () => true,
                                 new List<int>
                                 {

                                     ModContent.ItemType<EarthEmerald>(),
                                     ModContent.ItemType<TheOutcastsOverseer>(),
                                     ModContent.ItemType<BarrenBrew>(),
                           //          ModContent.ItemType<BotanicLogLauncher>(),
                                     ModContent.ItemType<Overgrowth>(),
                                     ModContent.ItemType<FoliageFury>(),

                                 },
                                 ModContent.ItemType<PhotosynthesisItem>(),
                                 "To this day, the dirt titan is a remarkable standpoint of the growth of Terraria, from its hard life and from its happiest life, it will always be a symbol of the land. The name that is given to it now is called The Outcrop Outcast, for many reasons. The dirt effigy still remains on this day and is untouched even for the irresponsible. Would you rather defeat the past or lose the future?\r\n",
                                 ""
                       });
                       bossChecklist.Call(new object[11]
                    {
                             "AddBoss",
                             this,
                             "Possessed Piggy Bank",
                             ModContent.NPCType<PossessedPiggy>(),
                             0.3f,
                             () => DownedBossSystem.downedPiggy,
                             () => true,
                             new List<int> { ModContent.ItemType<PiggyPorcelain>() },
                             ModContent.ItemType<MoneyVase>(),
                             "A rare scavenger of the land, looking for any Terrarian to stumble across it, stealing all its loot!!",
                             ""
                      });
                   }
               }*/
        

