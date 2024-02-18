using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class TatteredBarkShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tattered Bark Shield");
            Tooltip.SetDefault("'Nothing but skin penetrating wooden splinters on this shield'"
                + "\nWhen hit by an enemy, they take 20 damage and get inflicted by 'Splinted"
                + "\nWhen an enemy is inflicted by Splinted, the enemy rapidly loses life for a short duration"
                + "\nAll weapons inflict Splintered while equipping the shield");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.defense += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "BarkShield", 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BarkShieldPlayer>().splinteredShield = true;
        }
    }

    public class BarkShieldPlayer : ModPlayer
    {
        public bool splinteredShield = false;

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (splinteredShield)
            {
                npc.SimpleStrikeNPC(damage: 20, 0);
                //	npc.AddBuff(ModContent.BuffType<Splintered>(), 60); // Inflict the 'Splintered' debuff for 1 year (60 seconds * 60 minutes * 24 hours * 365 days)
            }
        }
    }
}