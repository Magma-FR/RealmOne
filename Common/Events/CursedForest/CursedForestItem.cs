using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

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
            Main.NewText("Big balls tbh", 179, 0, 255);
            SoundEngine.PlaySound(SoundID.Roar, new Vector2((int)player.position.X, (int)player.position.Y));
            CursedForestEvent.CursedForest = true;

            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
            else
            {
                CursedForestEvent.CursedForest = true;

            }
            return true;
        }
    }
}
