/*using Microsoft.Xna.Framework;
using RealmOne.Bosses;
using RealmOne.Common.Systems;
using RealmOne.Items.ItemCritter;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Items.BossSummons
{
    public class SquirmoSummon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Infested Carrot");
            Tooltip.SetDefault("'Awaken the sludgy scavenger of the soil'"
                + "\n'The soil will adhere relief!!'"
                + "\nSummons Squirmo");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(246, 136, 26),
                new Color(255, 187, 119),
                new Color(243, 202, 84)
            };
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item2;
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 0;
        }

        public override bool CanUseItem(Player player)
        {
            // If you decide to use the below UseItem code, you have to include !NPC.AnyNPCs(id), as this is also the check the server does when receiving MessageID.SpawnBoss.
            // If you want more constraints for the summon item, combine them as boolean expressions:
            //    return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<MinionBossBody>()); would mean "not daytime and no MinionBossBody currently alive"
            return !NPC.AnyNPCs(ModContent.NPCType<SquirmoHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>()) && !NPC.AnyNPCs(ModContent.NPCType<MegaSquirmHead>());
        }
         

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int CarrotGore1 = Mod.Find<ModGore>("CarrotGore1").Type;
                int CarrotGore2 = Mod.Find<ModGore>("CarrotGore2").Type;
                int CarrotGore3 = Mod.Find<ModGore>("CarrotGore3").Type;
                int CarrotGore4 = Mod.Find<ModGore>("CarrotGore4").Type;



                IEntitySource entitySource = Item.GetSource_Death();

                for (int i = 0; i < 1; i++)
                {
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore1);
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore2);
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore3);
                    Gore.NewGore(entitySource, player.position, new Vector2(Main.rand.Next(0, 0), Main.rand.Next(0, 0)), CarrotGore4);





                }

                if (Main.netMode != NetmodeID.Server)
                    Main.NewText(Language.GetTextValue("'The soil feels moist...'"), 210, 100, 175);
                if (Main.netMode != NetmodeID.Server)
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(255, 198, 125, 255), "'Squirmo is tunneling towards you!!", false, false);
                SoundEngine.PlaySound(rorAudio.SquirmoSummonSound, player.position);

                player.GetModPlayer<Screenshake>().WormScreenshake = true;




            }

            if (player.whoAmI == Main.myPlayer)
            {
                // If the player using the item is the client
                // (explicitely excluded serverside here)

                int type = ModContent.NPCType<SquirmoHead>();

                int type1 = ModContent.NPCType<MegaSquirmHead>();

                int type2 = ModContent.NPCType<MegaSquirmHead>();

                int type3 = ModContent.NPCType<MegaSquirmHead>();
                int type4 = ModContent.NPCType<MegaSquirmHead>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the player is not in multiplayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                    NPC.SpawnOnPlayer(player.whoAmI, type1);
                    NPC.SpawnOnPlayer(player.whoAmI, type2);
                    NPC.SpawnOnPlayer(player.whoAmI, type3);
                    NPC.SpawnOnPlayer(player.whoAmI, type4);

                }
                else
                {
                    // If the player is in multiplayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type1);
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type2);
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type3);
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type4);

                }

                player.GetModPlayer<Screenshake>().WormScreenshake = true;

            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
           .AddIngredient(Mod, "Carrot", 1)
           .AddIngredient(ModContent.ItemType<SquirmItem>(), 4)
                      .AddIngredient(ItemID.Worm, 4)

            .AddTile(TileID.WorkBenches)
            .Register();

        }
    }
}
*/