using RealmOne.RealmPlayer;
using RealmOne.Tiles.Blocks;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RealmOne.NPCs.TownNPC
{
    // This is how to make a rescuable Town NPC.
    public class LostFarmerTied : ModNPC
    {
        public const string ShopName = "Shop";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;

            // Hide this NPC from the bestiary.
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Hide = true,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            // Notice NPC.townNPC is not set.
            NPC.friendly = true;
            NPC.width = 28;
            NPC.height = 32;
            NPC.aiStyle = 0; // aiStyle of 0 is used. The NPC will not move.
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.rarity = 1; // To make our NPC will show up on the Lifeform Analyzer.\
            NPC.dontTakeDamageFromHostiles = true;
        }

        public override bool CanChat()
        {
            // Make it so our NPC can be talked to.
            return true;
        }

        public override void AI()
        {
            // Using aiStyle 0 will make it so the NPC will always turn to face the player.
            // If you don't want that, you can set the spriteDirection to -1 (left) or 1 (right) so they always appear to face one way.
            // NPC.spriteDirection = 1;

            // This is where we check to see if a player has clicked on our NPC.
            // First, don't run this code if it is a multiplayer client.
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // Loop through every player on the server.
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    // If the player is active (on the server) and are talking to this NPC...
                    if (Main.player[i].active && Main.player[i].talkNPC == NPC.whoAmI)
                    {
                        NPC.Transform(ModContent.NPCType<LostFarmer>()); // Transform to our real Town NPC.
                        Main.BestiaryTracker.Chats.RegisterChatStartWith(NPC); // Unlock the Town NPC in the Bestiary.
                        Main.player[i].SetTalkNPC(NPC.whoAmI);  // Change who the player is talking to to the new Town NPC.
                        NPCsync.rescuedFarmer = true; // Set our rescue bool to true.

                        // We need to sync these changes in multiplayer.
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NetMessage.SendData(MessageID.SyncTalkNPC, -1, -1, null, i);
                            NetMessage.SendData(MessageID.WorldData);
                        }
                    }
                }
            }
        }

        public override string GetChat()
        {
            // Make the Town NPC say something unique when first rescued.
            return Language.GetTextValue("Mods.RealmOne.Dialogue.LostFarmer.LostTalk");
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.Player;

            // In this example, the bound NPC can spawn at the surface on grass, dirt, or hallowed grass.
            // We also make sure not spawn the bound NPC if it has already spawned or if the NPC has already been rescued.
            if (player.ZoneFarmy() && spawnInfo.Player.ZoneOverworldHeight && !NPCsync.rescuedFarmer && !NPC.AnyNPCs(ModContent.NPCType<LostFarmerTied>()) && !NPC.AnyNPCs(ModContent.NPCType<LostFarmer>()))
            {
                if (spawnInfo.SpawnTileType == ModContent.TileType<FarmSoil>() || spawnInfo.SpawnTileType == TileID.Dirt || spawnInfo.SpawnTileType == TileID.WoodBlock)
                {
                    return 1.1f;
                }
            }
            return 0f;
        }
    }

    public class NPCsync : ModSystem
    {
        public static bool rescuedFarmer = false;

        public override void SaveWorldData(TagCompound tag)
        {
            if (rescuedFarmer)
            {
                tag["rescuedFarmer"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            rescuedFarmer = tag.ContainsKey("rescuedFarmer");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = rescuedFarmer;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            rescuedFarmer = flags[0];
        }
    }
}