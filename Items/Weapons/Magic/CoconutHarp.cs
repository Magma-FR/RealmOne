using Microsoft.Xna.Framework;
using RealmOne.Projectiles.Magic;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Magic
{
    public class CoconutHarp : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coconut Harp"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Plucks out thick palm tree strings to conjure a vibey and tropical note"
            + "\nRandomly shoots out Coconuts that deal heavy knockback"
            + "\n'How the landlubbers kept 'emslves entertained'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Harp");

            Item.shoot = ModContent.ProjectileType<GrassNote>();
            Item.shootSpeed = 10f;
            Item.value = Item.buyPrice(gold: 2, silver: 75);
            Item.mana = 7;
            Item.noMelee = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "CoconutHarp", "'Pluck and Plunder!'")
            {
                OverrideColor = new Color(70, 250, 22)
            };
            tooltips.Add(line);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            for (int i = 0; i < 60; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.SeaOatsOasis, speed * 5, Scale: 1.3f);
                ;
                d.noGravity = true;
            }

            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Every projectile shot from this gun has a 1/3 chance of being an ExampleInstancedProjectile
            if (Main.rand.NextBool(4))
                type = ModContent.ProjectileType<CoconutProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.PalmWood, 15)
            .AddIngredient(ItemID.Coconut, 1)
            .AddRecipeGroup("Sand", 12)
            .AddIngredient(ItemID.Cobweb, 4)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-8, 0);
            return offset;
        }
    }
}