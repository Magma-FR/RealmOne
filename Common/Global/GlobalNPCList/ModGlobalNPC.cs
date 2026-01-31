using RealmOne.Items.Misc.EnemyDrops;
using RealmOne.Items.Opens;
using RealmOne.Items.Placeables;
using RealmOne.Items.Placeables.Furniture.Paintings;
using RealmOne.Items.Tools.Pick;
using RealmOne.Items.Weapons.Magic;
using RealmOne.Items.Weapons.PreHM.Classless;
using RealmOne.Items.Weapons.PreHM.Throwing;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Common.Global.GlobalNPCList
{
    public class ModGlobalNPCList : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<IllicitStash>();
            }
            if (shop.NpcType == NPCID.Demolitionist)
            {
                shop.Add<SpecialDynamite>();
            }
            if (shop.NpcType == NPCID.Dryad)
            {
                shop.Add<DepthsPNT>();
            }
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<NothingPNT>();
            }
            if (shop.NpcType == NPCID.GoblinTinkerer)
            {
                shop.Add<NewtonCradleItem>();
            }
        }

        // ModifyNPCLoot uses a unique system called the ItemDropDatabase, which has many different rules for many different drop use cases.
        // Here we go through all of them, and how they can be used.
        // There are tons of other examples in vanilla! In a decompiled vanilla build, GameContent/ItemDropRules/ItemDropDatabase adds item drops to every single vanilla NPC, which can be a good resource.
        public override void OnKill(NPC npc)
        {
            /*if (npc.type == ModContent.NPCType<SquirmoHead>())
			{
				if (Main.netMode != NetmodeID.Server)
				{
					Main.NewText(Language.GetTextValue("The soil has been adhered, the ground has been enchanted!"), 71, 229, 231);
				}
			}*/

            if (npc.type == NPCID.KingSlime)
                if (Main.netMode != NetmodeID.Server)
                    Main.NewText(Language.GetTextValue("The oversized glob of bacteria has been vanquished, but for what reason?"), 71, 229, 231);

            if (npc.type == NPCID.EyeofCthulhu)
                if (Main.netMode != NetmodeID.Server)
                    Main.NewText(Language.GetTextValue("The seer of the land has been slayed, but you're still being watched."), 178, 30, 250);

            //      if (npc.type == NPCID.EaterofWorldsHead)x
            //          if (Main.netMode != NetmodeID.Server)
            //              Main.NewText(Language.GetTextValue("The vile, slithering worm of infection has been slaughtered, decreasing the spread of power of the corruption"), 200, 50, 230);

            if (npc.type == NPCID.BrainofCthulhu)
                if (Main.netMode != NetmodeID.Server)
                    Main.NewText(Language.GetTextValue("The hypnotic brain of amalgamated knowledge has been slain, you feel your mind calming down."), 250, 20, 80);

            if (npc.type == NPCID.QueenBee)
                if (Main.netMode != NetmodeID.Server)
                    Main.NewText(Language.GetTextValue("The grand protector of the hives has been killed, making insects favour you in positive or negatives ways"), 235, 221, 54);
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // 4  is the chance, so 4 chance of dropping it. And 1-2 is the amount you will probably get
            //Blood Moon Drops

            //Evil Biome drops

            //Hell Drops
            /*     if (npc.type == NPCID.Demon)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 2));
                 }
                 if (npc.type == NPCID.Hellbat)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 2));
                 }
                 if (npc.type == NPCID.LavaSlime)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 2));
                 }
                 if (npc.type == NPCID.FireImp)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 2));
                 }
                 if (npc.type == NPCID.BoneSerpentHead)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 3));
                 }
                 if (npc.type == NPCID.VoodooDemon)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 3));
                 }
                 if (npc.type == NPCID.Demon)
                 {
                     npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HellishMembrane>(), 4, 1, 3));
                 }
            */
            //Goblin Army
            if (npc.type == NPCID.GoblinArcher)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 3));
            }
            if (npc.type == NPCID.GoblinPeon)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 3));
            }
            if (npc.type == NPCID.GoblinScout)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 3));
            }

            if (npc.type == NPCID.GoblinThief)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 3));
            }
            if (npc.type == NPCID.GoblinWarrior)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 3));
            }
            if (npc.type == NPCID.GoblinSorcerer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 3));
            }

            if (npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GizmoScrap>(), 4, 1, 5));
            }

            if (npc.type == NPCID.GoblinSorcerer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagicGobBag>(), 50, 1, 1));
            }
            if (npc.type == NPCID.ServantofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FleshyCornea>(), 3, 1, 2));
            }

            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EyePick>(), 5, 1, 1));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FleshyCornea>(), 1, 1, 10));
            }

            if (npc.type == NPCID.Vampire)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<vampdag>(), 4, 1, 30)); //4 out of 1
            }

            if (npc.type == NPCID.VampireBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<vampdag>(), 4, 1, 30)); //4 out of 1
            }

            /*	public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
				{
					// This example does not use the AppliesToEntity hook, as such, we can handle multiple npcs here by using if statements.
					if (type == NPCID.Merchant)
					{
						// Adding an item to a vanilla NPC is easy:
						// This item sells for the normal price.
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<TundraThrowingKnife>());
						nextSlot++; // Don't forget this line, it is essential.

						// We can use shopCustomPrice and shopSpecialCurrency to support custom prices and currency. Usually a shop sells an item for item.value.
						// Editing item.value in SetupShop is an incorrect approach.

						// This shop entry sells for 2 Defenders Medals.
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<sparky>());
						shop.item[nextSlot].shopCustomPrice = 2;

						nextSlot++;
					}

					if (type == NPCID.Clothier)
					{
						// Adding an item to a vanilla NPC is easy:
						// This item sells for the normal price.
						shop.item[nextSlot].SetDefaults(ModContent.ItemType<SkullMask>());
						nextSlot++; // Don't forget this line, it is essential.

						// We can use shopCustomPrice and shopSpecialCurrency to support custom prices and currency. Usually a shop sells an item for item.value.
						// Editing item.value in SetupShop is an incorrect approach.

						// This shop entry sells for 2 Defenders Medals
					}
					else if (type == NPCID.Merchant)
					{
						// You can use conditions to dynamically change items offered for sale in a shop
						if (Main.dayTime == false && Main.hardMode == false)
						{
							shop.item[nextSlot].SetDefaults(ModContent.ItemType<IllicitStash>());
							nextSlot++;
						}
					}
				}*/
        }
    }
}