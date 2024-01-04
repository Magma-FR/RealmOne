using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Brass
{
    public class BrassSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brass Sword");
            Tooltip.SetDefault("Has a chance to shrink in size and significantly increase its usetime");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 21;
            Item.useAnimation = 21;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.useTurn = true;
            Item.crit = 2;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_MetalSwing");
            Item.autoReuse = true;

        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.CopperCoin, 0f, 0f, 0, default, 0.7f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.5f;

        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(rorAudio.SFX_Porce);

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod, "BrassIngot", 6);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}