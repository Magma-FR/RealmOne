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

    public class LostFarmer : ModNPC
    {

        public const string ShopName = "Shop";
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 0; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
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
            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetBiomeAffection<Biomes.Farm.FarmSurface>(AffectionLevel.Love) // Example Person likes the Example Surface Biome
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike) // Dislikes living near the merchant.
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate); // Hates living near the demolitionist.
            // < Mind the semicolon!

            // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.
            
           
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 15;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            AnimationType = NPCID.Guide;
        }
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

          
            // These are things that the NPC has a chance of telling you when you talk to it.
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.LostFarmer.StandardDialogue1"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.LostFarmer.StandardDialogue2"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.LostFarmer.StandardDialogue3"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.LostFarmer.CommonDialogue"));
            chat.Add(Language.GetTextValue("Mods.RealmOne.Dialogue.LostFarmer.RareDialogue"), 0.3);

            

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
				new FlavorTextBestiaryInfoElement("Abandoned, just like his home, the Lost Farmer was a child left by himself in a cursed timeline"),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
            });
        }
        public override List<string> SetNPCNameList()
        {
            return new List<string>() {
                "Ralph",
                "Dustin",
                "Scott",
                "Johnston"
            };
        }
        public override void SetChatButtons(ref string button, ref string button2)
        { // What the chat buttons are when you open up the chat UI
            button = Language.GetTextValue("LegacyInterface.28");
         
        }
        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                // We want 3 different functionalities for chat buttons, so we use HasItem to change button 1 between a shop and upgrade action.

             

                shop = ShopName; // Name of the shop tab we want to open.
            }
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;

            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Hay);
            }

            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                string variant = "";
             
                int headGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Head").Type;
                int armGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Arm").Type;
                int legGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Leg").Type;
                int forkGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Leg").Type;


                // Spawn the gores. The positions of the arms and legs are lowered for a more natural look.

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, headGore, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 22), NPC.velocity, forkGore);

                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
            }
        
    }
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, ShopName)
                .Add(new Item(ModContent.ItemType<StunSeed>()) { shopCustomPrice = Item.buyPrice(gold: 5) })

                .Add(new Item(ModContent.ItemType<AntiVenomVial>()) { shopCustomPrice = Item.buyPrice(gold: 2) })


                                .Add(new Item(ModContent.ItemType<SunHat>()) { shopCustomPrice = Item.buyPrice(gold: 2, silver: 25) })

                .Add(new Item(ModContent.ItemType<Wheat>()) { shopCustomPrice = Item.buyPrice(silver: 2) })
                 .Add(new Item(ItemID.Hay) { shopCustomPrice = Item.buyPrice(copper: 10) })
                 .Add(new Item(ModContent.ItemType<FarmAcorn>()) { shopCustomPrice = Item.buyPrice(copper: 80) })
                                  .Add(new Item(ItemID.Sunflower) { shopCustomPrice = Item.buyPrice(silver: 30) })

                 .Add(new Item(ModContent.ItemType<FarmKey>()) { shopCustomPrice = Item.buyPrice(gold: 3) })
                                  .Add(new Item(ModContent.ItemType<SuperWheat>()) { shopCustomPrice = Item.buyPrice(gold: 1) })



                .Add<SquirmoSummon>(Condition.TimeNight);



            npcShop.Register(); // Name of this shop tab
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SunHat>()));
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

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 8;
            knockback = 1f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<AcornNadeProj>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 4f;
            randomOffset = 1f;
            // SparklingBall is not affected by gravity, so gravityCorrection is left alone.
        }
    }
}
    

