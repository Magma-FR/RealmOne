using RealmOne.NPCs.Enemies.BloodMoon.ButcherRat;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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

        Item.crit = 2;
        Item.damage = 15;
        Item.knockBack = 4f;

        Item.width = 78;
        Item.height = 76;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = ButcherRat.FrenzySound;

        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.BloodthirstyVerminsaw>();

        Item.rare = ItemRarityID.Orange;
    }

    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] == 0;
    }
}