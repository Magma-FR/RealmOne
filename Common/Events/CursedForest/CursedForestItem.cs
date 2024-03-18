using Microsoft.Xna.Framework;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using RealmOne.Common.Core;

namespace RealmOne.Common.Events.CursedForest
{
    public class CursedForestItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 16;
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = Item.CommonMaxStack;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 10;
            Item.noMelee = true;
            Item.consumable = true;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item43;
        }

        public override bool CanUseItem(Player player)
        {
            if ((player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && !Main.pumpkinMoon && !Main.snowMoon)
                return false;

            if (CursedForestEvent.CursedForest || Main.dayTime)
                return false;

            return true;
        }

        public override bool? UseItem(Player player)
        {
            Main.NewText("The Forest has shifted into a transcendental state!!", 200, 0, 80);
            SoundEngine.PlaySound(rorAudio.LeechHeartEat, new Vector2((int)player.position.X, (int)player.position.Y));
            CursedForestEvent.CursedForest = true;
            for (int i = 0; i < 5; i++)
            {
                GenericGlowParticle particle = new(new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), Color.IndianRed, 0.5f, 120);
                SparkleParticle sparkle = new(Color.Red, 1, new Vector2(player.Center.X + Main.rand.Next(-30, 30), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.7f, 1.2f)), 120);

                ParticleSystem.GenerateParticle(sparkle);
                ParticleSystem.GenerateParticle(particle);
            }
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
            else
            {
                CursedForestEvent.CursedForest = true;
            }
            return true;
        }
    }

    public class CursedBuff : ModBuff
    {
        public override string Texture => Helper.Empty;

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Terraria.ID.BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.aggro += 700;
        }
    }
}