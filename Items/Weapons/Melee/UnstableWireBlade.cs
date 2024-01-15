/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Sword;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.Melee
{
    public class UnstableWireBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unstable WireBlade"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A metallic and a wire-infested sword, capable of transfering viruses"
             + "\n'The strength of conduction of this sword can magnetise raw lightning from the skies'");
            ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/Melee/UnstableWireBlade_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 24;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.crit = 4;
            Item.shoot = ModContent.ProjectileType<UnstableWireBladeProj>();
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_MetalSwing");
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 28f;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/Melee/UnstableWireBlade_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.Aqua,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

          public override void MeleeEffects(Player player, Rectangle hitbox)
           {
               int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric, 1f, 1f, 0, default, 1f);
               Main.dust[dust].noGravity = true;
               Main.dust[dust].velocity *= 1.5f;
           }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Collision.AnyCollision(Item.position + Item.velocity, Item.velocity, Item.width, Item.height);
            SoundEngine.PlaySound(rorAudio.ElectricPulse);
            target.AddBuff(BuffID.Electrified, 200);
            for (int i = 0; i < 10; i++)
            {
                Vector2 speed = Main.rand.NextVector2Square(-1f, 1f);

                var d = Dust.NewDustPerfect(target.position, DustID.LunarOre, speed * 5, Scale: 2f);
                ;
                d.noGravity = true;
            }

            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(-1f, 1f);

                var d = Dust.NewDustPerfect(target.position, 202, speed * 5, Scale: 1.5f);
                ;
                d.noGravity = true;
            }
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position += velocity;
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X / 28, velocity.Y / 28, type, damage, knockback, player.whoAmI, velocity.X, velocity.Y);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(Mod, "ScavengerSteel", 12);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}*/