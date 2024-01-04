/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Items.Ammo;
using RealmOne.Projectiles.Bullet;
using RealmOne.Rarities;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Shotguns
{
    public class HPChainShot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lovecraft's Chain Shotgun");
            Tooltip.SetDefault("Shoots a 3 burst shot "
            + "\nRight click to shoot out a chain."
            + "\nThe gun extends into the chain!");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = 30000;
            Item.rare = RarityType<ModRarities>();
            Item.autoReuse = true;
            Item.useAmmo = ModContent.ItemType<RustedBullets>();
            Item.noMelee = true;
            Item.shootSpeed = 16f;
            Item.shoot = ProjectileType<OldChainHook>();

        }

        public override bool CanUseItem(Player player)
        {


            if (player.altFunctionUse == 2)
            {
                Item.useTime = 44;
                Item.useAnimation = 44;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.UseSound = SoundID.Item1;
                Item.autoReuse = false;
                Item.shootSpeed = 15f;
                Item.shoot = ProjectileType<OldChainHook>();
                Item.damage = 14;
                Item.knockBack = 4f;

            }
            else
            {
                Item.shootSpeed = 80f;
                Item.useTime = 73;
                Item.useAnimation = 73;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noUseGraphic = false;
                Item.UseSound = rorAudio.SFX_PumpShotgun;
                Item.shoot = ProjectileID.Bullet;
                Item.damage = 4;
                Item.knockBack = 1f;
                Item.useAmmo = ModContent.ItemType<RustedBullets>();
            }

            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)

                type = ProjectileType<OldChainHook>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            if (player.altFunctionUse == 2)
            {
            }
            else
            {
                Lighting.AddLight(player.position, 0.8f, 0.5f, 0.0f);
                const int NumProjectiles = 3;

                for (int i = 0; i < NumProjectiles; i++)
                {

                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(14));
                    newVelocity *= 1f - Main.rand.NextFloat(0.2f);

                    Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                }

                player.GetModPlayer<Screenshake>().SmallScreenshake = true;

                Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
                if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                    position += muzzleOffset;
                Gore.NewGore(source, player.Center + muzzleOffset * 1, new Vector2(player.direction * -1, -0.5f) * 2, Mod.Find<ModGore>("TommyGunPellets").Type, 1f);

                Projectile.NewProjectile(player.GetSource_ItemUse(Item), position + muzzleOffset, Vector2.Zero, ProjectileType<TommyGunBarrelFlash>(), 0, 0, player.whoAmI);
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Chain, 5);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(-2, 0);
            return offset;
        }
    }

    internal class OldChainHook : ModProjectile
    {

        private static Asset<Texture2D> chainTexture;

        public override void Load()
        { // This is called once on mod (re)load when this piece of content is being loaded.
          // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
            chainTexture = Request<Texture2D>("RealmOne/Projectiles/Returning/OldChain");
        }

        public override void Unload()
        { // This is called once on mod reload when this piece of content is being unloaded.
          // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
            chainTexture = null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("${ProjectileName.ChainGuillotine}");
        }

        public override void SetDefaults()
        {
            // Copies the attributes of the Amethyst hook's projectile.
            Projectile.width = 34;
            Projectile.height = 18;
            Projectile.tileCollide = true;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.damage = 20;
            Projectile.aiStyle = ProjAIStyleID.Harpoon;
            AIType = ProjectileID.ChainGuillotine;
            Projectile.light = 0.2f;
            Projectile.CloneDefaults(ProjectileID.ChainGuillotine);
            Projectile.alpha = 0;

        }
        // Amethyst Hook is 300, Static Hook is 600.

        public override void Kill(int timeLeft)
        {
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("OldChainGore").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Left, Vector2.Zero, Mod.Find<ModGore>("OldChainGore").Type, 1f);
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.Right, Vector2.Zero, Mod.Find<ModGore>("OldChainGore").Type, 1f);

            for (int i = 0; i < 6; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Iron, 0f, 0f, 0, default, 0.6f);

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }
        public override bool PreDrawExtras()
        {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 center = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - Projectile.Center;
            float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
            float distanceToPlayer = directionToPlayer.Length();

            while (distanceToPlayer > 20f && !float.IsNaN(distanceToPlayer))
            {
                directionToPlayer /= distanceToPlayer; // get unit vector
                directionToPlayer *= chainTexture.Height(); // multiply by chain link length

                center += directionToPlayer; // update draw position
                directionToPlayer = playerCenter - center; // update distance
                distanceToPlayer = directionToPlayer.Length();

                Color drawColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16));

                // Draw chain
                Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
                    chainTexture.Value.Bounds, drawColor, chainRotation,
                    chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
            }
            // Stop vanilla from drawing the default chain.
            return false;
        }
        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Iron, Projectile.velocity.X * 0.8f, Projectile.velocity.Y * 0.8f, Alpha: 129);
            Vector2 center = Projectile.Center;
            for (int j = 0; j < 5; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.Sandnado, 0f, 0f, 100, default, 1f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;

                Vector2 speed = Main.rand.NextVector2CircularEdge(0.25f, 0.25f);

            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            player.GetModPlayer<Screenshake>().SmallScreenshake = true;
            SoundEngine.PlaySound(SoundID.DD2_CrystalCartImpact);
            Projectile.Kill();

        }
    }
}
*/
