using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions.Cologne
{
    public class TraditionalCologne : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Traditional Cologne"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A classic bottle of fine moonglow and skybreeze liquid"
            + "\n'Using the cologne provides you with a distinct aroma'"
            + "\n'Gives the player cheaper shop prices, increased defense by 3+, reduced enemy aggression and 8% increased movement speed '");
        }

        public override void SetDefaults()
        {
            Item.height = 32;
            Item.width = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = 500;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.damage = 0;
            Item.DamageType = DamageClass.Melee;
            Item.buffType = ModContent.BuffType<TraditionalCologneBuff>();
            Item.buffTime = 7200;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Cologne");
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PurificationPowder, 0f, 0f, 35, default, 3f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 2f;
        }

        public override void HoldItem(Player player)
        {
            SoundEngine.PlaySound(rorAudio.SFX_CologneClink);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Moonglow, 2);
            recipe.AddIngredient(ItemID.Cloud, 2);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}