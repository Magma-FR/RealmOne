using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Tiles.Blocks
{
    internal class OldGoldOreT : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileLighted[Type] = true;

            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 350;

            LocalizedText name = CreateMapEntryName();
            name.SetDefault("Old Gold Ore");
            AddMapEntry(new Color(243, 255, 5), name);

            DustType = DustID.Gold;

            HitSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/OldGoldTink");
            MineResist = 1.5f;
            MinPick = 60;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
            => NPC.downedBoss1;

        public override bool CanExplode(int i, int j)
            => NPC.downedBoss1;

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.13f;
            g = 0.11f;
            b = 0.08f;
        }
    }
}