/*using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Potions
{
    public class Cigarette1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cigarette"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'When you light cigarette'" + "\n'Your life burns with it'"
            + "\nWhen holding a cigarette, you gain 25% increased damage and weapon speed but no life regen nor pickup hearts");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 10;
            Item.useTime = 145;
            Item.useAnimation = 145;
            Item.maxStack = 99;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = 500;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item3;
            Item.autoReuse = false;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<CigaretteBuff>();
            Item.buffTime = 3000;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Cigarette");
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 35, default, 2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 2f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Parchment", 2);
            recipe.AddTile(TileID.Tables);
            recipe.Register();
        }
    }
}*/