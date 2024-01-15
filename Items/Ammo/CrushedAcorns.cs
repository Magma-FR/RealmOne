using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Ammo
{
    public class CrushedAcorns : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shoots explosives acorns"); // The item's description, can be set to whatever you want.

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 14; // The width of item hitbox
            Item.height = 14; // The height of item hitbox

            Item.damage = 4; // The damage for projectiles isn't actually 8, it actually is the damage combined with the projectile and the item together
            Item.DamageType = DamageClass.Ranged; // What type of damage does this ammo affect?

            Item.maxStack = 999; // The maximum number of items that can be contained within a single stack
            Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible
            Item.knockBack = 1f; // Sets the item's knockback. Ammunition's knockback added together with weapon and projectiles.
            Item.value = Item.sellPrice(0, 0, 1, 0); // Item price in copper coins (can be converted with Item.sellPrice/Item.buyPrice)
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.shoot = ProjectileID.SeedlerNut; // The projectile that weapons fire when using this item as ammunition.

            Item.ammo = Item.type; // Important. The first item in an ammo class sets the AmmoID to its type
        }

        public override void AddRecipes()
        {
            CreateRecipe(10)

            .AddIngredient(ItemID.Acorn, 3)

            .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}