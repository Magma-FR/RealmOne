
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Bosses;
using RealmOne.Common.Systems;
using RealmOne.Items.BossSummons;
using RealmOne.Items.Food;
using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.PaperUI;
using RealmOne.Items.Placeables.Furniture.BossThing;
using RealmOne.Items.Vanities;
using RealmOne.Items.Weapons.PreHM.BossDrops.RatDrops;

//using RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops;
using RealmOne.NPCs.Critters;
using RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;
using RealmOne.NPCs.Enemies.MiniBoss;
using System;
using System.Collections.Generic;
using Terraria.ID;
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

            //Possessed Piggybank
            string internalName = "The Possessed Piggybank";
            float weight = 0.3f;
            Func<bool> downed = () => DownedBossSystem.downedPiggy;
            int bossType = ModContent.NPCType<PossessedPiggy>();
            int spawnItem = ModContent.ItemType<MoneyVase>();

            List<int> collectibles = new()
            {

                ModContent.ItemType<PiggyPorcelain>(),
                ItemID.GoldCoin,
                ItemID.PiggyBank,
                ItemID.Bacon,
                ItemID.MoneyTrough,

            };
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName,
                weight,
                downed,
                bossType,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItem,
                    ["collectibles"] = collectibles,
                }
            );

            /*
            string internalName1 = "Squirmo";
            float weight1 = 0.4f;
            Func<bool> downed1 = () => DownedBossSystem.downedSquirmo;
            int bossType1 = ModContent.NPCType<Squirmo>();
            int spawnItem1 = ModContent.ItemType<MoneyVase>();

            List<int> collectibles1 = new()
            {

                ModContent.ItemType<SquirmoMask>(),
                ItemID.GoldCoin,
                ItemID.PiggyBank,
                ItemID.Bacon,
                ItemID.MoneyTrough,

            };
            var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Textures/SquirmoTexture").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName1,
                weight1,
                downed1,
                bossType1,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItem,
                    ["collectibles"] = collectibles,
                    ["customPortrait"] = customPortrait
                }
            );
            */


            string internalName2 = "Butcher Rat";
            float weight2 = 0.57f;
            Func<bool> downed2 = () => DownedBossSystem.downedRat;
            int bossType2 = ModContent.NPCType<ButcherRat>();
            int spawnItem2 = ModContent.ItemType<MoneyVase>();

            List<int> collectibles2 = new()
            {

                ModContent.ItemType<GoreshankShotgun>(),
                ModContent.ItemType<Goreberry>(),
                ItemID.BloodyMachete,
               

            };
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                internalName2,
                weight2,
                downed2,
                bossType2,
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = spawnItem2,
                    ["collectibles"] = collectibles2,
                }
            );
        }
    }
}