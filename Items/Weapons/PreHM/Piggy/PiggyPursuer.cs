using RealmOne.Projectiles.HeldProj;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Piggy
{
    public class PiggyPursuer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Piggy Pursuer"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Swing a delicate but sharp porcelain sword"
             + "\n'And I Huff and I puff and I blow your house down!!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 62;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.reuseDelay = 1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.crit = 3;
            Item.shoot = ModContent.ProjectileType<PiggyPursuerHeld>();
            Item.shootSpeed = 20f;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_MetalSwing");
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<PiggyPursuerHeld>()] < 1;
        }

        public override bool? UseItem(Player player)
        {
            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod, "PiggyPorcelain", 6);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}