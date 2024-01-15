using RealmOne.NPCs.Enemies.MiniBoss;
using RealmOne.Projectiles.Throwing;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.BossSummons
{
    public class MoneyVase : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Money Vase");
            Tooltip.SetDefault("'Why you takin allat money for kid?'"
                + "\n'Your bank account finna be empty after using this dawg'"
                + "\nSummons The Possessed Piggy Bank");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 20000;
            Item.rare = ItemRarityID.Pink;
            Item.consumable = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 20;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<MoneyVaseProj>();
            Item.shootSpeed = 13f;
            Item.noUseGraphic = true;
        }

        public override bool CanUseItem(Player player)
        {
            // If you decide to use the below UseItem code, you have to include !NPC.AnyNPCs(id), as this is also the check the server does when receiving MessageID.SpawnBoss.
            // If you want more constraints for the summon item, combine them as boolean expressions:
            //    return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<MinionBossBody>()); would mean "not daytime and no MinionBossBody currently alive"
            return !NPC.AnyNPCs(ModContent.NPCType<PossessedPiggy>());
        }

        /* public override bool? UseItem(Player player)
         {
             if (player.whoAmI == Main.myPlayer)
             {
                 NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y - 180, ModContent.NPCType<PossessedPiggy>());

                 if (Main.netMode != NetmodeID.Server)
                 {
                     CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color(255, 198, 125, 255), "'Goofy ahh pig fr'", false, false);
                 }
                 SoundEngine.PlaySound(SoundID.Item59, player.position);

                 player.GetModPlayer<Screenshake>().SmallScreenshake = true;
             }

             return true;
         }*/

        public override void AddRecipes()
        {
            CreateRecipe(1)
           .AddIngredient(Mod, "PiggyPorcelain", 10)

            .AddTile(TileID.PiggyBank)
            .Register();
        }
    }
}