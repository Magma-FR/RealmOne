using RealmOne.Tiles.Furniture.MusicBoxx;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables.FarmStuff
{
    public class FarmMusicItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/CottageOrchestra"), ModContent.ItemType<FarmMusicItem>(), ModContent.TileType<FarmMusicTile>());
        }

        public override void SetDefaults()
        {
            Item.DefaultToMusicBox(ModContent.TileType<FarmMusicTile>(), 0);
        }
    }
}