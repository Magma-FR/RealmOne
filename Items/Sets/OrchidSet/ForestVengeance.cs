using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Projectiles.Magic;
using RealmOne.Projectiles.Piggy;
using RealmOne.RealmPlayer;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Sets.OrchidSet
{
    public class ForestVengeance : ModItem
    {
        private int useDelay = 64; //usetime - 1
        private int cd = 8;
        private int cd2 = 0;
        private Vector2 mouse;

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.useTurn = false;
            Item.damage = 28;
            Item.useTime = 115;
            Item.useAnimation = 115;
            Item.mana = 12;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.value = Item.sellPrice(0, 0, 57, 35);
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 3f;
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            Vector2 hold = new Vector2(-2, 0);

            return hold;
        }

        public override bool? UseItem(Player player)
        {
            if (cd2 > 0)
                cd2--;

            if (cd2 == 0)
            {
                cd2 = 116;
                mouse = Main.MouseWorld;
            }

            if (mouse.X < player.position.X)
                player.direction = -1;
            if (mouse.X > player.position.X)
                player.direction = 1;

            if (useDelay > 8)
            {
                if (cd > 0)
                    cd--;

                if (cd == 0)
                {
                    cd = 8;
                    SoundEngine.PlaySound(SoundID.Item8);
                    Vector2 spawnLoc = new Vector2(player.Center.X + Main.rand.Next(-150, 150), player.Center.Y + Main.rand.Next(-150, 150));
                    Vector2 d = (player.Center - spawnLoc).SafeNormalize(Vector2.UnitX);
                    Projectile.NewProjectile(player.GetSource_FromThis(), spawnLoc, d, ModContent.ProjectileType<ForestSpirit>(), 0, 8f, Main.myPlayer);
                }
            }

            if (useDelay > 0)
                useDelay--;

            if (useDelay == 0)
            {
                cd = 72;
                useDelay = 114; //usetime - 1
                SoundEngine.PlaySound(SoundID.Item8);
                Vector2 dir = (player.Center - Main.MouseWorld).SafeNormalize(Vector2.UnitX);
                Projectile.NewProjectile(player.HeldItem.GetSource_FromThis(), player.Center, dir * -4f, ModContent.ProjectileType<ForestVengeanceProj>(), player.HeldItem.damage, 0f, Main.myPlayer);
                for (int i = 0; i < 60; i++)
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(0.8f, 0.8f);
                    var dus = Dust.NewDustPerfect(player.Center, DustID.ChlorophyteWeapon, speed * 5f, Scale: 1.2f);
                    ;
                    dus.noGravity = true;
                }
            }

            return false;
        }
    }
}