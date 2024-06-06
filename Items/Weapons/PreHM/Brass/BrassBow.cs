using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Brass
{
    public class BrassBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Bow");
            Tooltip.SetDefault("Has a 20% chance of shooting a copper coin");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.damage = 3;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Arrow;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.value = Item.buyPrice(copper: 80, silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 22f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.scale = 1f;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (Main.rand.NextBool(5))
                type = ProjectileID.CopperCoin;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(1, 0);
            return offset;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "BrassIngot", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}