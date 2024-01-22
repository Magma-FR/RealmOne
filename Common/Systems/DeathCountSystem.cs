using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace RealmOne.Common.Systems
{
    public class DeathCountPlayer : ModPlayer
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
                Main.NewText("test");
                if (DeathCount == 10)
                {
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

    public class PlayerBanners : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            DustType = -1;
            LocalizedText name = CreateMapEntryName();

            AddMapEntry(new Color(200, 200, 200), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 18;
            switch (style)
            {
                case 0:
                    Item.NewItem(new EntitySource_TileBreak(i * 16, j * 16), i * 16, j * 16, 16, 48, ModContent.ItemType<PlayerBannerTen>());
                    break;



                default:
                    return;
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                Player player = Main.LocalPlayer;
                int style = Main.tile[i, j].TileFrameX / 18;
                switch (style)
                {
                    case 0:
                        player.AddBuff(ModContent.BuffType<PlayerBannerTenBuff>(), 4); //Too lazy
                        Main.SceneMetrics.hasBanner = true; //IDK what this does
                        break;



                    default:
                        return;
                }
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
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
            Item.DefaultToPlaceableTile(ModContent.TileType<PlayerBanners>(), 0);
        }
    }

    public class PlayerBannerTenBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.05f;
            player.moveSpeed *= 1.05f;
            player.statDefense += 2;
        }
    }
}