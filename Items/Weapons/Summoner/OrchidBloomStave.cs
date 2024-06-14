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

    public class OrchidBloomStave : ModItem
    {
        bool used = false;
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "", "");
            var line1 = new TooltipLine(Mod, "", "");
            var line2 = new TooltipLine(Mod, "", "");
            line = new TooltipLine(Mod, "OrchidBloomStave", "Summons a beatiful orchid flower")
            {
                OverrideColor = new Color(255, 255, 255)
            };

            line1 = new TooltipLine(Mod, "OrchidBloomStave", "Can be resummoned to power it up to three times")
            {
                OverrideColor = new Color(255, 255, 255)
            };

            line2 = new TooltipLine(Mod, "OrchidBloomStave", "Requires an empty minion slot per power up")
            {
                OverrideColor = new Color(255, 255, 255)
            };

            tooltips.Add(line);
            tooltips.Add(line1);
            tooltips.Add(line2);

        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.width = 26;
            Item.height = 34;
            Item.useTime = 40;
            Item.mana = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6f;
            Item.value = 30000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<OrchidFlower>();
            Item.shoot = ModContent.ProjectileType<OrchidBloom>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = player.position;
        }

        public override bool Shoot(Player p, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            p.AddBuff(Item.buffType, 2);

            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            
            projectile.originalDamage = Item.damage;

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, -10);
        }

    }


}
