using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Tools.Hooks
{
    internal class BloodHook : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.shootSpeed = 25f;
            Item.shoot = ModContent.ProjectileType<BloodHookProj>();
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.knockBack = 4;
            Item.value = 30000;
            Item.rare = ItemRarityID.Blue;
            Item.damage = 25;

            Item.crit = 2;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "BloodHook", "Grasp onto death")
            {
                OverrideColor = new Color(200, 80, 90)
            };
            tooltips.Add(line);
        }

        internal class BloodHookProj : ModProjectile
        {
            private static Asset<Texture2D> chainTexture;

            public override void Load()
            {
                chainTexture = ModContent.Request<Texture2D>("RealmOne/Items/Tools/Hooks/BloodHookChain");
            }

            public override void Unload()
            { // This is called once on mod reload when this piece of content is being unloaded.
              // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
                chainTexture = null;
            }

            public override void OnKill(int timeLeft)
            {
                for (int i = 0; i < 10; i++)
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.t_Flesh, 0f, 0f, 0, default, 0.6f);

                Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.ChesterOpen);
            }

            public override void SetStaticDefaults()
            {
            }

            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.GemHookRuby); // Copies the attributes of the Amethyst hook's projectile.
                Projectile.damage = 25;
                Projectile.CritChance = 5;
            }

            // Use this hook for hooks that can have multiple hooks mid-flight: Dual Hook, Web Slinger, Fish Hook, Static Hook, Lunar Hook.
            public override bool? CanUseGrapple(Player player)
            {
                int hooksOut = 0;
                for (int l = 0; l < 1000; l++)
                    if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type)
                        hooksOut++;

                return hooksOut <= 1;
            }

            public override float GrappleRange()
            {
                return 410f;
            }

            public override void NumGrappleHooks(Player player, ref int numHooks)
            {
                numHooks = 2; // The amount of hooks that can be shot out
            }

            // default is 11, Lunar is 24
            public override void GrappleRetreatSpeed(Player player, ref float speed)
            {
                speed = 17f; // How fast the grapple returns to you after meeting its max shoot distance
            }

            public override void GrapplePullSpeed(Player player, ref float speed)
            {
                speed = 14; // How fast you get pulled to the grappling hook projectile's landing position
            }

            // Adjusts the position that the player will be pulled towards. This will make them hang 50 pixels away from the tile being grappled.
            public override void GrappleTargetPoint(Player player, ref float grappleX, ref float grappleY)
            {
                Vector2 dirToPlayer = Projectile.DirectionTo(player.Center);
                float hangDist = 40f;
                grappleX += dirToPlayer.X * hangDist;
                grappleY += dirToPlayer.Y * hangDist;
            }

            // Draws the grappling hook's chain.
            public override bool PreDrawExtras()
            {
                Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
                Vector2 center = Projectile.Center;
                Vector2 directionToPlayer = playerCenter - Projectile.Center;
                float chainRotation = directionToPlayer.ToRotation() - MathHelper.PiOver2;
                float distanceToPlayer = directionToPlayer.Length();

                while (distanceToPlayer > 10f && !float.IsNaN(distanceToPlayer))
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
        }
    }
}