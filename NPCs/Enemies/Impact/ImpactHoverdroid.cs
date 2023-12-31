using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader.Utilities;
using RealmOne.Items.Weapons.PreHM.Throwing;
using Terraria.GameContent.ItemDropRules;
using RealmOne.Projectiles.Bullet;
using RealmOne.Items.Misc.EnemyDrops;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Core;
using Terraria.GameContent.Bestiary;

namespace RealmOne.NPCs.Enemies.Impact
{
    public class ImpactHoverdroid : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 0.8f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
            Main.npcFrameCount[Type] = 3;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,

                new FlavorTextBestiaryInfoElement("A hyperactive and trigger happy hoverdroid, shoots a bolt of electricity that stuns the player"),


            });
        }
        public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 30;
			NPC.damage = 10;
			NPC.defense = 2;
			NPC.lifeMax = 58;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath44;
			NPC.value = Item.buyPrice(0,0,2,15);
			NPC.knockBackResist = 0.5f; 
			
			NPC.noGravity = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.SpawnTileY < Main.rockLayer ? SpawnCondition.OverworldNightMonster.Chance * 0.12f : 0f;

		
      
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => GlowMaskSystem.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("RealmOne/NPCs/Enemies/Impact/ImpactHoverdroid_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, screenPos);

        public override void AI()
        {

            Lighting.AddLight(NPC.position, r: 0.1f, g: 0.2f, b: 1.1f);

           
			Player player = Main.player[NPC.target];
		//WIP AI CODE
		}
       
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("HoverdroidGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.TopLeft, NPC.velocity, Mod.Find<ModGore>("HoverdroidGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.TopRight, NPC.velocity, Mod.Find<ModGore>("HoverdroidGore3").Type, 1f);



            }
            for (int k = 0; k < 14; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric, 2.5f * hit.HitDirection, -2.5f, 0, Color.White, 0.9f);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ImpactTech>(), 2, 2, 3));

        }
       
	}


}
