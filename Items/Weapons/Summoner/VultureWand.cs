using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Buffs.Summons;
using RealmOne.Common.Global;
using RealmOne.Common.Systems;
using RealmOne.Items.Others;
using RealmOne.Projectiles.Summon;
using RealmOne.RealmPlayer;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Summoner
{

    public class VultureWand : ModItem
    {
        bool used = false;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");
            var line1 = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "VultureWand", "Summon baby vultures to assist you in combat")
            {
                OverrideColor = new Color(255, 255, 255)
            };

            line1 = new TooltipLine(Mod, "VultureWand", "Baby vultures pierce all of the enemy's defense")
            {
                OverrideColor = new Color(255, 255, 255)
            };

            tooltips.Add(line);
            tooltips.Add(line1);
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.width = 46;
            Item.height = 46;
            Item.useTime = 30;
            Item.mana = 9;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.ArmorPenetration = 999999;
            Item.knockBack = 6f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Gray;
            Item.UseSound = SoundID.Item44;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<BabyVultures>();
            Item.shoot = ModContent.ProjectileType<BabyVulture>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player p, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            p.AddBuff(Item.buffType, 2);

            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);

            projectile.originalDamage = Item.damage;

            return false;
        }

        public override bool? UseItem(Player player)
        {
            if (!used)
            {
                SoundEngine.PlaySound(SoundID.NPCHit28 with { Pitch = 0.25f, PitchVariance = 0.5f }, player.Center);

                used = true;
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCHit28 with { Pitch = 0.25f, PitchVariance = 0.5f }, player.Center);
                used = false;
            }

            return base.UseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, -10);
        }

    }


}
