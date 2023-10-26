using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Others;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace RealmOne.NPCs.Enemies.Forest
{
    public class MangoBat : ModNPC
    {
        static Asset<Texture2D> glow;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mango Fruitbat");
            Main.npcFrameCount[NPC.type] = 5;


          

            glow = ModContent.Request<Texture2D>(Texture + "_Glow");





        }

        public override void SetDefaults()
        {

            NPC.width = 20;
            NPC.height = 20;

            NPC.damage = 8;
            NPC.defense = 0;
            NPC.lifeMax = 30;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath4;

            NPC.knockBackResist = 0.34f;

            NPC.npcSlots = 0;
            NPC.aiStyle = NPCAIStyleID.Bat;
            AIType = NPCID.CaveBat;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Farm.FarmSurface>().Type };

            AnimationType = NPCID.CaveBat;

        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Mango, 16));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FarmKey>(), 35, 1, 1));


        }


        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = GetAlpha(Color.White) ?? Color.White;

            if (NPC.IsABestiaryIconDummy)
                color = Color.White;

            Main.EntitySpriteDraw(glow.Value, NPC.Center - screenPos + new Vector2(0, 3), NPC.frame, color, NPC.rotation, NPC.frame.Size() / 2f, 1f, SpriteEffects.None, 0);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MangoBatGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MangoBatGore2").Type, 1f);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNight.Chance * 0.176f;
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Rabies, 180);
        }






        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                   


                new FlavorTextBestiaryInfoElement("When this flying fella is near, expect all ya' mangoes to be gone by the morning. This angry lil bat feeds off the flesh of mango, and when disturbed, humans! "),


            });
        }
    }
}
