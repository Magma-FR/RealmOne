using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Bullet;
using RealmOne.Projectiles.Shortsword;
using RealmOne.Projectiles.Throwing;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using System.Security.Principal;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.Melee
{
    public class GildedGladius : ModItem
    {
        private int stabCount = 0;

        public override void SetStaticDefaults()
        {
          

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 14;

            Item.width = 42;
            Item.height = 42;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.crit = 4;
            Item.UseSound = SoundID.Item1;

            Item.flame = true;
            Item.shoot = ProjectileType<GildedGladiusProj>();
            Item.shootSpeed = 4f;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {

            if (Main.rand.NextBool(10))
                type = ModContent.ProjectileType<GildedGladiusThrown>();
         



        }


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Orange.ToVector3() * 0.8f);

            if (Item.timeSinceItemSpawned % 12 == 0)
            {
                Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

                Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
                float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
                var velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

                var dust = Dust.NewDustPerfect(center + direction * distance, DustID.GoldFlame, velocity);
                dust.scale = 0.8f;
                dust.fadeIn = 1.1f;
                dust.noGravity = true;
                dust.noLight = true;
                dust.alpha = 0;
            }
        }
        
        public override void HoldItem(Player player)
        {
            player.statDefense += 4;


            
        }

       

        public override Vector2? HoldoutOffset()
        {
            var offset = new Vector2(6, 0);
            return offset;
        }
    }
}