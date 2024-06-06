using RealmOne.Common.Systems;
using RealmOne.Items.Misc.EnemyDrops;

//using RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops;
using RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;
using RealmOne.NPCs.Enemies.MiniBoss;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RealmOne
{
    public class Integrations : ModSystem
    {
        public override void PostSetupContent()
        {
            DoBossChecklistIntegration();
        }

        private void DoBossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }
            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            string internalName = "PossessedPiggy";

            float weight = 0.2f;
            Func<bool> downed = () => DownedBossSystem.downedPiggy;
            int bossType = ModContent.NPCType<PossessedPiggy>();
            List<int> collectibles = new List<int>()
            {
                ModContent.ItemType<PiggyPorcelain>(),
            };
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName,
                weight,
                downed,
                //  spawnInfo,
                bossType,
                new Dictionary<string, object>()
                {
                    ["collectibles"] = collectibles,

                    //  ["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki`
                }
            );
            string internalName2 = "ButcherRat";
            float weight2 = 2.3f;
            Func<bool> downed2 = () => DownedBossSystem.downedRat;
            int bossType2 = ModContent.NPCType<ButcherRat>();
            List<int> collectibles2 = new List<int>()
            {
            };
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName2,
                weight2,
                downed2,
                //  spawnInfo,
                bossType2,
                new Dictionary<string, object>()
                {
                    ["collectibles"] = collectibles2,

                    //  ["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );
        }
    }
}