using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Bullet;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Impact
{
    public class ImpactPulsa : ModItem
    {
        private int shotCount;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact Pulsa"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'A weak, plasma diverged rifle'"
                + "\nMidway through firing the gun, the gun will short circuit and burst a burst of pulse bullets that bounces and targets enemies"
            + "\n'Xeonically Charged!!'");
            //  ItemGlowy.AddGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Impact/ImpactPulsa_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item158;
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.None;
            Item.shoot = ProjectileType<PulsaShot>();
            Item.shootSpeed = 60f;
            Item.noMelee = true;
            Item.crit = 2;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (!player.TryGetModPlayer(out RealmModPlayer modPlayer) || !modPlayer.Static)
            {
                return;
            }

            damage *= 1.5f;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Impact/ImpactPulsa_Glow", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),

                Color.LightCyan,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(Mod, "ImpactTech", 12)
            .AddTile(TileID.Anvils)
            .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            shotCount++;
            if (shotCount == 4)
            {
                shotCount = 0;
                Item.shootSpeed = 80f;
                Item.useTime = 5;
                Item.useAnimation = 5;
                Item.damage = 14;
                Item.crit = 2;
                Item.shoot = ProjectileType<PulsaShot>();

                Item.reuseDelay = 0;

                for (int d = 0; d < 40; d++)
                    Dust.NewDust(player.position, player.width, player.height, DustID.Electric, 0f, 0f, 150, default, 1.5f);
                SoundEngine.PlaySound(rorAudio.ElectricPulse);
            }
            else if (shotCount == 3)
            {
                Item.useTime = 8;
                Item.shoot = ProjectileType<PulsaShot>();
                Item.shootSpeed = 60f;
                Item.damage = 14;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 32;
                Item.height = 32;
                Item.useAnimation = 8;
                Item.reuseDelay = 0;
                Item.UseSound = SoundID.Item158;
                Item.autoReuse = true;
                Item.useAmmo = AmmoID.None;
                for (int d = 0; d < 40; d++)
                    Dust.NewDust(player.position, player.width, player.height, DustID.UnusedWhiteBluePurple, 0f, 0f, 500, default, 1f);
            }
            else
            {
                Item.damage = 14;
                Item.DamageType = DamageClass.Ranged;
                Item.width = 32;
                Item.height = 32;
                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.value = 30000;
                Item.rare = ItemRarityID.Blue;
                Item.knockBack = 4;
                Item.UseSound = SoundID.Item158;
                Item.autoReuse = true;
                Item.useAmmo = AmmoID.None;
                Item.shoot = ProjectileType<PulsaShot>();
                Item.shootSpeed = 60f;
                Item.noMelee = true;
                Item.crit = 2;
                Item.reuseDelay = 38;
                for (int i = 0; i < 50; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Flare_Blue, speed * 5, Scale: 2.5f);
                    ;
                    d.noGravity = true;
                }
            }

            return true;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;

            Rectangle frame;

            if (Main.itemAnimations[Item.type] != null)
                frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
            else
                frame = texture.Frame();

            Vector2 frameOrigin = frame.Size() / 2f;
            var offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
            Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

            float time = Main.GlobalTimeWrappedHourly;
            float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

            time %= 4f;
            time /= 2f;

            if (time >= 1f)
                time = 2f - time;

            time = time * 0.5f + 0.5f;

            for (float i = 0f; i < 1f; i += 0.25f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(35, 139, 221, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            for (float i = 0f; i < 1f; i += 0.34f)
            {
                float radians = (i + timer) * MathHelper.TwoPi;
                spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(58, 62, 190, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-1, 3);
            return offset;
        }

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(rorAudio.PulsaPickup);

            return true;
        }
    }
}