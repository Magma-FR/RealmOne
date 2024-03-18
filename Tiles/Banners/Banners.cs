using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Placeables.BannerItems;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.NPCs.Enemies.Impact;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RealmOne.Tiles.Banners
{
    public class Banners : ModTile
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
            name.SetDefault("Banners");
            AddMapEntry(new Color(200, 200, 200), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 18;
            switch (style)
            {
                case 0:
                    Item.NewItem(new EntitySource_TileBreak(i * 16, j * 16), i * 16, j * 16, 16, 48, ModContent.ItemType<BannerItem.AcornSprinterB>());
                    break;

                case 1:
                    Item.NewItem(new EntitySource_TileBreak(i * 16, j * 16), i * 16, j * 16, 16, 48, ModContent.ItemType<BannerItem.AcornStiltB>());
                    break;

                case 2:
                    Item.NewItem(new EntitySource_TileBreak(i * 16, j * 16), i * 16, j * 16, 16, 48, ModContent.ItemType<BannerItem.ImpactTurretB>());
                    break;

                case 3:
                    Item.NewItem(new EntitySource_TileBreak(i * 16, j * 16), i * 16, j * 16, 16, 48, ModContent.ItemType<BannerItem.EslimeB>());
                    break;

                case 4:
                    Item.NewItem(new EntitySource_TileBreak(i * 16, j * 16), i * 16, j * 16, 16, 48, ModContent.ItemType<BannerItem.EeyeB>());
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
                        Main.SceneMetrics.NPCBannerBuff[ModContent.NPCType<AcornSprinter>()] = true;
                        Main.SceneMetrics.hasBanner = true;
                        break;

                    case 1:
                        Main.SceneMetrics.NPCBannerBuff[ModContent.NPCType<AcornStiltWalker>()] = true;
                        Main.SceneMetrics.hasBanner = true;
                        break;

                    case 2:
                        //        Main.SceneMetrics.NPCBannerBuff[ModContent.NPCType<ImpactTurret>()] = true;
                        Main.SceneMetrics.hasBanner = true;
                        break;

                    case 3:
                        Main.SceneMetrics.NPCBannerBuff[ModContent.NPCType<Eslime>()] = true;
                        Main.SceneMetrics.hasBanner = true;
                        break;

                    case 4:
                        Main.SceneMetrics.NPCBannerBuff[ModContent.NPCType<ImpactDrone>()] = true;
                        Main.SceneMetrics.hasBanner = true;
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
}