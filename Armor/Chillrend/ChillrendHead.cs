using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Armor.Chillrend
{
    [AutoloadEquip(EquipType.Head)]
    public class ChillrendHead : ModItem
    {
        private int Watertimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chillrend Vikinghelm");
            Tooltip.SetDefault("6% increased ranged damage and ranged attack speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.06f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.06f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            if (body.type == ModContent.ItemType<ChillrendBody>())
            {
                return legs.type == ModContent.ItemType<ChillrendLegs>();
            }
            return false;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<IceArmour>().icySHIT = true;

            player.setBonus = "When enemies strike you they inflict frostburn\nWhen enemies strike you they take 40 damage\n5% increased ranged crit chance";
            player.gills = true;
            Lighting.AddLight(player.position, 0.08f, 0.4f, 1f);
            Lighting.Brightness(2, 2);
            Watertimer++;
            Vector2 center = player.Center;
            for (int j = 0; j < 150; j++)
            {
                int dust1 = Dust.NewDust(center, 0, 0, DustID.IceTorch, 0f, 0f, 100, default, 1.2f);
                Main.dust[dust1].noGravity = true;
                Main.dust[dust1].velocity = Vector2.Zero;
                Main.dust[dust1].noLight = false;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.IceBlock, 16)
                                    .AddIngredient(ItemID.GoldBar, 6)

                        .AddIngredient(ItemID.Snowball, 40)

            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()

           .AddIngredient(ItemID.IceBlock, 16)
                                   .AddIngredient(ItemID.PlatinumBar, 6)

                        .AddIngredient(ItemID.Snowball, 40)

           .AddTile(TileID.Anvils)
           .Register();
        }
    }

    public class IceArmour : ModPlayer
    {
        public bool icySHIT = false;

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (icySHIT)
            {
                npc.SimpleStrikeNPC(damage: 40, 0);
                npc.AddBuff(BuffID.Frostburn, 120);
            }
        }
    }
}