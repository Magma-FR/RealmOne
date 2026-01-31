using RealmOne.Common.Core;
using RealmOne.Common.Core.ParticleContent.Particles;
using RealmOne.Common.Core.ParticleContent;
using RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace RealmOne.Items.Weapons.Melee;

public class BloodthirstyVerminsaw : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
        ItemID.Sets.LockOnIgnoresCollision[Type] = true;
        ItemID.Sets.AnimatesAsSoul[Type] = true;

        Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 2));
    }

    public override void SetDefaults()
    {
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.channel = true;

        Item.crit = 4;
        Item.damage = 25;
        Item.knockBack = 4f;

        Item.width = 78;
        Item.height = 76;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = ButcherRat.FrenzySound;

        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BloodthirstyVerminsawProj>();

        Item.rare = ItemRarityID.Orange;
    }

    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] == 0;
    }
}

public class SawBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoTimeDisplay[Type] = false;
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true;
        Terraria.ID.BuffID.Sets.LongerExpertDebuff[Type] = true;
    }

    public override string Texture => Helper.Empty;

    public override void Update(Player player, ref int buffIndex)
    {
        player.GetModPlayer<SawPlay>().flamed = true;

        if (player.lifeRegen > 0)
            player.lifeRegen -= 55;

        if (player.statLife <= player.statLife / 1)
            player.lifeRegen -= 55;

        GenericGlowParticle particle = new(new Vector2(player.Center.X + Main.rand.Next(-2, 3), player.Center.Y), new Vector2(0, -Main.rand.NextFloat(0.2f, 3f)), Color.OrangeRed, 0.15f, 50);
        ParticleSystem.GenerateParticle(particle);
    }
}

public class SawPlay : ModPlayer
{
    public bool flamed;

    public override void ResetEffects() => flamed = false;

    public override void UpdateDead() => flamed = false;

    public override void PostUpdateRunSpeeds()
    {
        if (flamed)
        {
            Player.runAcceleration *= 5.25f;
            Player.maxRunSpeed += 3.1f;
            Player.accRunSpeed += 3.05f;
        }
    }
}