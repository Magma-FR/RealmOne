/*using RealmOne.Buffs;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    public class DungeonPendant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Pendant");
            Tooltip.SetDefault("'An ancient, but still powerful dungeon charm'"
                + "\nImmunity to spikes"
                + "\nDungeon enemies do 50% less damage to you"
              + "\n10% increased damage and 10+ defense while in the dungeon");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {

            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ZoneDungeon)
                player.AddBuff(ModContent.BuffType<DungeonPendantBuff>(), 60);

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe balls = CreateRecipe();
            balls.AddIngredient(ItemID.Bone, 25);
            balls.AddIngredient(ItemID.DemoniteBar, 10);
            balls.AddIngredient(ItemID.Chain, 2);
            balls.AddTile(TileID.Anvils);
            balls.Register();
        }
    }
}*/