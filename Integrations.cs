/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Bosses;
using RealmOne.Common.Systems;
using RealmOne.Items.BossSummons;
using RealmOne.Items.PaperUI;
using RealmOne.Items.Placeables.Furniture.BossThing;
using RealmOne.Items.Vanities;
using RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RealmOne
{

    public class Integrations : ModSystem
    {
        public override void PostSetupContent()
        {
            // Most often, mods require you to use the PostSetupContent hook to call their methods. This guarantees various data is initialized and set up properly

            // Boss Checklist shows comprehensive information about bosses in its own UI. We can customize it:
            
            DoBossChecklistIntegration();

            // We can integrate with other mods here by following the same pattern. Some modders may prefer a ModSystem for each mod they integrate with, or some other design.
        }

        private void DoBossChecklistIntegration()
        {
            // The mods homepage links to its own wiki where the calls are explained: https://github.com/JavidPack/BossChecklist/wiki/%5B1.4.4%5D-Boss-Log-Entry-Mod-Call
            // If we navigate the wiki, we can find the "LogBoss" method, which we want in this case
            // A feature of the call is that it will create an entry in the localization file of the specified NPC type for its spawn info, so make sure to visit the localization file after your mod runs once to edit it

            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            // For some messages, mods might not have them at release, so we need to verify when the last iteration of the method variation was first added to the mod, in this case 1.6
            // Usually mods either provide that information themselves in some way, or it's found on the GitHub through commit history/blame
            if (bossChecklistMod.Version < new Version(1, 6))
            {
                return;
            }

            // The "LogBoss" method requires many parameters, defined separately below:

            // Your entry key can be used by other developers to submit mod-collaborative data to your entry. It should not be changed once defined
            string internalName = "Squirmo";

            // Value inferred from boss progression, see the wiki for details
            float weight = 0.2f;

            // Used for tracking checklist progress
            Func<bool> downed = () => DownedBossSystem.downedSquirmo;

            // The NPC type of the boss
            int bossType = ModContent.NPCType<SquirmoHead>();

            // The item used to summon the boss with (if available)
            int spawnItem = ModContent.ItemType<SquirmoSummon>();

            // "collectibles" like relic, trophy, mask, pet
            List<int> collectibles = new List<int>()
            {
                 ModContent.ItemType<GlobGun>(),
                              ModContent.ItemType<SquirmStaff>(),
                              ModContent.ItemType<SquirYo>(),
                              ModContent.ItemType<SquirmoLorePageOne>(),
                              ModContent.ItemType<SquirmoRelicItem>(),
                              ModContent.ItemType<SquirmoMask>(),
                              ModContent.ItemType<SquirmoTrophyItem>(),




            };

            // By default, it draws the first frame of the boss, omit if you don't need custom drawing
            // But we want to draw the bestiary texture instead, so we create the code for that to draw centered on the intended location
            var customPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Textures/SquirmoTexture").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
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
                    ["customPortrait"] = customPortrait
                    // Other optional arguments as needed are inferred from the wiki
                }
            );

            // Other bosses or additional Mod.Call can be made here.
        }
    }
}*/