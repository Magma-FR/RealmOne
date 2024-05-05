using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Jungle
{
    public class JungleFisharang : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemGlowy.AddGlowMask(Item.type, Texture + "_Glow");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Quest;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<JungleFisharangProj>();
            Item.shootSpeed = 14f;
            Item.noUseGraphic = true;
            Item.questItem = true;
            Item.uniqueStack = true;
            Item.noMelee = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxProjectiles; ++i)
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                    return false;
            return true;
        }

        public override bool IsQuestFish()
        {
            return true; // Makes the item a quest fish
        }

        public override bool IsAnglerQuestAvailable()
        {
            return !Main.hardMode; // Makes the quest only appear in hard mode. Adding a '!' before Main.hardMode makes it ONLY available in pre-hardmode.
        }

        public override void AnglerQuestChat(ref string description, ref string catchLocation)
        {
            // How the angler describes the fish to the player.
            description = "The Jungle has one of the most fascinating looking fish I've caught. Plus, if you want a weapon, this weapon will be viable... as a boomerang.";
            // What it says on the bottom of the angler's text box of how to catch the fish.
            catchLocation = "Caught in the Jungle";
        }
    }

    public class JungleFisharangProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 18;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 32;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -2;
            Projectile.timeLeft = 600;
            Projectile.scale = 1f;
        }

        public override void AI()
        {
            Projectile.rotation += 0.13f;
            {
                /*    int dust2 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Sunflower, Projectile.velocity.X * 0.6f, Projectile.velocity.Y * 0.6f);
                    Main.dust[dust2].velocity *= 0.05f;
                    Main.dust[dust2].scale = .5f;
                    Main.dust[dust2].noGravity = true;
                */
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = Request<Texture2D>("RealmOne/Assets/Effects/seat").Value;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                var frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = Projectile.scale * (Projectile.oldPos.Length - k) / (Projectile.oldPos.Length * 2.2f);
                Color color = Main.DiscoColor * (0.3f - Projectile.alpha) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.oldRot[k], frame.Size() / 2f, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}