using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Common.Recipess
{
    public class GroupRECIPEE : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup woods = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Wood"]];

            woods.ValidItems.Add(ModContent.ItemType<TatteredWood>());

            RecipeGroup BaseGroup(object GroupName, int[] Items)
            {
                string Name = "";
                Name += GroupName switch
                {
                    int i => Lang.GetItemNameValue((int)GroupName),
                    short s => Lang.GetItemNameValue((short)GroupName),
                    _ => GroupName.ToString(),
                };
                return new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Name, Items);
            }

            RecipeGroup.RegisterGroup("RealmOne:CopperBars", BaseGroup(ItemID.CopperBar, new int[]
          { ItemID.CopperBar, ItemID.TinBar }));
            RecipeGroup.RegisterGroup("RealmOne:GoldBars", BaseGroup(ItemID.GoldBar, new int[]
                { ItemID.GoldBar, ItemID.PlatinumBar }));

            RecipeGroup.RegisterGroup("RealmOne:Any HM Evil Drop", BaseGroup(ItemID.CursedFlame, new int[]
                { ItemID.CursedFlame, ItemID.Ichor }));

            RecipeGroup.RegisterGroup("RealmOne:Any evil chunk", BaseGroup(ItemID.ShadowScale, new int[]
                { ItemID.ShadowScale, ItemID.TissueSample }));

            RecipeGroup.RegisterGroup("RealmOne:SilverBars", BaseGroup(ItemID.SilverBar, new int[]
                { ItemID.SilverBar, ItemID.TungstenBar }));

            RecipeGroup.RegisterGroup("RealmOne:Tier3HMBar", BaseGroup(ItemID.AdamantiteBar, new int[]
                { ItemID.AdamantiteBar, ItemID.TitaniumBar }));
        }
    }
}