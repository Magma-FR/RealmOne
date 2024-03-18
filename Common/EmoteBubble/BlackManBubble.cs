using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace RealmOne.Common.EmoteBubble
{
    public class BlackManBubble : ModEmoteBubble
    {
        public override void SetStaticDefaults()
        {
            AddToCategory(EmoteID.Category.General);
        }
    }
}