using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Whip;
using RealmOne.Rarities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.BloodMoon
{
    public class ToothedTendril : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
                     Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;

        }
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<ToothedTendrilProj>(), 17, 3, 4);
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
            
        }


        public override bool MeleePrefix()
        {
            return true;
        }
    }

    public class ToothedTendrilProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.DefaultToWhip();

            Projectile.WhipSettings.Segments = 18;
            Projectile.WhipSettings.RangeMultiplier = 1f;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.t_Flesh, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, Alpha: 180, Scale: 0.8f);

        }
        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.OrangeRed);
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            
            
          //  Gore.NewGore(Projectile.GetSource_Death(), Projectile.Left, Vector2.Zero, Mod.Find<ModGore>("ToothGore1").Type, 1f);
         //   Gore.NewGore(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, Mod.Find<ModGore>("ToothGore2").Type, 1f);
        //    Gore.NewGore(Projectile.GetSource_Death(), Projectile.Right, Vector2.Zero, Mod.Find<ModGore>("ToothGore3").Type, 1f);


        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.



            return false;
        }
        public override void Kill(int timeLeft)
        {

        }
    }
}
