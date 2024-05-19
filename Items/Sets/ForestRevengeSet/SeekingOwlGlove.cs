using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

using Microsoft.Xna.Framework;
using RealmOne.Common.Systems;

namespace RealmOne.Items.Sets.ForestRevengeSet
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class SeekingOwlGlove : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.height = 24;
            Item.rare = ItemRarityID.Blue;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<SeekingOwlProj>();
            Item.shootSpeed = 11f;
            Item.noMelee = true;
            Item.mana = 40;
            Item.damage = 20;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}