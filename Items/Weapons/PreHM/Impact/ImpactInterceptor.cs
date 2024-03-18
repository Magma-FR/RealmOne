using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Debuffs;
using RealmOne.Common.Core;
using RealmOne.Common.Systems;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Impact
{
    public class ImpactInterceptor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impact Interceptor");
            Tooltip.SetDefault("Calls down randomly positioned pulse lasers that spark into electricity when homed onto an enemy"
                + "\nThe lasers depend on the mouse position when firing"
                + "\n'No WIFI password required'");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Impact/ImpactInterceptor_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.autoReuse = true;
            Item.useTurn = true;
            Item.mana = 8;
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 2f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 4f;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(silver: 90);
            Item.shoot = ProjectileType<ImpactSonarShot>();
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Sonar");
            Item.scale = 1f;
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(1, 0);
            return offset;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Impact/ImpactInterceptor_Glow", AssetRequestMode.ImmediateLoad).Value;
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

        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(rorAudio.PulsaPickup);

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
            .AddIngredient(Mod, "ImpactTech", 12)

            .AddTile(TileID.Anvils)
            .Register();
        }
    }

    public class ImpactSonarShot : ModProjectile
    {
        public override string Texture => Helper.Empty;

        public override void SetDefaults()
        {
            Projectile.height = 400;
            Projectile.width = 400;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -2;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.ai[0] += 0.05f;
            if (Projectile.ai[0] > 1)
            {
                Projectile.Kill();
            }
        }

        public override void PostAI()
        {
            if (Projectile.ai[1] == 1)
                Projectile.damage = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = Request<Texture2D>("RealmOne/Assets/Effects/Pulsee").Value;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); float alpha = MathHelper.Lerp(4, 0, Projectile.ai[0]);
            for (int i = 0; i < 3; i++)
                Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.Cyan * (3 - alpha), Projectile.rotation, tex.Size() / 2, Projectile.ai[0], SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.ZoomMatrix); return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hitinfo, int damage)
        {
            Projectile.ai[1] = 1;
            target.AddBuff(BuffType<AltElectrified>(), 180);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}