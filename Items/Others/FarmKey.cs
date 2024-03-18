using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Others
{
    public class FarmKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.GoldenKey);
        }
    }
}