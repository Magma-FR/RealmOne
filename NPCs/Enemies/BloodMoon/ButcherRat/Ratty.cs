using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;

public class Ratty : ModItem
{
    public override void SetStaticDefaults() {
        Item.ResearchUnlockCount = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort inventory know that this is a boss summoning Item.
    }

    public override void SetDefaults() {
        Item.width = 20;
        Item.height = 20;
        Item.maxStack = 20;
        Item.value = 100;
        Item.rare = ItemRarityID.Blue;
        Item.useAnimation = 30;
        Item.useTime = 30;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.consumable = true;
    }

    public override bool CanUseItem(Player player) {
        // If you decide to use the below UseItem code, you have to include !NPC.AnyNPCs(id), as this is also the check the server does when receiving MessageID.SpawnBoss.
        // If you want more constraints for the summon item, combine them as boolean expressions:
        //    return !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<MinionBossBody>()); would mean "not daytime and no MinionBossBody currently alive"
        return !NPC.AnyNPCs(ModContent.NPCType<ButcherRat>());
    }

    public override bool? UseItem(Player player) {
        if (player.whoAmI == Main.myPlayer) {
            SoundEngine.PlaySound(rorAudio.Rat, new Vector2(0, 15));

            var type = ModContent.NPCType<ButcherRat>();

            if (Main.netMode != NetmodeID.MultiplayerClient) {
                NPC.SpawnOnPlayer(player.whoAmI, type);
            }
            else {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
        }

        return true;
    }
}
