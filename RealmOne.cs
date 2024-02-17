using RealmOne.Common;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Systems;
using RealmOne.Items.BossSummons;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.NPCs.Enemies.MiniBoss;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace RealmOne
{
    public class RealmOne : Mod
    {
        public static RealmOne Instance { get; set; }

        public RealmOne()
        {
            Instance = this;
        }
        public const string AssetPath = $"{nameof(RealmOne)}/Assets/";
        public static float ModTime { get; internal set; }
        public static object MessageType { get; internal set; }


        public override void Unload()
        {
            ParticleSystem.Unload();

        }

        public override void Load()
        {

            Instance = this;

            if (!Main.dedServ)
            {
                Filters.Scene["RealmOne:BloodSky"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryHigh);
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
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MORTICIDE");
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;

        public override bool IsSceneEffectActive(Player player)
        {
            return Main.bloodMoon;
        }
    }


}

  
