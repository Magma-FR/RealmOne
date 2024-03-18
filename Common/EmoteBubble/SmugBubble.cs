using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace RealmOne.Common.EmoteBubble
{
    public class SmugBubble : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            AddToCategory(EmoteID.Category.General);
        }
    }
}