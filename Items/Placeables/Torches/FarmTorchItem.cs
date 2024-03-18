using Microsoft.Xna.Framework;
using RealmOne.Items.Placeables.FarmStuff;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables.Torches
{
    public class FarmTorchItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Farm Torch");
            Tooltip.SetDefault("Surprised its still flamable!");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        { // Overrides the default sorting method of this Item.
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Torches; // Vanilla usually matches sorting methods with the right type of item, but sometimes, like with torches, it doesn't. Make sure to set whichever items manually if need be.
        }

        public override void SetDefaults()
        {
            Item.flame = true;
            Item.noWet = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.holdStyle = ItemHoldStyleID.HoldFront;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.White;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Torches.FarmTorch>();
            Item.width = 10;
            Item.height = 12;
            Item.value = 50;
        }

        public override void HoldItem(Player player)
        {
            // Randomly spawn sparkles when the torch is held. Twice bigger chance to spawn them when swinging the torch.
            if (Main.rand.NextBool(player.itemAnimation > 0 ? 50 : 100))
            {
                Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 5, 5, DustID.Torch);
            }

            Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);

            Lighting.AddLight(position, 1.3f, 0.8f, 0.1f);
        }

        public override void PostUpdate()
        {
            Vector2 ItemPos = Item.Center;
            if (!Item.wet)
            {
                Lighting.AddLight(ItemPos, 1.3f, 0.8f, 0.1f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(3)
                .AddIngredient(ModContent.ItemType<TatteredWood>(), 1)
                                .AddIngredient(ItemID.Gel, 2)

                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}