using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader.IO;
using RealmOne.Items.Accessories;
using RealmOne.Items.BossSummons;
using RealmOne.Items.Misc;
using RealmOne.Items.Weapons.Ranged;
using RealmOne.Items.Others;
using RealmOne.Biomes.Farm;
using RealmOne.Items.Weapons.PreHM.Throwing;
using RealmOne.Projectiles.Throwing;
using RealmOne.Items.Vanities;

namespace RealmOne.NPCs.TownNPC
{
    [AutoloadHead]

    public class FarmerSlime : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 14; // The total amount of frames the NPC has
            

            NPCID.Sets.ShimmerTownTransform[NPC.type] = false; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

            NPCID.Sets.ShimmerTownTransform[Type] = false ; // Allows for this NPC to have a different texture after touching the Shimmer liquid.

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

            // Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
            // NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.

            // < Mind the semicolon!

            // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.


        }
        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.CloneDefaults(NPCID.TownSlimeBlue);
            AnimationType = NPCID.TownSlimeBlue;
        }
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

          
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.FarmerSlime.First"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.FarmerSlime.Second"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.FarmerSlime.Third"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.FarmerSlime.Forth"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.FarmerSlime.Rare"), 0.3);

            

            return chat; // chat is implicitly cast to a string.
        }
    
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            // If our Town NPC hasn't been rescued, don't arrive.
            if (!NPCsync.rescuedFarmer)
            {
                return false;
            }
            return true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
			
			

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("A slightly drunk and tipsy slime, give him some hay and he might give you something special."),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
            });
        }
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Big Dick Motherfucker",
                "Cuckaroo",
                "Cum Squirter",
                "Dr. Retard"
            };
        }
    
        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Hay);
            }

           
        
    }
       

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wheat>(), 8));
        }

        public override void FindFrame(int frameHeight)
        {
            /*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
        }

       
    }
}
    

