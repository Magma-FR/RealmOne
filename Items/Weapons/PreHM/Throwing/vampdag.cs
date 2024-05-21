using RealmOne.Projectiles.Throwing;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Throwing
{
    public class vampdag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Daggers"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A cheaper but weaker version of the Vampire Knives"
                + "\nHeals the player on hit");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 15;
            Item.height = 28;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 0, 0, 30);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<vampdagProjectile>();
            Item.shootSpeed = 15f;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
        }
    }
}