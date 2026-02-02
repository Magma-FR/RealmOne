using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace RealmOne.Common.Systems.DeathCountSystemF
{
    public class DeathCountSystem : ModPlayer
    {
        private bool JustDied = false;
        private bool DoSpawnFlag = false;
        private int DeathCount;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("DeathCount", DeathCount);
        }

        public override void LoadData(TagCompound tag)
        {
            DeathCount = tag.GetInt("DeathCount");
        }

        public override void OnRespawn()
        {
            if (JustDied)
            {
                Main.NewText(DeathCount);

                DoSpawnFlag = true;
                //

                JustDied = false;
            }
        }

        public override void PostUpdate()
        {
            if (DoSpawnFlag)
            {
                if (DeathCount == 10)
                {
                    Main.NewText("You've been vanquished 10 times, here's a reward for being reckless!");

                    Item.NewItem(Player.GetSource_FromThis(), Player.Center, ModContent.ItemType<PlayerBannerTen>());
                }
                DoSpawnFlag = false;
            }
        }

        public override void UpdateDead()
        {
            if (!JustDied)
            {
                DeathCount++;
                JustDied = true;
            }
        }
    }

    public class BannerTenTiles : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Origin = new Point16(0, 3);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.addTile(Type);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                if (!player.dead)
                    player.AddBuff(ModContent.BuffType<PlayerBannerTenBuff>(), 10, true);
            }
        }
    }

    public class PlayerBannerTen : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 2;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<BannerTenTiles>(), 0);
        }
    }

    public class PlayerBannerTenBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.05f;
            player.moveSpeed *= 1.05f;
            player.statDefense += 4;
        }
    }
}