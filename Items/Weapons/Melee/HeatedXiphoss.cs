using Microsoft.Xna.Framework;
using RealmOne.Buffs.Debuffs;
using RealmOne.Common.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Melee
{
    public class HeatedXiphoss : ModItem
    {
        private int hitCount = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heated Xiphos"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Smelted and structured at extreme heat'"
             + "\n'A training sword from the gods'"
             + "\n'The xiphos upgrades in power the more you hit hit enemies'"
             + "\n5 hits of enemies makes the sword increase in the sword's speed"
             + "\n10 hits of enemies makes the sword increase its damage by 20%"
             + "\n15 hits of enemies makes the sword reach it's final stage and inflicts Sharp Heat debuff"
             + "\nThese effects reset on the 16th swing");

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
            Item.knockBack = 3f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.holdStyle = ItemHoldStyleID.HoldUp;
            Item.crit = 4;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_MetalSwing");
            Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch, 0f, 0f, 0, default, 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1.5f;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 180);
            Collision.AnyCollision(Item.position + Item.velocity, Item.velocity, Item.width, Item.height);
            SoundEngine.PlaySound(rorAudio.SFX_MetalClash);

            hitCount++;
            if (hitCount >= 5)
            {
                Item.useTime = 14;
                Item.useAnimation = 14;

                //  hitCount = 0;
            }
            else if (hitCount >= 10)
            {
                Item.damage = 18;

                //hitCount = 0;
            }
            else if (hitCount >= 15)
            {
                target.AddBuff(ModContent.BuffType<SharpHeat>(), 3600);

                //hitCount = 0;
            }
            else if (hitCount >= 20)
            {
                hitCount = 0;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Glass, 12);
            recipe.AddIngredient(ItemID.Torch, 20);
            recipe.AddIngredient(Mod, "BrassIngot", 6);

            recipe.AddTile(TileID.GlassKiln);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}