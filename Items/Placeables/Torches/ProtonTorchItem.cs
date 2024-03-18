using Microsoft.Xna.Framework;
using RealmOne.Items.Misc.EnemyDrops;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Placeables.Torches
{
    public class ProtonTorchItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Proton Torch");
            Tooltip.SetDefault("Creates electrical sparks when swung and placed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        { // Overrides the default sorting method of this Item.
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Torches; // Vanilla usually matches sorting methods with the right type of item, but sometimes, like with torches, it doesn't. Make sure to set whichever items manually if need be.
        }

        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.flame = true;
            Item.noWet = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.holdStyle = ItemHoldStyleID.HoldFront;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.Torches.ProtonTorch>();
            Item.width = 10;
            Item.height = 12;
            Item.value = 50;
        }

        public override void HoldItem(Player player)
        {
            // Randomly spawn sparkles when the torch is held. Twice bigger chance to spawn them when swinging the torch.
            if (Main.rand.NextBool(player.itemAnimation > 0 ? 50 : 100))
            {
                Dust.NewDust(new Vector2(player.itemLocation.X + 16f * player.direction, player.itemLocation.Y - 14f * player.gravDir), 5, 5, DustID.Electric);
            }

            Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);

            Lighting.AddLight(position, 0.2f, 0.8f, 1.6f);
        }

        public override void PostUpdate()
        {
            Vector2 ItemPos = Item.Center;
            if (!Item.wet)
            {
                Lighting.AddLight(ItemPos, 0.2f, 0.8f, 1.6f);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)
                .AddIngredient(ModContent.ItemType<ImpactTech>(), 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}