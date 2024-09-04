using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Items.Misc.Bars;
using RealmOne.Items.Sets.SunflowerSet;
using RealmOne.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Classless
{
    public class SafeHook : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Generic;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2f;
            Item.value = 30000;
            Item.UseSound = SoundID.Item116;

            Item.rare = ModContent.RarityType<ModRarities>();
            Item.autoReuse = true;
            Item.useAmmo = AmmoID.None;
            Item.noMelee = true;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<SafeHookProj>();
            Item.UseSound = SoundID.Item38;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-3, 1);

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<BrassIngot>(), 6)
            .AddIngredient(ItemID.Wood, 10)
            .AddRecipeGroup("IronBar", 8)
            .AddTile(TileID.WorkBenches)

            .Register();
        }
    }

    public class SafeHookProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 6;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new(18, 30);
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = ProjAIStyleID.Harpoon;
            Projectile.timeLeft = 500;
            AIType = ProjectileID.Harpoon;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.catchItem > 0) // Check if the critter can be caught
            {
                Player player = Main.player[Projectile.owner];

                if (player == null)
                    return;

                Item.NewItem(null, player.getRect(), target.catchItem);
                target.active = false;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item53, Projectile.position);
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(0.5f, 0.5f);
                var d = Dust.NewDustPerfect(Projectile.Center, DustID.Iron, speed * 5, Scale: 1f);
                ;
                d.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            for (int j = 0; j < Projectile.oldPos.Length; j++)
            {
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - j) / (float)Projectile.oldPos.Length);

                Vector2 drawPos = Projectile.oldPos[j] - Main.screenPosition + (Projectile.Size / 2) + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(tex, drawPos, null, color, Projectile.rotation, tex.Size() / 2, Projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
    }
}