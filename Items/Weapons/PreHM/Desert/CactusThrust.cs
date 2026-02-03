using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Desert
{
    public class CactusThrust : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Thrusts a spiky spear that has a chace to thrust out a bunch of sticky prickly pears that stick onto enemies");
            DisplayName.SetDefault("Cactus Thrust");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.Spears[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 95);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 36;
            Item.useTime = 36;
            Item.UseSound = SoundID.DD2_GoblinBomberThrow;
            Item.autoReuse = true;

            Item.damage = 14;
            Item.knockBack = 5f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;

            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<CactusThrustProj>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Cactus, 20);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}