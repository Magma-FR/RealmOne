/*using RealmOne.Projectiles.Whip;
using RealmOne.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Summoner
{
    public class WoodenTrainWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<WoodenTrainProjectile>(), 10, 3, 4);
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
        }

        public override bool MeleePrefix()
        {
            return true;
        }
    }
}*/